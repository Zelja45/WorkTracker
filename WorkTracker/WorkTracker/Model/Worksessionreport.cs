using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Worksessionreport
{
    public int WorkSessionIdSession { get; set; }

    public TimeOnly WorkedHours { get; set; }

    public TimeOnly OvertimeHours { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal OvertimeHourlyRate { get; set; }

    public virtual Worksession WorkSessionIdSessionNavigation { get; set; } = null!;
}
