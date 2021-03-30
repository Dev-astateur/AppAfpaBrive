using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AFPANADbContext context = new AFPANADbContext();

            Pee pee = new Pee();
            IQueryable<Pee> pees = context.Pees
                .Include(P => P.MatriculeBeneficiaireNavigation)
                .Include(S => S.IdEntrepriseNavigation)
                .Where(P => P.IdOffreFormation == 20102 && P.IdEtablissement == "19011");

            //foreach (var item in pees)
            //{
            //    Console.WriteLine(item.MatriculeBeneficiaireNavigation.NomBeneficiaire);
            //}
            List<PeriodePee> list = new List<PeriodePee>();
            var periodePees = context.PeriodePees.Include(pr => pr.IdPeeNavigation).ToList();
            //var listPeeBenificiaire = pees.Include(p => p.MatriculeBeneficiaireNavigation.Pees).ToList();
            foreach (var item in pees)
            {
                foreach (var element in periodePees)
                {
                    if (element.IdPee == item.IdPee)
                    {
                        list.Add(element);
                    }
                }

            }
          
            foreach (var p in pees)
            {
               
               
                    if (p.MatriculeBeneficiaire == p.MatriculeBeneficiaire)
                    {
                        Console.WriteLine(p.IdPee);
                    }
               
            }
            

            //foreach (var element in list)
            //{
            //    string v = element.DateDebutPeriodePee.ToString("MMddyyy");
            //    Console.WriteLine(v);
            //}

            var Convention = context.Pees.Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(S => S.CodeTitreCiviliteNavigation)
                .Include(pee => pee.IdResponsableJuridiqueNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(E => E.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation)
                .FirstOrDefault(pee => pee.IdPee == 12);
            Console.WriteLine(Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire);

            var date = context.PeriodePees.Where(p => p.IdPee == Convention.IdPee).FirstOrDefault();
            Console.WriteLine(date.DateDebutPeriodePee);



            Console.WriteLine("------------------------------------------------");

            var obj = context.Contacts.Include(x => x.TitreCivilite).ToList();
            foreach(var el in obj)
            {
                Console.WriteLine(el.TitreCivilite);
                Console.WriteLine(el.TitreCivilite.TitreCiviliteComplet + " " + el.Nom  );
            }
        }
    }
}
