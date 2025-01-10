using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;

namespace WorkTracker.Services
{
    public class TODOListService
    {
        public async Task<List<Todolist>> GetWorkerTodolists(string username)
        {
            List<Todolist> list = new List<Todolist>();
            using (WorktrackerContext context = new WorktrackerContext()) 
            { 
               list=await context.Todolists.Where(t=>t.WorkerUsername == username).Include(t=>t.Todolistitems).ToListAsync();
            }
            return list;
        }
        public async System.Threading.Tasks.Task UpdateItem(Todolistitem item)
        {
            using(WorktrackerContext context=new WorktrackerContext())
            {
                context.Todolistitems.Update(item);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task AddNewItem(Todolistitem item)
        {
            using(WorktrackerContext context=new WorktrackerContext())
            {
                var list=await context.Todolists.FirstOrDefaultAsync(t=>t.IdTodolist==item.IdTodolist);
                item.IdTodolistNavigation = list;
                context.Todolistitems.Add(item);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task AddNewList(Todolist list)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                await context.Todolists.AddAsync(list);
                await context.SaveChangesAsync();
            }
        }
        public async System.Threading.Tasks.Task UpdateListIsSelected(Todolist list)
        {
            using (WorktrackerContext context = new WorktrackerContext())
            {
                var realList = await context.Todolists.FirstOrDefaultAsync(t => t.IdTodolist ==list.IdTodolist);
                realList.IsSelected = list.IsSelected;
                context.Todolists.Update(realList);
                await context.SaveChangesAsync();
            }
        }

    }
}
