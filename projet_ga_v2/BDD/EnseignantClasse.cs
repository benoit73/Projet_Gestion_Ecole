using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class EnseignantClasse
{
    public int Id { get; set; }

    public int? EnseignantId { get; set; }

    public int? ClasseId { get; set; }

    public virtual Class? Classe { get; set; }

    public virtual Enseignant? Enseignant { get; set; }
}
