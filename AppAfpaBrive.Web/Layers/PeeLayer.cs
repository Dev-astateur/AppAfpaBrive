using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using AppAfpaBrive.Web.ModelView;
using ReflectionIT.Mvc.Paging;

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
                .LastOrDefault();
        }

        public decimal Pee_Create_ID_Back(Pee pee)
        {
            _dbContext.Pees.Add(pee);
            _dbContext.SaveChanges();
            return pee.IdPee;
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

        public async Task<PagingList<ListePeeAValiderModelView>> GetPeeByMatriculeCollaborateurAfpaAsync(string idMatricule, int page = 1)
        {
            var qry = _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.EtatPee == 0)
                .Include(e => e.Id)
                .Include(e => e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation).
                Select(e => new ListePeeAValiderModelView()
                {
                    NomBeneficiaire = e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    RaisonSociale = e.IdEntrepriseNavigation.RaisonSociale,
                    IdPee = e.IdPee
                }).AsQueryable();
            Task<PagingList<ListePeeAValiderModelView>> model = PagingList.CreateAsync((IOrderedQueryable<ListePeeAValiderModelView>)qry, 10, page);
            model.Result.Action = "ListePeeAValider";
            return await model;
        }

        public async Task<PeeEntrepriseModelView> GetPeeByIdPeeOffreEntreprisePaysAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e => e.IdPee == idPee)
                .Include(e => e.Id)
                .Include(e => e.IdEntrepriseNavigation).ThenInclude(e => e.Idpays2Navigation)
                .Select(e => new PeeEntrepriseModelView()
                {
                    IdPee = e.IdPee,
                    MatriculeCollaborateurAfpa = e.Id.MatriculeCollaborateurAfpa,
                    IdEntrepriseNavigation = new EntrepriseModelView(e.IdEntrepriseNavigation)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PeeModelView> GetPeeByIdAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e => e.IdPee == idPee).Select(e=>new PeeModelView() 
            { 
                IdPee = e.IdPee,
                MatriculeBeneficiaire = e.MatriculeBeneficiaire,
                IdTuteur = e.IdTuteur,
                IdResponsableJuridique = e.IdResponsableJuridique,
                IdEntreprise = e.IdEntreprise,
                IdOffreFormation = e.IdOffreFormation,
                IdEtablissement = e.IdEtablissement,
                Remarque = e.Remarque,
                EtatPee = e.EtatPee,
                Id = new OffreFormationModelView(e.Id),
                IdEntrepriseNavigation = new EntrepriseModelView(e.IdEntrepriseNavigation),
                MatriculeBeneficiaireNavigation = new BeneficiaireModelView(e.MatriculeBeneficiaireNavigation),
                IdResponsableJuridiqueNavigation = new ProfessionnelModelView(e.IdResponsableJuridiqueNavigation),
                IdTuteurNavigation = new ProfessionnelModelView(e.IdTuteurNavigation),
            }).FirstOrDefaultAsync();
        }

        public async Task<string> GetPeeMatriculeFormateurByIdAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e=>e.IdPee == idPee)
                .Include(e=>e.Id).Select(e=>e.Id.MatriculeCollaborateurAfpa).FirstOrDefaultAsync();
        }

        public async Task<string> GetPeeMatriculeFormateurByIdAsync(int idPee)
        {
            return await _dbContext.Pees.Where(e => e.IdPee == idPee)
                .Include(e => e.Id).Select(e => e.Id.MatriculeCollaborateurAfpa).FirstAsync();
        }

        public async Task<ICollection<PeeDocumentModelView>> GetPeeDocumentByIdAsync(decimal idPee)
        {
            return await _dbContext.PeeDocuments.Where(e => e.IdPee == idPee)
                .OrderBy(e=>e.NumOrdre).Select(e=>new PeeDocumentModelView(e)).ToListAsync();
        }

        public async Task<PeeModelView> UpdatePeeAsync( PeeModelView peeModelView )
        {
            Pee pee = new ()
            {
                IdPee = peeModelView.IdPee,
                MatriculeBeneficiaire = peeModelView.MatriculeBeneficiaire,
                IdTuteur = peeModelView.IdTuteur,
                IdResponsableJuridique = peeModelView.IdResponsableJuridique,
                IdEntreprise = peeModelView.IdEntreprise,
                IdOffreFormation = peeModelView.IdOffreFormation,
                IdEtablissement = peeModelView.IdEtablissement,
                EtatPee = peeModelView.EtatPee,
                Remarque = peeModelView.Remarque,
                Etat = peeModelView.Etat,
            };

            if (pee.IsValid)
            {
                if (pee.Etat == EntityPOCOState.Modified)
                {
                    _dbContext.Pees.Update(pee);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return peeModelView;
        }

        public async Task<MessageModelView> GetElementByIdPeeForMessageAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e => e.IdPee == idPee)
                .Include(e=>e.MatriculeBeneficiaireNavigation)
                .ThenInclude(e=>e.CodeTitreCiviliteNavigation)
                .Include(e=>e.IdEntrepriseNavigation)
                .Include(e=>e.PeriodePees)
                .Select(e=> new MessageModelView() { 
                    Remarque = e.Remarque,
                    EtatPee = e.EtatPee,
                    NomBeneficiaire = e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    MailBeneficiaire = e.MatriculeBeneficiaireNavigation.MailBeneficiaire,
                    CodeTitreCiviliteNavigation = new TitreCiviliteModelView(e.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation),
                    RaisonSociale = e.IdEntrepriseNavigation.RaisonSociale,
                    periodes = e.PeriodePees.Select(e=> new PeriodePeeModelView(e)).ToList()
                } ).FirstAsync();
        }

        
    }
}