using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;

namespace AppAfpaBrive.DAL.Layer
{
    public class Layer_Offres_Formation
    {
        private readonly AFPANADbContext _db;
        public Layer_Offres_Formation(AFPANADbContext context)
        {
            _db = context;
        }

        public IEnumerable<BeneficiaireOffreFormation> GetFormations(string matricule)
        {
            return _db.BeneficiaireOffreFormations.Where(x => x.MatriculeBeneficiaire == matricule).ToList();
        }
        public IEnumerable<BeneficiaireOffreFormation> GetIdetablissemnt(string matricule,int Idformation)
        {
            return _db.BeneficiaireOffreFormations.
                Where(x => x.MatriculeBeneficiaire == matricule && x.IdOffreFormation==Idformation).ToList();
        }

    }
}
