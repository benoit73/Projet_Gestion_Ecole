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
                    Include("EnseignantMatiereClasses.Matiere").
                    Include("Enseignants.Cours").
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

        public void DeleteClasse(Classe classe)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Classes.Remove(classe);
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


    }
}
