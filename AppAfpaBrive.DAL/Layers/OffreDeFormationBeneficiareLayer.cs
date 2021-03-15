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
        public BOL.OffreFormation GetAllByOffredeFormation()
        {
            return _context.OffreFormations.Where(b => b.MatriculeCollaborateurAfpa == "96GB011").FirstOrDefault();
                
                

        }
        #endregion
    }
}
