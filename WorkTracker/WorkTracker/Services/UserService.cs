using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Utils;

namespace WorkTracker.Services
{
    public class UserService
    {
        public UserService() { }
        public async Task<User?> LoginUser(string username,string password)
        {
            User? user = null;
            string passwordHash = Util.HashPassword(password);
            using (WorktrackerContext context = new WorktrackerContext())
            {
                user =await context.Users.FirstOrDefaultAsync(u=>u.Username == username&&u.Password==password);
            }
            return user;
        }
        public async Task<int> GetNumberOfWorkers()
        {
            int countOfUsers = 0;
            using(WorktrackerContext context = new WorktrackerContext())
            {
                countOfUsers = await context.Users.Where(u=>u.AccountType==Constants.WorkerKeyWord&&u.IsActive==(sbyte)1).CountAsync();
            }    
            return countOfUsers;
        }
        public async Task<int> GetNumberOfManagers()
        {
            int countOfUsers = 0;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                countOfUsers = await context.Users.Where(u => u.AccountType == Constants.ManagerKeyWord && u.IsActive == (sbyte)1).CountAsync();
            }
            return countOfUsers;
        }
        public async Task<int> GetNumberOfAdmins()
        {
            int countOfUsers = 0;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                countOfUsers = await context.Users.Where(u => u.AccountType == Constants.AdminKeyWord && u.IsActive == (sbyte)1).CountAsync();
            }
            return countOfUsers;
        }
        public async Task<int> GetNumberOfSectors()
        {
            int countOfSectors = 0;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                countOfSectors = await context.Sectors.CountAsync();
            }
            return countOfSectors;
        }
        public async Task<int> GetNumberOfWorkersDeactivated()
        {
            int countOfUsers = 0;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                countOfUsers = await context.Users.Where(u => u.AccountType == Constants.WorkerKeyWord && u.IsActive == (sbyte)0).CountAsync();
            }
            return countOfUsers;
        }
        public async Task<int> GetNumberOfManagersDeactivated()
        {
            int countOfUsers = 0;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                countOfUsers = await context.Users.Where(u => u.AccountType == Constants.ManagerKeyWord && u.IsActive == (sbyte)0).CountAsync();
            }
            return countOfUsers;
        }
    }
}
