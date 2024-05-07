using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Absence
{
    public int Id { get; set; }

    public int EleveId { get; set; }

    public int CourId { get; set; }

    public bool Justifiee { get; set; }

    public virtual Cour Cour { get; set; } = null!;

    public virtual Eleve Eleve { get; set; } = null!;
}
