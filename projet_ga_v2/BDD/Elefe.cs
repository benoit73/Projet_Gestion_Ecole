using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Elefe
{
    public int Id { get; set; }

    public string NomEleve { get; set; } = null!;

    public string PrenomEleve { get; set; } = null!;

    public string MailEleve { get; set; } = null!;

    public int? ClasseId { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual Class? Classe { get; set; }

    public virtual Compte? Compte { get; set; }
}
