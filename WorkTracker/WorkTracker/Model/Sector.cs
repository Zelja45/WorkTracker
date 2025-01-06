using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Sector
{
    public int IdSector { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal OvertimeHourlyRate { get; set; }

    public int DailyHoursNorm { get; set; }

    public int WeeklyHoursNorm { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<User> ManagerUsernames { get; set; } = new List<User>();
}
