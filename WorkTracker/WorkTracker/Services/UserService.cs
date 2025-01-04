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
        public async Task<Manager?> LoginManager(User user)
        {
            Manager? manager = null;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                manager =await context.Managers.FirstOrDefaultAsync(m => m.Username == user.Username);
                if(manager != null)
                    manager.UsernameNavigation = user;
            }
            return manager;
        }
        public async Task<Worker?> LoginWorker(User user)
        {
            Worker? worker = null;
            using(WorktrackerContext context = new WorktrackerContext())
            {
                worker =await context.Workers.FirstOrDefaultAsync(w => w.Username == user.Username);
                if (worker != null)
                    worker.UsernameNavigation = user;
            }
            return worker;
        }
    }
}
