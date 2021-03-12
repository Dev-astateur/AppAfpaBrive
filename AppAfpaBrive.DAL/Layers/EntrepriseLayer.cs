using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
   public class EntrepriseLayer
    {
        private readonly AFPANADbContext _context;

        public EntrepriseLayer(AFPANADbContext context)
        {
            _context = context;
        }

       public List<Entreprise> GetAllEntreprise()
        {
            return _context.Entreprises.ToList();
        }
    }
}
