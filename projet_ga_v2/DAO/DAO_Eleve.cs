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
        public List<Eleve> GetAllEleves()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var Eleves = context.Eleves.ToList();
                return Eleves;
            }
        }
        public List<Eleve> GetAllElevesWithClasse()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var ElevesClasse = context.Eleves.Include(ele => ele.Classe).ToList();
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
                    ToList();
                return ElevesClasse;
            }
        }
        public void AddEleve(Eleve eleve)
        {
            DoesParentExist(eleve.Parents, eleve);
            eleve.Parents = null;
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Eleves.Add(eleve);
                context.SaveChanges();
            }
        }
        public void DeleteEleve(Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Eleves.Remove(eleve);
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
        public void DoesParentExist(Parent parent, Eleve eleve)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var Parent = context.Parents.Where(p => p.Email == parent.Email).FirstOrDefault();
                if (Parent == null)
                {
                    context.Parents.Add(parent);
                    context.SaveChanges();
                }
                else
                {
                    eleve.ParentsId = Parent.Id;
                    context.Eleves.Update(eleve);
                    context.SaveChanges();
                }
            }
        }
    }
}
