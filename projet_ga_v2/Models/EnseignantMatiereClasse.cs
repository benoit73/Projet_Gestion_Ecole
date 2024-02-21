using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class EnseignantMatiereClasse
{
    public int Id { get; set; }

    public int EnseignantId { get; set; }

    public int MatiereId { get; set; }

    public int ClasseId { get; set; }

    public virtual Classe Classe { get; set; } = null!;

    public virtual Enseignant Enseignant { get; set; } = null!;

    public virtual Matiere Matiere { get; set; } = null!;
}
