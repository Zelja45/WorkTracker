﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    .Where(u => u.IdSector.HasValue && sectorIds.Contains(u.IdSector.Value)&&u.IsActive==(sbyte)1)
                    .ToListAsync();
            }
            return users;
        }
    }
}
