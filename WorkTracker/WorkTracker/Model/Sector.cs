using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Sector
{
    public int IdSector { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? HourlyRate { get; set; }

    public string? OvertimeHourlyRate { get; set; }

    public string? DailyHoursNorm { get; set; }

    public string? WeeklyHoursNorm { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();

    public virtual ICollection<Manager> ManagerUsernames { get; set; } = new List<Manager>();
}
