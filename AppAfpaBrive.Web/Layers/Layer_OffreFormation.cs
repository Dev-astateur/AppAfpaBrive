using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_OffreFormation
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public Layer_OffreFormation(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        public BOL.OffreFormation GetByMatriculeCollaborateurAFPA(string idCollaborateurAFPA)
        {
            return _context.OffreFormations.Where(a => a.MatriculeCollaborateurAfpa == idCollaborateurAFPA)
                .Include(e => e.BeneficiaireOffreFormations).ThenInclude(a => a.MatriculeBeneficiaireNavigation)
               .FirstOrDefault();
            // return _context.OffreFormations.Where(b => b.MatriculeCollaborateurAfpa == idCollaborateurAFPA).FirstOrDefault();

        }
        public IEnumerable<SelectListItem> GetAllbyMatricule(string idCollaborateurAFPA)
        {
            return _context.OffreFormations.Where(a => a.MatriculeCollaborateurAfpa == idCollaborateurAFPA)
                .Select(e => new SelectListItem() { Text = e.LibelleOffreFormation, Value = e.IdOffreFormation.ToString()})
                .ToList();
               
        }

        public List<OffreFormation> GetAllOffreFormation()
        {
            return _context.OffreFormations.ToList();
        }

        public async Task<List<OffreFormation>> GetOffreFormationByContainsAsync(string query)
        {
            return await _context.OffreFormations.Where(x => x.LibelleOffreFormation.Contains(query)).ToListAsync();
        }

        public List<OffreFormation> GetOffreFormationStartsWith(string query)
        {
            return _context.OffreFormations.Where(x => x.LibelleOffreFormation.StartsWith(query)).ToList();
        }
        #endregion
    }
}
