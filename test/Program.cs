using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            int IdOffreFormation = 20101;
            int idEntreprise = 19011;
            AFPANADbContext context = new AFPANADbContext();
            IQueryable<Pee> pees = context.Pees.Include(P => P.MatriculeBeneficiaireNavigation).ThenInclude(M => M.Pees).Where(s => s.IdOffreFormation == IdOffreFormation || s.IdEntreprise == idEntreprise);
           
           foreach(var item in pees)
            {
                Console.WriteLine($"{item.MatriculeBeneficiaireNavigation.PrenomBeneficiaire} ");
            }
                
          
                
            
        }
    }
}
