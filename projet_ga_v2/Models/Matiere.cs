using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Matiere
{
    public int Id { get; set; }

    public string NomMatiere { get; set; } = null!;

    public int? NbEnseignants { get; set; }

    public virtual ICollection<EnseignantMatiereClasse> EnseignantMatiereClasses { get; set; } = new List<EnseignantMatiereClasse>();

    public virtual ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
}
