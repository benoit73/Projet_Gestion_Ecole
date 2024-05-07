using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Eleve
{
    public int Id { get; set; }

    public int? ParentsId { get; set; }

    public int? ClasseId { get; set; }

    public string NomEleve { get; set; } = null!;

    public string PrenomEleve { get; set; } = null!;

    public string? MailEleve { get; set; }

    public string? DateNaissance { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual Classe? Classe { get; set; }

    public virtual Parent? Parents { get; set; }

    public virtual User? User { get; set; }
}
