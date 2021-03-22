using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using AppAfpaBrive.Web.ModelView;

namespace AppAfpaBrive.Web.Layers
{
    public class PeeLayer
    {
        private readonly AFPANADbContext _dbContext = null;

        public PeeLayer (AFPANADbContext context) 
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ListePeeAValiderModelView>> GetPeeByMatriculeCollaborateurAfpaAsync(string idMatricule)
        {
            return await _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.EtatPee == 0)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation)
                .Include(e => e.MatriculeBeneficiaireNavigation).
                Select(e=> new ListePeeAValiderModelView () { 
                    NomBeneficiaire=e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    RaisonSociale = e.IdEntrepriseNavigation.RaisonSociale,
                    IdPee = e.IdPee
                } ).ToListAsync();
        }

        public async Task<PeeEntrepriseModelView> GetPeeByIdPeeOffreEntreprisePaysAsync(int idPee)
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

        public async Task<PeeModelView> GetPeeByIdAsync(int idPee)
        {
            return new PeeModelView(await _dbContext.Pees.FindAsync((decimal)idPee));
        }

        public async Task<ICollection<PeeDocumentModelView>> GetPeeDocumentByIdAsync(int idPee)
        {
            return await _dbContext.PeeDocuments.Where(e => e.IdPee == idPee).Select(e=>new PeeDocumentModelView(e)).ToListAsync();
        }
    }
}
