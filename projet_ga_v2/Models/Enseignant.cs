﻿using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Enseignant
{
    public int Id { get; set; }

    public string NomEnseignant { get; set; } = null!;

    public string PrenomEnseignant { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Cour> Cours { get; set; } = new List<Cour>();

    public virtual ICollection<EnseignantMatiereClasse> EnseignantMatiereClasses { get; set; } = new List<EnseignantMatiereClasse>();

    public virtual ICollection<Classe> Classes { get; set; } = new List<Classe>();

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
}
