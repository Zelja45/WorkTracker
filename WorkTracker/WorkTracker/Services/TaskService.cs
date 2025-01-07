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

                if (manager == null)
                {
                    throw new InvalidOperationException("Manager not found.");
                }

                // ID-ovi sektora kojima upravlja menadžer
                var managedSectorIds = manager.IdSectors.Select(s => s.IdSector).ToList();

                // Filtriranje taskova prema sektorima kojima upravlja
                    tasks = await context.Tasks
                    .Include(t => t.WorkerUsernameNavigation) // Uključujemo informacije o radniku
                    .Where(t =>
                        t.WorkerUsernameNavigation.IdSector.HasValue &&
                        managedSectorIds.Contains(t.WorkerUsernameNavigation.IdSector.Value))
                    .ToListAsync();
            }
            return tasks;
        }
    }
}
