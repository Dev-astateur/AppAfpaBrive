using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class OffreFormationLayer
    {
        private readonly AFPANADbContext _context;
        public OffreFormationLayer(AFPANADbContext context)
        {
            _context = context;
        }

        // Methode pour aller chercher toutes les offres de formation
        public List<OffreFormation> GetAllOffreFormation()
        {
            return _context.OffreFormations.ToList();
        }

        public async Task<List<OffreFormation>> GetOffreFormationByContainsAsync(string query)
        {
            return await _context.OffreFormations.Where(x => x.LibelleOffreFormation.Contains(query)).ToListAsync();
        }

        public List<OffreFormation>  GetOffreFormationStartsWith(string query)
        {
            return _context.OffreFormations.Where(x => x.LibelleOffreFormation.StartsWith(query)).ToList();
        }

    }
}
