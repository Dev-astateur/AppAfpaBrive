using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;

namespace AppAfpaBrive.Web.Layers
{
     public class PeeLayer
    {
        private readonly AFPANADbContext _dbContext = null;

        public PeeLayer (AFPANADbContext context) 
        {
            _dbContext = context;
        }

        public IEnumerable<Pee> GetPeeByMatriculeCollaborateurAfpa(string idMatricule)
        {
            return _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.Etat == 0)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation);
        }

        public Pee GetPeeByIdPee(int idPee)
        {
            return _dbContext.Pees.Where(e=>e.IdPee==idPee)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation).ThenInclude(e=>e.Idpays2Navigation)
                .FirstOrDefault();
        }

        public void Pee_Create(Pee pee)
        {
            _dbContext.Pees.Add(pee);
            _dbContext.SaveChanges();
        }
        public IEnumerable<Pee> GetPeeEntrepriseWithBeneficiaireBy(int IdOffreFormation, string idEtablissement)
        {
            return _dbContext.Pees
                .Include(P => P.MatriculeBeneficiaireNavigation)
                .Include(S => S.IdEntrepriseNavigation)
                .Where(P => P.IdOffreFormation == IdOffreFormation && P.IdEtablissement == idEtablissement);
        }
        public IEnumerable<PeriodePee> GetListPeriodePeeByIdPee(int IdOffreFormation, string idEtablissement)
        {
            var periodePees = _dbContext.PeriodePees.Include(pr => pr.IdPeeNavigation).ToList();
            List<PeriodePee> listPeriode = new List<PeriodePee>();
            foreach (var item in GetPeeEntrepriseWithBeneficiaireBy(IdOffreFormation, idEtablissement))
            {
                foreach (var element in periodePees)
                {
                    if (element.IdPee == item.IdPee)
                    {
                        listPeriode.Add(element);
                    }
                }

            }
            return listPeriode;
        }
        public decimal GetPeeBy_Idmatricule_idFormation_idetablissemnt(string matricule,int identreprise,string idetablissement)
        {
            return _dbContext.Pees.Where(e => e.MatriculeBeneficiaire == matricule && e.IdEntreprise == identreprise && e.IdEtablissement==idetablissement)
                .Select(x=>x.IdPee)
                .FirstOrDefault();
        }
    }
}
