using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ga_v2.DAO
{
    internal class DAO_Cour
    {

        public void AddCour(Cour cour)
        {
            cour.EnseignantMatiereClasse = null;
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Cours.Add(cour);
                context.SaveChanges();
            }
        }
    }
}
