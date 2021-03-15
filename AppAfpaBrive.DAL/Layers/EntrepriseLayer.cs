using AppAfpaBrive.BOL;
using Microsoft.EntityFrameworkCore;
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
        public List<Entreprise> GetEntreprisesByDepartement(string dep)
        {
            return _context.Entreprises.Where(e => e.CodePostal.StartsWith(dep)).ToList();
        }
        public List<Entreprise> GetEntrepriseByProduitFormation(string offre)
        {
            var query = _context.OffreFormations.GroupJoin()
                                                
                                                
            var x = _context.OffreFormations.Select(x => x.Pees.
        }


    }
}
