using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Sector
{
    public int IdSector { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string HourlyRate { get; set; } = null!;

    public string OvertimeHourlyRate { get; set; } = null!;

    public string DailyHoursNorm { get; set; } = null!;

    public string WeeklyHoursNorm { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<User> ManagerUsernames { get; set; } = new List<User>();
}
