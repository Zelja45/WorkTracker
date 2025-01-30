using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Model;

namespace WorkTracker.Services
{
    public class TaskService
    {
        public async Task<List<Model.Task>> GetAllTasksOfManager(string username)
        {
            List<Model.Task> tasks;
            using (WorktrackerContext context = new WorktrackerContext()) 
            {
                var manager = await context.Users
            .Include(u => u.IdSectors)
            .FirstOrDefaultAsync(u => u.Username == username);
                var managedSectorIds = manager.IdSectors.Select(s => s.IdSector).ToList();

                
                    tasks = await context.Tasks
                    .Include(t => t.WorkerUsernameNavigation) 
                    .Where(t =>
                        t.WorkerUsernameNavigation.IdSector.HasValue &&
                        managedSectorIds.Contains(t.WorkerUsernameNavigation.IdSector.Value))
                    .ToListAsync();
            }
            return tasks;
        }
        public async System.Threading.Tasks.Task DeleteTask(Model.Task task)
        {
            using(WorktrackerContext context = new WorktrackerContext())
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task AddNewTask(Model.Task task,User worker)
        {
            using(WorktrackerContext context = new WorktrackerContext())
            {
                var workerToConnect=await context.Users.FirstOrDefaultAsync(u=>u.Username == worker.Username);
                task.WorkerUsernameNavigation = workerToConnect;
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<List<Model.Task>> getAllTasksOfWorker(String workerUsername)
        {
            List<Model.Task> tasks = new List<Model.Task>();
            using(WorktrackerContext context=new WorktrackerContext())
            {
                tasks = await context.Tasks.Where(t => t.WorkerUsername == workerUsername).ToListAsync(); 
            }
            return tasks;
        }
        public async System.Threading.Tasks.Task UpdateTask(Model.Task task)
        {
            using(WorktrackerContext context=new WorktrackerContext())
            {
                var realTask = await context.Tasks.FirstOrDefaultAsync(t => t.IdTask == task.IdTask);
                if (realTask != null)
                {
                    realTask.Status = task.Status;
                    realTask.Progress = task.Progress;
                    realTask.Pinned= task.Pinned;
                    context.Tasks.Update(realTask);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async System.Threading.Tasks.Task<Model.Task?> GetPinnedTask(String workerUsername)
        {
            Model.Task? task = null;
            using(WorktrackerContext context=new WorktrackerContext())
            {
                task = await context.Tasks.FirstOrDefaultAsync(t => t.WorkerUsername == workerUsername && t.Pinned == 1 && t.Status <= 1 && t.DueDate >= DateTime.Now);
            }
            return task;
        }

        public async System.Threading.Tasks.Task<List<Model.Task>> GetTasksForCurrentWeek()
        {
            List<Model.Task> tasks=new List<Model.Task>();
            DateTime today = DateTime.Now;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            using(WorktrackerContext context=new WorktrackerContext())
            {
                tasks=await context.Tasks.Where(t=>t.DueDate.Date>=startOfWeek.Date&&t.DueDate.Date<=endOfWeek.Date).ToListAsync();
            }
            return tasks;
        }

        public async System.Threading.Tasks.Task<List<Model.Task>> GetTasksForCurrentDay()
        {
            List<Model.Task> tasks = new List<Model.Task>();
            using(WorktrackerContext context=new WorktrackerContext())
            {
                tasks=await context.Tasks.Where(t=>t.DueDate.Date==DateTime.Now.Date).ToListAsync();
            }
            return tasks;
        }
    }
}
