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

    public virtual Manager? Manager { get; set; }

    public virtual Worker? Worker { get; set; }
}
