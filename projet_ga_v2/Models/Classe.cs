using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Classe
{
    public int Id { get; set; }

    public string NomClasse { get; set; } = null!;

    public string? Niveau { get; set; }

    public int? NbEleves { get; set; }

    public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();

    public virtual ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();

    public virtual ICollection<EnseignantMatiereClasse> EnseignantMatiereClasses { get; set; } = new List<EnseignantMatiereClasse>();

    public virtual ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
}
