using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class User
{
    public int Id { get; set; }

    public int? EleveId { get; set; }

    public int? ParentsId { get; set; }

    public string Email { get; set; } = null!;

    /// <summary>
    /// (DC2Type:json)
    /// </summary>
    public string Roles { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsVerified { get; set; }

    public virtual Eleve? Eleve { get; set; }

    public virtual Parent? Parents { get; set; }
}
