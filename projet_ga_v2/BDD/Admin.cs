using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Admin
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;
}
