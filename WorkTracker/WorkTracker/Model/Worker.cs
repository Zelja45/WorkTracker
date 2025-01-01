using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Worker
{
    public string Username { get; set; } = null!;

    public decimal? OvertimeRateWorkerSpecific { get; set; }

    public decimal? HourlyRateWorkerSpecific { get; set; }

    public int IdSector { get; set; }

    public virtual Sector IdSectorNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Todolist> Todolists { get; set; } = new List<Todolist>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual ICollection<Worksession> Worksessions { get; set; } = new List<Worksession>();
}
