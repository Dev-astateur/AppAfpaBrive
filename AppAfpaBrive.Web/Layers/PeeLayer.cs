using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.Web.ModelView.ValidationPee;

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
            return await _dbContext.Pees.Where(e => e.Id.MatriculeCollaborateurAfpa == idMatricule && e.Etat == 0)
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

        public async Task<object> GetPeeByIdPeeOffreEntreprisePaysAsync(int idPee)
        {
            return await _dbContext.Pees.Where(e=>e.IdPee==idPee)
                .Include(e => e.Id)
                .Include(e=>e.IdEntrepriseNavigation).ThenInclude(e=>e.Idpays2Navigation)
                .Select(e=>new { IdPee = e.IdPee, MatriculeCollaborateurAfpa= e.Id.MatriculeCollaborateurAfpa, IdEntrepriseNavigation=e.IdEntrepriseNavigation } )
                .FirstOrDefaultAsync();
        }

        public async Task<Pee> GetPeeByIdAsync(int idPee)
        {
            return await _dbContext.Pees.FindAsync((decimal)idPee);
        }
    }
}
