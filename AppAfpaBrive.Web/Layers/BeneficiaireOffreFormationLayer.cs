using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.Web.Layers
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
        public ICollection<BOL.BeneficiaireOffreFormation> GetAll()
        {
            return _context.BeneficiaireOffreFormations.ToList();
        }
        #endregion
    }

}
