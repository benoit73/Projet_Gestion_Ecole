using Microsoft.EntityFrameworkCore;
using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_ga_v2.DAO
{
    internal class DAO_Eleve
    {
        public IEnumerable<object> GetElevesByNiveau()
        {
            using(var context = new Benoit73SymfonyV5Context())
            {
                var eleves = context.Eleves.Include(e => e.Classe).ToList();
                var elevesByNiveau = eleves
                    .Where(e => e.Classe != null && e.NomEleve != "deleted" && e.PrenomEleve != "deleted") // Filtrer les élèves ayant une classe non nulle
                    .GroupBy(e => e.Classe.Niveau)
                    .Select(g => new { Niveau = g.Key, Nb = g.Count() });
                return elevesByNiveau;
            }
        }
        public List<Eleve> GetAllEleves()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var Eleves = context.Eleves.Where(e => e.NomEleve != "deleted" && e.PrenomEleve != "deleted").ToList();
                return Eleves;
            }
        }
        public List<Eleve> GetAllElevesWithClasseAndParent()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var ElevesClasse = context.Eleves.Include(ele => ele.Classe).Include(ele => ele.Parents).Where(e => e.NomEleve != "deleted" && e.PrenomEleve != "deleted").ToList();
                return ElevesClasse;
            }
        }

        public List<Eleve>GetAllElevesExeptInClass(Classe classe)
        {
        using (var context = new Benoit73SymfonyV5Context())
            {
                var ElevesClasse = context.Eleves.
                    Include(ele => ele.Classe).
                    Where(ele => ele.ClasseId != classe.Id).
                    Where(e => e.NomEleve != "deleted" && e.PrenomEleve != "deleted").
                    ToList();
                return ElevesClasse;
            }
        }

        public void DeleteEleve(Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                Eleve eleve1 = context.Eleves.Include(e => e.Absences).Include(e => e.User).Include(e => e.Parents).Single(e => e.Id == eleve.Id);
                foreach (Absence absence in eleve1.Absences)
                {
                    context.Absences.Remove(absence);
                }
                if (eleve1.User != null)
                {
                    context.Users.Remove(eleve1.User);
                }
                if (eleve1.Parents.Eleves.Count == 1)
                {
                    User userParent = context.Users.Where(u => u.Email == eleve1.Parents.Email).FirstOrDefault();
                    context.Parents.Remove(eleve1.Parents);
                    if (userParent != null)
                    {
                        context.Users.Remove(userParent);
                    }
                }
                context.Eleves.Remove(eleve1);
                context.SaveChanges();
            }
        }
        public void UpdateEleve(Eleve eleve)
        {
            DoesParentExist(eleve.Parents, eleve);
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Eleves.Update(eleve);
                context.SaveChanges();
            }
        }

        public void AddEleve(Eleve eleve)
        {
            Parent leparent  = eleve.Parents;
            eleve.Parents = null;
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Eleves.Add(eleve);
                context.SaveChanges();
            }
            DoesParentExist(leparent, eleve);
        }
        public bool DoesParentExist(Parent parent, Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var Parent = context.Parents.Where(p => p.Email == parent.Email).FirstOrDefault();
                if (Parent == null)
                {
                    Eleve eleve1 = context.Eleves.Where(e => e.Id == eleve.Id).FirstOrDefault();
                    parent.Eleves.Add(eleve1);
                    context.Parents.Add(parent);
                    context.SaveChanges();
                    return false;
                }
                else
                {
                    eleve.ParentsId = Parent.Id;
                    context.Eleves.Update(eleve);
                    context.SaveChanges();
                    return true;
                }
            }
        }
    }
}
