using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Todolistitem
{
    public int IdTodolistItem { get; set; }

    public int IdTodolist { get; set; }

    public string Content { get; set; } = null!;

    public sbyte Checked { get; set; }

    public virtual Todolist IdTodolistNavigation { get; set; } = null!;
}
