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

        #region méthode asynchrone pour le PeeController et l'action __AfficheBeneficiairePee
        public async Task<IEnumerable<Pee>> GetPeeEntrepriseWithBeneficiaireBy(int IdOffreFormation, string idEtablissement)
        {
            return await _dbContext.Pees
                .Include(P => P.MatriculeBeneficiaireNavigation)
                .Include(S => S.IdEntrepriseNavigation)
                .Where(P => P.IdOffreFormation == IdOffreFormation && P.IdEtablissement == idEtablissement).ToListAsync();
        }
        public async Task<IEnumerable<PeriodePee>> GetListPeriodePeeByIdPee(int IdOffreFormation, string idEtablissement)
        {
            var periodePees = await _dbContext.PeriodePees.Include(pr => pr.IdPeeNavigation).ToListAsync();
            List<PeriodePee> listPeriode = new List<PeriodePee>();
            foreach (var item in GetPeeEntrepriseWithBeneficiaireBy(IdOffreFormation, idEtablissement).Result)
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
        #endregion
        public decimal GetPeeBy_Idmatricule_idFormation_idetablissemnt(string matricule,int identreprise,string idetablissement)
        {
            return _dbContext.Pees.Where(e => e.MatriculeBeneficiaire == matricule && e.IdEntreprise == identreprise && e.IdEtablissement==idetablissement)
                .Select(x=>x.IdPee)
                .FirstOrDefault();
        }
    }
}
