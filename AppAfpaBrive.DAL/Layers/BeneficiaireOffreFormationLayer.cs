using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
    public class BeneficiaireOffreFormationLayer
    {
        private readonly AFPANADbContext _context;
        #region Constructeur
        public BeneficiaireOffreFormationLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        public ICollection <BOL.BeneficiaireOffreFormation> GetAllByOffreFormation(int id)
        {
            return _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).ToList();
        }
        #endregion
    }

}
