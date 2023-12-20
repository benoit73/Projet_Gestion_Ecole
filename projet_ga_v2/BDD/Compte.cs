using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Compte
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public int? EleveId { get; set; }

    public string IdEnseignant { get; set; } = null!;

    public virtual Elefe? Eleve { get; set; }
}
