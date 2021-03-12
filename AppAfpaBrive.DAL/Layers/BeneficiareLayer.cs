using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AppAfpaBrive.DAL
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
        public IEnumerable<BOL.Beneficiaire> GetAllByOffredeFormation()
        {
          return _context.Beneficiaires.ToList();
            
        }
        #endregion 
    }
}
