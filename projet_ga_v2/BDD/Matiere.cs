using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Matiere
{
    public int Id { get; set; }

    public string NomMatiere { get; set; } = null!;

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();

    public virtual ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
}
