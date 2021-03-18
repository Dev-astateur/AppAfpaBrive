using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
{
    public class ProduitFormationLayer
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public ProduitFormationLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique

        public List<OffreFormation> GetProduitFormationStartWith(string codeProduit)
        {
            return _context.OffreFormations.Where(x => x.CodeProduitFormation.ToString().StartsWith(codeProduit)).ToList();
          
        }
        #endregion
    }
}
