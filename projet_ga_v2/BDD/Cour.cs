using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Cour
{
    public int Id { get; set; }

    public int Duree { get; set; }

    public int Debut { get; set; }

    public int Jour { get; set; }

    public int? EdtId { get; set; }

    public int? EnseignantId { get; set; }

    public int? MatiereId { get; set; }

    public virtual Edt? Edt { get; set; }

    public virtual Enseignant? Enseignant { get; set; }

    public virtual Matiere? Matiere { get; set; }
}
