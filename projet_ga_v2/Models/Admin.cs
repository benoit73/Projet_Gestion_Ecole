using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
