using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Manager:User
{
    public string Username { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual ICollection<Sector> IdSectors { get; set; } = new List<Sector>();
}
