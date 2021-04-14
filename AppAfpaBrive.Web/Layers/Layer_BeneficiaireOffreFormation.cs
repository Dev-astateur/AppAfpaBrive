using System.Collections.Generic;
using AppAfpaBrive.BOL;
using System.Linq;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_BeneficiaireOffreFormation
    {
        private readonly AFPANADbContext _context;
        public Layer_BeneficiaireOffreFormation(AFPANADbContext context)
        {
            _context = context;
        }

        public IEnumerable<BeneficiaireOffreFormation> GetFormations(string matricule)
        {
            return _context.BeneficiaireOffreFormations.Where(x => x.MatriculeBeneficiaire == matricule).ToList();
        }
        public IEnumerable<BeneficiaireOffreFormation> GetIdetablissemnt(string matricule,int Idformation)
        {
            return _context.BeneficiaireOffreFormations.
                Where(x => x.MatriculeBeneficiaire == matricule && x.IdOffreFormation==Idformation).ToList();
        }

        public string GetIdetablissemnt_Id_Etablissement(string matricule, int Idformation)
        {
            return _context.BeneficiaireOffreFormations.
                Where(x => x.MatriculeBeneficiaire == matricule && x.IdOffreFormation == Idformation).FirstOrDefault().Idetablissement;
        }
        public ICollection<BOL.BeneficiaireOffreFormation> GetAllByOffreFormation(int id)
        {
            return _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).ToList();
        }
        public ICollection<BOL.BeneficiaireOffreFormation> GetAll()
        {
            return _context.BeneficiaireOffreFormations.ToList();
        }

    }
}
