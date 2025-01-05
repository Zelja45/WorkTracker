using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public sbyte IsActive { get; set; }

    public DateOnly CreatedAt { get; set; }

    public byte[]? Image { get; set; }

    public string AccountType { get; set; } = null!;

    public decimal? OvertimeRateWorkerSpecific { get; set; }

    public decimal? HourlyRateWorkerSpecific { get; set; }

    public int? IdSector { get; set; }

    public virtual Sector? IdSectorNavigation { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Todolist> Todolists { get; set; } = new List<Todolist>();

    public virtual ICollection<Worksession> Worksessions { get; set; } = new List<Worksession>();

    public virtual ICollection<Sector> IdSectors { get; set; } = new List<Sector>();
}
