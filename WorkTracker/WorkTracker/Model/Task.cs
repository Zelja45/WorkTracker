using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Task
{
    public int IdTask { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public sbyte Status { get; set; }

    public int Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DueDate { get; set; }

    public int Progress { get; set; }

    public string WorkerUsername { get; set; } = null!;

    public virtual User WorkerUsernameNavigation { get; set; } = null!;
}
