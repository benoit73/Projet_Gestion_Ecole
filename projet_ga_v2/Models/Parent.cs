using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Parent
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();

    public virtual User? User { get; set; }
}
