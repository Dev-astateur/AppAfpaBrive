using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
{
    public class ProduitDeFormationLayer
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public ProduitDeFormationLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique

        public List<ProduitFormation> GetProduitFormationStartWith(string codeProduit)
        {
            return _context.ProduitFormations.Where(x => x.CodeProduitFormation.ToString().StartsWith(codeProduit)).ToList();
          
        }


        #endregion
    }
}
