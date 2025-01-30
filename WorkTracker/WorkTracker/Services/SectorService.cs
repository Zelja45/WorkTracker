using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Model;
using WorkTracker.Utils;

namespace WorkTracker.Services
{
    public class SectorService
    {
        public async System.Threading.Tasks.Task AddNewSector(Sector sector,List<User> selectedManagers)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                var managers = await context.Users
                    .Where(u => selectedManagers.Contains(u))
                    .ToListAsync();
                sector.ManagerUsernames = managers;
                await context.Sectors.AddAsync(sector);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task<int> CountNumberOfMenagersSectors(string username)
        {
            int count = 0;
            using(WorktrackerContext context = new WorktrackerContext())
            {
                count=await context.Sectors.Where( s=> s.ManagerUsernames.Where(u=>u.Username.Equals(username)).Count()==1).CountAsync();
            }
            return count;
        }
        public async System.Threading.Tasks.Task<List<Sector>> GetManagerSectors(string username)
        {
            List<Sector> sectors= new List<Sector>();
            using(WorktrackerContext context = new WorktrackerContext()) 
            { 
                sectors= await context.Sectors.Where(s => s.ManagerUsernames.Where(u => u.Username.Equals(username)).Count() == 1).ToListAsync();
            }
            return sectors;
        }
        public async System.Threading.Tasks.Task<List<User>> GetAllManagerWorkers(string username)
        {
            List<User> users = new List<User>();
            using (WorktrackerContext context = new WorktrackerContext())
            {
                var manager = await context.Users
            .Include(u => u.IdSectors)
            .FirstOrDefaultAsync(u => u.Username == username);

                var sectorIds = manager.IdSectors.Select(s => s.IdSector).ToList();

                users = await context.Users
                    .Where(u => u.IdSector.HasValue && sectorIds.Contains(u.IdSector.Value)&&u.IsActive==(sbyte)1).Include(u=>u.IdSectorNavigation)
                    .ToListAsync();
            }
            return users;
        }
        public async System.Threading.Tasks.Task<int> GetNumberOfWorkersInSector(int sectorId)
        {
            int count = 0;
            using(WorktrackerContext context = new WorktrackerContext()) 
            { 
                count= await context.Sectors.Where(s => s.IdSector == sectorId).Select(s => s.Users.Count).FirstOrDefaultAsync();
            }
            return count;
        }
        public async System.Threading.Tasks.Task<List<User>> GetWorkersInSector(int sectorId)
        {
            List<User> workers= new List<User>();
            using (WorktrackerContext context = new WorktrackerContext())
            {
                workers = await context.Users.Where(u=>u.IdSector==sectorId).ToListAsync();
            }
            return workers;
        }
        public async System.Threading.Tasks.Task RemoveWorkerFromSector(int sectorId,string workerUsername)
        {
            using(WorktrackerContext context = new WorktrackerContext())
            {
                var sector = await context.Sectors.FirstOrDefaultAsync(s=>s.IdSector==sectorId);
                var worker=await context.Users.FirstOrDefaultAsync(u=>u.Username==workerUsername);
                sector.Users.Remove(worker);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task UpdateSectorInfo(int sectorId, decimal newHourlyRate, decimal newOvertimeRate)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                var sector= await context.Sectors.FirstOrDefaultAsync(s => s.IdSector == sectorId);
                sector.OvertimeHourlyRate = newOvertimeRate;
                sector.HourlyRate = newHourlyRate;
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task AddWorkerToSector(int idSector,string workerUsername)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            { 
                var sector = await context.Sectors.FirstOrDefaultAsync(s => s.IdSector == idSector);
                var worker = await context.Users.FirstOrDefaultAsync(u => u.Username == workerUsername);

                sector.Users.Add(worker);
                await context.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<Sector?> GetSector(int sectorId)
        {
            Sector? sector = null ;
            using(WorktrackerContext context = new WorktrackerContext())
            {
               sector = await context.Sectors.FirstOrDefaultAsync(s => s.IdSector == sectorId);
            }
            return sector;
        }
    }
}
