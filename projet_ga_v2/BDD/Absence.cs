using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Absence
{
    public int Id { get; set; }

    public int? IdEleve { get; set; }

    public int Jour { get; set; }

    public int Heure { get; set; }

    public int? IdMatiere { get; set; }

    public virtual Elefe? IdEleveNavigation { get; set; }

    public virtual Matiere? IdMatiereNavigation { get; set; }
}
