using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Enseignant
{
    public int Id { get; set; }

    public string NomEnseignant { get; set; } = null!;

    public string PrenomEnseignant { get; set; } = null!;

    public int? MatiereId { get; set; }

    public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();

    public virtual EnseignantClasse? EnseignantClasse { get; set; }

    public virtual Matiere? Matiere { get; set; }
}
