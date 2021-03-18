using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AppAfpaBrive.Web.Layers
{
   public  class BeneficiaireLayer
    {
        private readonly AFPANADbContext _context;
        
        #region Constructeur
        public BeneficiaireLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        public ICollection<BOL.Beneficiaire> GetAllByOffredeFormation(int id)
        {
            return _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).Include(a => a.MatriculeBeneficiaireNavigation)
                 .Select(e => e.MatriculeBeneficiaireNavigation).ToList();
        }
        #endregion 
    }
}
