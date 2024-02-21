using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class UserParent
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    /// <summary>
    /// (DC2Type:json)
    /// </summary>
    public string Roles { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Parent? Parent { get; set; }
}
