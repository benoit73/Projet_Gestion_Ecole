using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Cour
{
    public int Id { get; set; }

    public int EnseignantMatiereClasseId { get; set; }

    public int Duree { get; set; }

    public int Jour { get; set; }

    public int Semaine { get; set; }

    public int Debut { get; set; }

    public int Annee { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual EnseignantMatiereClasse EnseignantMatiereClasse { get; set; } = null!;
}
