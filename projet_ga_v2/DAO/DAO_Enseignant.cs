using Microsoft.EntityFrameworkCore;
using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ga_v2.DAO
{
    internal class DAO_Enseignant
    {
        public IEnumerable<object> GetEnseignantsByNiveauClasse()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var enseignants = context.Enseignants.Include(e => e.EnseignantMatiereClasses).ToList();
                foreach (Enseignant enseignant in enseignants)
                {
                    foreach (EnseignantMatiereClasse emc in enseignant.EnseignantMatiereClasses)
                    {
                        if (emc.Classe == null && emc.ClasseId != 0)
                        {
                            emc.Classe = context.Classes.Single(c => c.Id == emc.ClasseId);
                        }
                    }
                }
                var enseignantsByNiveau = enseignants
                    .Where(e => e.EnseignantMatiereClasses != null && e.NomEnseignant != "deleted" && e.PrenomEnseignant != "deleted") // Filtrer les enseignants ayant une classe non nulle
                    .SelectMany(e => e.EnseignantMatiereClasses.Select(emc => new { Enseignant = e, Niveau = emc.Classe.Niveau }))
                    .GroupBy(e => e.Niveau)
                    .Select(g => new { Niveau = g.Key, Nb = g.Select(e => e.Enseignant).Distinct().Count() });

                return enseignantsByNiveau;
            }
        }
        public List<Enseignant> GetAllEnseignants()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var Enseignants = context.Enseignants.Where(e => e.NomEnseignant != "deleted" && e.PrenomEnseignant != "deleted").ToList();
                return Enseignants;
            }
        }
        public List<Enseignant> GetAllEnseignantsWithMatiereAndClasses()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var EnseignantsMatiere = context.Enseignants
                    .Include(ens => ens.Matieres)
                    .Include(ens => ens.EnseignantMatiereClasses)
                    .Where(e => e.NomEnseignant != "deleted" && e.PrenomEnseignant != "deleted")
                    .ToList();
                foreach (Enseignant enseignant in EnseignantsMatiere)
                {
                    foreach (EnseignantMatiereClasse emc in enseignant.EnseignantMatiereClasses)
                    {
                        if (emc.Classe == null && emc.ClasseId != 0)
                        {
                            emc.Classe = context.Classes.Single(c => c.Id == emc.ClasseId);
                        }
                    }
                }
                return EnseignantsMatiere;
            }
        }

        public void AddEnseignant(Enseignant enseignant)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                ICollection<Matiere> matieres = new List<Matiere>();
                foreach (Matiere matiere in enseignant.Matieres)
                {
                    matieres.Add(context.Matieres.Single(m => m.Id == matiere.Id));
                }

                enseignant.Matieres.Clear();
                context.Enseignants.Add(enseignant);
                context.SaveChanges();

                foreach (Matiere matiere in matieres)
                {
                    enseignant.Matieres.Add(context.Matieres.Single(m => m.Id == matiere.Id));
                }
                context.SaveChanges();


            }
        }

        public void DeleteEnseignant(Enseignant enseignant)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                Enseignant enseignant1 = context.Enseignants.Single(e => e.Id == enseignant.Id);
                enseignant1.PrenomEnseignant = "deleted";
                enseignant1.NomEnseignant = "deleted";
                enseignant1.Email = "deleted";
                context.Enseignants.Update(enseignant1);
                context.SaveChanges();
            }
        }


        public List<Enseignant> GetAllEnseignantsInMatiereNotInClasse(Matiere matiere, Classe classe)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var EnseignantsMatiere = context.Enseignants
                    .Include(ens => ens.Matieres)
                    .Include(ens => ens.EnseignantMatiereClasses)
                    .Where(ens => ens.Matieres.Contains(matiere))
                    .Where(ens => ens.NomEnseignant != "deleted" && ens.PrenomEnseignant != "deleted")
                    .ToList();
                foreach (Enseignant enseignant in EnseignantsMatiere.ToList())
                {
                    foreach (EnseignantMatiereClasse emc in enseignant.EnseignantMatiereClasses)
                    {
                        if (emc.ClasseId == classe.Id && emc.MatiereId == matiere.Id)
                        {
                            EnseignantsMatiere.Remove(enseignant);
                        }
                    }
                }
                return EnseignantsMatiere;
            }
        }


        //public void AddEnseignantToClass(Enseignant enseignant, Classe classe)
        //{
        //    using (var context = new Benoit73SymfonyV5Context())
        //    {
        //        var enseignantToUpdate = context.Enseignants.Include(e => e.Classes)
        //                                                  .Single(e => e.Id == enseignant.Id);
        //        var classeToAdd = context.Classes.Single(c => c.Id == classe.Id);
        //        enseignantToUpdate.Classes.Add(classeToAdd);
        //        context.SaveChanges();
        //    }
        //}

        //public void RemoveEnseignantFromClass(Enseignant enseignant, Classe classe)
        //{
        //    using (var context = new Benoit73SymfonyV5Context())
        //    {
        //        // Charger l'enseignant avec ses classes depuis la base de données
        //        var enseignantToUpdate = context.Enseignants.Include(e => e.Classes)
        //                                                  .Single(e => e.Id == enseignant.Id);

        //        // Recherche de la classe à supprimer dans la collection de classes de l'enseignant
        //        var classeToRemove = enseignantToUpdate.Classes.SingleOrDefault(c => c.Id == classe.Id);
        //        if (classeToRemove != null)
        //        {
        //            // Supprimer la classe de la collection de classes de l'enseignant
        //            enseignantToUpdate.Classes.Remove(classeToRemove);
        //            context.SaveChanges();
        //        }
        //    }
        //}

        public void UpdateEnseignant(Enseignant enseignant)
        {
            RemoveMatiereFromEnseignant(enseignant);

            using (var context = new Benoit73SymfonyV5Context())
            {
                var enseignantToUpdate = context.Enseignants.Single(e => e.Id == enseignant.Id);
                enseignantToUpdate.NomEnseignant = enseignant.NomEnseignant;
                enseignantToUpdate.PrenomEnseignant = enseignant.PrenomEnseignant;
                enseignantToUpdate.Email = enseignant.Email;

                context.SaveChanges();

                foreach (Matiere matiere in enseignant.Matieres)
                {
                    var matiereToAdd = context.Matieres.Single(m => m.Id == matiere.Id);
                    enseignantToUpdate.Matieres.Add(matiereToAdd);
                }
                context.SaveChanges();
            }
        }

        public void RemoveMatiereFromEnseignant(Enseignant enseignant)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                Enseignant oldEnseignant = context.Enseignants.Include(e => e.Matieres).Single(e => e.Id == enseignant.Id);
                oldEnseignant.Matieres.Clear();
                context.SaveChanges();
            }
        }


    }
}
