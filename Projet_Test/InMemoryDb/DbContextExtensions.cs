using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.InMemoryDb
{
    public static class DbContextExtensions
    {

        public static void Seed(this AFPANADbContext dbContext)
        {
            dbContext.CollaborateurAfpas.Add(new CollaborateurAfpa
            {
                NomCollaborateur = "Titi",
                MatriculeCollaborateurAfpa = "96AA011"
            });

        }

    }
}
