using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Pauselog
{
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int IdWorkSession { get; set; }

    public virtual Worksession IdWorkSessionNavigation { get; set; } = null!;
}
