using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
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

        public List<OffreFormation> GetOffreFormationByContains(string query)
        {
            return _context.OffreFormations.Where(x => x.LibelleOffreFormation.Contains(query)).ToList();
        }

        public List<OffreFormation>  GetOffreFormationStartsWith(string query)
        {
            return _context.OffreFormations.Where(x => x.LibelleOffreFormation.StartsWith(query)).ToList();
        }

    }
}
