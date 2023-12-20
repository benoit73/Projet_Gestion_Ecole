using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Edt
{
    public int Id { get; set; }

    public string EnseignantClasse { get; set; } = null!;

    public int? ClasseId { get; set; }

    public virtual Class? Classe { get; set; }

    public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();
}
