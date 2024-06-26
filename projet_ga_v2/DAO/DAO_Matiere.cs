﻿using projet_ga_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace projet_ga_v2.DAO
{
    public class DAO_Matiere
    {
        public List<Matiere> GetAllMatieres()
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                var AllMatieres = context.Matieres.Where(e => e.NomMatiere != "deleted").ToList();
                return AllMatieres;
            }
        }

        public void DeleteMatiere(Matiere matiere)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                matiere.NomMatiere = "deleted";
                context.Matieres.Update(matiere);
                context.SaveChanges();
            }
        }

        public void AddMatiere(Matiere matiere)
        {
            using (var context = new Benoit73SymfonyV5Context())
            {
                context.Matieres.Add(matiere);
                context.SaveChanges();
            }
        }

    }
}
