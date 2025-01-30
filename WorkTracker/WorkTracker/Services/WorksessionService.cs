using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Utils;

namespace WorkTracker.Services
{
    public class WorksessionService
    {
        public async System.Threading.Tasks.Task AddNewWorkSession(Worksession session)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                var worker =await context.Users.FirstOrDefaultAsync(u => u.Username == session.WorkerUsername);
                session.WorkerUsernameNavigation = worker;
                await context.Worksessions.AddAsync(session);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task StopWorkSession(Worksession session)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                context.Worksessions.Update(session);

                Pauselog pauseLog = await context.Pauselogs
                    .FirstOrDefaultAsync(p => p.IdWorkSession == session.IdSession && p.EndTime == null);
                if (pauseLog != null)
                {
                    pauseLog.EndTime = DateTime.Now;
                    context.Entry(pauseLog).State = EntityState.Modified;
                }

                var sector = context.Sectors.FirstOrDefault(s => context.Users.FirstOrDefault(u => u.Username == session.WorkerUsername).IdSector == s.IdSector);
                TimeOnly overtimeHours= new TimeOnly(0, 0);
                TimeOnly workedHours= new TimeOnly(0, 0);

                if (session.WorkedHours.HasValue&&session.WorkedHours!=null)
                {
                    if (session.WorkedHours.Value.Hour >= sector.DailyHoursNorm)
                    {
                        TimeSpan workedSpan = session.WorkedHours.Value.ToTimeSpan();
                        TimeSpan dailyNormSpan = new TimeSpan(sector.DailyHoursNorm, 0, 0);
                        TimeSpan overtimeSpan = workedSpan - dailyNormSpan;
                        overtimeHours = TimeOnly.FromTimeSpan(overtimeSpan);
                        workedHours= TimeOnly.FromTimeSpan(dailyNormSpan);
                    }
                    else 
                    {
                        workedHours = session.WorkedHours.Value;
                    }
                }

                Worksessionreport report = new Worksessionreport
                {
                    HourlyRate=sector.HourlyRate,
                    WorkedHours=workedHours,
                    OvertimeHourlyRate=sector.OvertimeHourlyRate,
                    OvertimeHours=overtimeHours,
                    WorkSessionIdSession=session.IdSession,
                };

                context.Worksessionreports.Add(report);
                await context.SaveChangesAsync();

            }
        }
        public async System.Threading.Tasks.Task PauseWorkSession(Worksession session)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                session.Status = 1;
                Pauselog pauseLog = new Pauselog
                {
                    IdWorkSession=session.IdSession,
                    StartTime = DateTime.Now,
                    
                };
                
                var realSession=await context.Worksessions.FirstOrDefaultAsync(s=>s.IdSession==session.IdSession);
                realSession.Status = 1;
                context.Worksessions.Update(realSession);
                pauseLog.IdWorkSessionNavigation = realSession;
                await context.Pauselogs.AddAsync(pauseLog);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task ResumeWorkSession(Worksession session)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                Pauselog pauseLog = await context.Pauselogs
                    .FirstOrDefaultAsync(p => p.IdWorkSession == session.IdSession && p.EndTime == null);
                var realSession = await context.Worksessions.FirstOrDefaultAsync(s => s.IdSession == session.IdSession);
                realSession.Status = 0;
                context.Worksessions.Update(realSession);
                if (pauseLog != null)
                {
                    pauseLog.EndTime = DateTime.Now;
                    context.Entry(pauseLog).State = EntityState.Modified;
                    

                    await context.SaveChangesAsync();
                }
            }
        }

        public async System.Threading.Tasks.Task<List<Worksession>> GetAllEndedWorkerSessionsInMonthOfYear(String username,int year,int month)
        {
            List<Worksession> sessions = new List<Worksession>();
            using(WorktrackerContext context=new WorktrackerContext())
            {
                sessions = await context.Worksessions.Where(ws => ws.WorkerUsername == username && ws.EndTime != null&&ws.StartTime.Month==month&&ws.StartTime.Year==year).
                    Include(ws => ws.Pauselogs).Include(ws => ws.Worksessionreport).ToListAsync();
            }
            return sessions;
        }

        public async System.Threading.Tasks.Task<TimeOnly> GetTotalTodayWorkingTime()
        {
            TimeOnly timeOnly = new TimeOnly(0,0,0);
            using(WorktrackerContext context=new WorktrackerContext())
            {
                var sessions = await context.Worksessions.Where(w => w.EndTime != null && w.EndTime.Value.Date == DateTime.Now.Date).ToListAsync();
                foreach(var session in sessions)
                {
                    timeOnly = Util.SumTimeOnly(timeOnly, session.WorkedHours.Value);
                }
            }
            return timeOnly;
        }

        public async System.Threading.Tasks.Task<TimeOnly> GetTotalCurrentWeekWorkingTime()
        {
            TimeOnly timeOnly = new TimeOnly(0, 0, 0);

            DateTime today = DateTime.Now;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            using (WorktrackerContext context = new WorktrackerContext())
            {
                var sessions = await context.Worksessions.Where(w => w.EndTime != null && w.EndTime.Value.Date >= startOfWeek.Date&&w.EndTime.Value.Date<=endOfWeek.Date).ToListAsync();
                foreach (var session in sessions)
                {
                    timeOnly = Util.SumTimeOnly(timeOnly, session.WorkedHours.Value);
                }
            }
            return timeOnly;
        }




    }
}
