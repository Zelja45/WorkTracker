using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Todolist
{
    public int IdTodolist { get; set; }

    public string Title { get; set; } = null!;

    public sbyte IsSelected { get; set; }

    public string WorkerUsername { get; set; } = null!;

    public virtual ICollection<Todolistitem> Todolistitems { get; set; } = new List<Todolistitem>();

    public virtual User WorkerUsernameNavigation { get; set; } = null!;
}
