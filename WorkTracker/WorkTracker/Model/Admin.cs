using System;
using System.Collections.Generic;

namespace WorkTracker.Model;

public partial class Admin
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;
}
