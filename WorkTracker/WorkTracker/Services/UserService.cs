using System;
using System.Collections.Generic;
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
        public User? LoginUser(string username,string password)
        {
            User? user = null;
            string passwordHash = Util.HashPassword(password);
            using (WorktrackerContext context = new WorktrackerContext())
            {
                user=context.Users.FirstOrDefault(u=>u.Username == username&&u.Password==password);
            }
            return user;
        }
        public Manager? LoginManager(User user)
        {
            Manager? manager = null;
            using (WorktrackerContext context = new WorktrackerContext())
            {
                manager = context.Managers.FirstOrDefault(m => m.Username == user.Username);
                if(manager != null)
                    manager.UsernameNavigation = user;
            }
            return manager;
        }
        public Worker? LoginWorker(User user)
        {
            Worker? worker = null;
            using(WorktrackerContext context = new WorktrackerContext())
            {
                worker = context.Workers.FirstOrDefault(w => w.Username == user.Username);
                if (worker != null)
                    worker.UsernameNavigation = user;
            }
            return worker;
        }
    }
}
