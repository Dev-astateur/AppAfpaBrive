using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAfpaBrive.DAL.Layers
{
    public class OffreDeFormationBeneficiareLayer
    {
        private readonly AFPANADbContext _context;

        #region Constructeur
        public OffreDeFormationBeneficiareLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        //public IEnumerable<BOL.BeneficiaireOffreFormation> GetAllByOffredeFormation()
        //{
        //    //return _context.OffreFormations
        //    //    .Where(o => o.MatriculeCollaborateurAfpaNavigation.MatriculeCollaborateurAfpa == "96GB011");
        //    //    .Select(o => o.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == 1)).ToList();

        //}
        #endregion
    }
}
