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
    }
}
