using System;
using System.Collections.Generic;

namespace projet_ga_v2.BDD;

public partial class Class
{
    public int Id { get; set; }

    public string NomClasse { get; set; } = null!;

    public virtual ICollection<Edt> Edts { get; set; } = new List<Edt>();

    public virtual ICollection<Elefe> Eleves { get; set; } = new List<Elefe>();

    public virtual EnseignantClasse? EnseignantClasse { get; set; }
}
