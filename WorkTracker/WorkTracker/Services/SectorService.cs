﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Model;

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
    }
}
