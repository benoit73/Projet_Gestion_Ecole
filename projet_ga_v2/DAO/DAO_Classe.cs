using Microsoft.EntityFrameworkCore;
using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ga_v2.DAO
{
    internal class DAO_Classe
    {
        public Classe GetClasseById(Classe classee)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var classe = context.Classes.
                    Include("Eleves").
                    Include("EnseignantMatiereClasses").
                    Include("EnseignantMatiereClasses.Enseignant").
                    Include("EnseignantMatiereClasses.Matiere").
                    Include("EnseignantMatiereClasses.Cours").
                    FirstOrDefault(c => c.Id == classee.Id);
                return classe;
            }
        }
        public List<Classe> GetAllClasses()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var classes = context.Classes.
                    Include("Eleves").
                    Include("EnseignantMatiereClasses.Matiere").
                    ToList();
                return classes;
            }
        }
        
        public void AddClasse(Classe classe)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Classes.Add(classe);
                context.SaveChanges();
            }
        }

        public void DeleteClasse(Classe classe_)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                Classe classe = context.Classes.FirstOrDefault(c => c.Id == classe_.Id);

                var eleves = context.Eleves.Where(e => e.ClasseId == classe.Id).ToList();

                // Mettre à jour la colonne classe_id de chaque élève à NULL
                foreach (var eleve in eleves)
                {
                    eleve.ClasseId = null;
                }

                // Récupérer tous les enseignantmatiereclasse liés à la classe
                var enseignantMatiereClasses = context.EnseignantMatiereClasses
                                                      .Where(emc => emc.ClasseId == classe.Id)
                                                      .ToList();
                foreach (var emc in enseignantMatiereClasses)
                {
                    // Supprimer tous les cours liés à l'enseignantmatiereclasse
                    var cours = context.Cours
                                      .Where(c => c.EnseignantMatiereClasseId == emc.Id)
                                      .ToList();
                    foreach (var cour in cours)
                    {
                        var absences = context.Absences
                                              .Where(a => a.CourId == cour.Id)
                                              .ToList();
                        context.Absences.RemoveRange(absences);
                    }
                    context.Cours.RemoveRange(cours);
                }
                // Supprimer tous les enseignantmatiereclasse liés
                context.EnseignantMatiereClasses.RemoveRange(enseignantMatiereClasses);

                // Supprimer la classe elle-même
                context.Classes.Remove(classe);

                // Enregistrer les modifications
                context.SaveChanges();
            }
        }


        public void AddEleveToClasse(Classe classe, Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Classes.Attach(classe); // Assurez-vous d'attacher la classe au contexte si elle n'est pas déjà suivie
                classe.Eleves.Add(eleve);
                context.SaveChanges();
            }
        }

        public void RemoveEleveFromClasse(Classe classe, Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Classes.Attach(classe); // Assurez-vous d'attacher la classe au contexte si elle n'est pas déjà suivie
                classe.Eleves.Remove(eleve);
                context.SaveChanges();
            }
        }

        public IEnumerable<object> GetNbClassesNiveaux()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var nbClassesNiveau = context.Classes
                    .GroupBy(c => c.Niveau)
                    .Select(g => new { Niveau = g.Key, Count = g.Count() })
                    .ToList();
                return nbClassesNiveau;
            }
        }


    }
}
