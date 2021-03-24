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

        public PeeLayer(AFPANADbContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Pee> GetPeeByMatriculeCollaborateurAfpa(string idMatricule)
        {
            return _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.Etat == 0)
                .Include(e => e.Id)
                .Include(e => e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation);
        }

        public Pee GetPeeByIdPee(int idPee)
        {
            return _dbContext.Pees.Where(e => e.IdPee == idPee)
                .Include(e => e.Id)
                .Include(e => e.IdEntrepriseNavigation).ThenInclude(e => e.Idpays2Navigation)
                .FirstOrDefault();
        }

        public void Pee_Create(Pee pee)
        {
            _dbContext.Pees.Add(pee);
            _dbContext.SaveChanges();
        }

        public decimal GetPeeBy_Idmatricule_idFormation_idetablissemnt(string matricule, int identreprise, string idetablissement)
        {
            return _dbContext.Pees.Where(e => e.MatriculeBeneficiaire == matricule && e.IdEntreprise == identreprise && e.IdEtablissement == idetablissement)
                .Select(x => x.IdPee)
                .FirstOrDefault();
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

        public async Task<PagingList<ListePeeAValiderModelView>> GetPeeByMatriculeCollaborateurAfpaAsync(string idMatricule,int page = 1)
        {
            var qry = _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.EtatPee == 0)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation).
                Select(e=> new ListePeeAValiderModelView () { 
                    NomBeneficiaire=e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    RaisonSociale = e.IdEntrepriseNavigation.RaisonSociale,
                    IdPee = e.IdPee
                } ).AsQueryable();
            Task<PagingList<ListePeeAValiderModelView>> model = PagingList.CreateAsync((IOrderedQueryable<ListePeeAValiderModelView>)qry, 10 , page);
            model.Result.Action = "ListePeeAValider";
            return await model;
        }

        public async Task<PeeEntrepriseModelView> GetPeeByIdPeeOffreEntreprisePaysAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e=>e.IdPee==idPee)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation).ThenInclude(e=>e.Idpays2Navigation)
                .Select(e=>new PeeEntrepriseModelView() 
                { 
                    IdPee = e.IdPee, 
                    MatriculeCollaborateurAfpa= e.Id.MatriculeCollaborateurAfpa, 
                    IdEntrepriseNavigation=new EntrepriseModelView(e.IdEntrepriseNavigation) 
                } )
                .FirstOrDefaultAsync();
        }

        public async Task<PeeModelView> GetPeeByIdAsync(decimal idPee)
        {
            return new PeeModelView(await _dbContext.Pees.FindAsync((decimal)idPee));
        }

        public async Task<string> GetPeeMatriculeFormateurByIdAsync(int idPee)
        {
            return await _dbContext.Pees.Where(e=>e.IdPee == idPee)
                .Include(e=>e.Id).Select(e=>e.Id.MatriculeCollaborateurAfpa).FirstAsync();
        }

        public async Task<ICollection<PeeDocumentModelView>> GetPeeDocumentByIdAsync(decimal idPee)
        {
            return await _dbContext.PeeDocuments.Where(e => e.IdPee == idPee).Select(e=>new PeeDocumentModelView(e)).ToListAsync();
        }

        public async Task<bool> UpdatePeeAsync( PeeModelView peeModelView )
        {
            Pee pee = new Pee()
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

            if (  pee.IsValid )
            {
                if (pee.Etat == EntityPOCOState.Modified)
                {
                    _dbContext.Update(pee);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<MessageModelView> GetElementByIdPeeForMessageAsync(decimal idPee)
        {
            return await _dbContext.Pees.Where(e => e.IdPee == idPee)
                .Include(e=>e.MatriculeBeneficiaireNavigation)
                .ThenInclude(e=>e.CodeTitreCiviliteNavigation)
                .Select(e=> new MessageModelView() { 
                    Remarque = e.Remarque,
                    EtatPee = e.EtatPee,
                    NomBeneficiaire = e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    MailBeneficiaire = e.MatriculeBeneficiaireNavigation.MailBeneficiaire,
                    CodeTitreCiviliteNavigation = new TitreCiviliteModelView(e.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation)
                } ).FirstAsync();
        }
    }
}
