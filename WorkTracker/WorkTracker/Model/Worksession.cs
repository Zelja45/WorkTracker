using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Worksession
{
    public int IdSession { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public TimeOnly? WorkedHours { get; set; }

    public sbyte Status { get; set; }

    public string WorkerUsername { get; set; } = null!;

    public virtual Pauselog? Pauselog { get; set; }

    public virtual User WorkerUsernameNavigation { get; set; } = null!;

    public virtual Worksessionreport? Worksessionreport { get; set; }
}
