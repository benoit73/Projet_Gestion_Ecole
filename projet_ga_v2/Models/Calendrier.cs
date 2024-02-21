using System;
using System.Collections.Generic;

namespace projet_ga_v2.Models;

public partial class Calendrier
{
    public int Id { get; set; }

    public int Annee { get; set; }

    public int Mois { get; set; }

    public int Jour { get; set; }

    public int Semaine { get; set; }

    public int Date { get; set; }
}
