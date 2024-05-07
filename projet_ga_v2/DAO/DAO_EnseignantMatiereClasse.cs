using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ga_v2.DAO
{
    public class DAO_EnseignantMatiereClasse
    {
        public void DeleteEnseignantMatiereClasse(Enseignant enseignant, Matiere matiere, Classe classe)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                EnseignantMatiereClasse enseignantMatiereClasse = context.EnseignantMatiereClasses.Single(emc => emc.EnseignantId == enseignant.Id && emc.MatiereId == matiere.Id && emc.ClasseId == classe.Id);
                if (context.Absences.Any(a => a.Cour.EnseignantMatiereClasseId == enseignantMatiereClasse.Id))
                {
                    throw new Exception("Impossible de supprimer l'enseignant matiere classe car il y a des absences associées");
                }
                else
                {
                    context.Cours.RemoveRange(context.Cours.Where(c => c.EnseignantMatiereClasseId == enseignantMatiereClasse.Id));
                    context.EnseignantMatiereClasses.Remove(enseignantMatiereClasse);
                    context.SaveChanges();
                }
                

            }
        }
        public void AddEnseignantMatiereClasse(Enseignant enseignant, Matiere matiere, Classe classe)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                EnseignantMatiereClasse enseignantMatiereClasse = new EnseignantMatiereClasse();
                Enseignant enseignant1 = context.Enseignants.Find(enseignant.Id);
                Matiere matiere1 = context.Matieres.Find(matiere.Id);
                Classe classe1 = context.Classes.Find(classe.Id);
                enseignantMatiereClasse.Enseignant = enseignant1;
                enseignantMatiereClasse.Matiere = matiere1;
                enseignantMatiereClasse.Classe = classe1;

                context.EnseignantMatiereClasses.Add(enseignantMatiereClasse);
                context.SaveChanges();
            }
        }
    }
}
