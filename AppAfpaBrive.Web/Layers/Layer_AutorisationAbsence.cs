using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class Layer_AutorisationAbsence
    {
        private readonly AFPANADbContext _db;
        public Layer_AutorisationAbsence(AFPANADbContext context)
        {
            _db = context;
        }

        public string GetNomStagiaireByMatricule(string matricule)
        {
            var nom = _db.Beneficiaires.Where(b => b.MatriculeBeneficiaire == matricule).Select(b => b.NomBeneficiaire).SingleOrDefault();
            return nom;
        }
        public string GetPrenomStagiaireByMatricule(string matricule)
        {
            var prenom = _db.Beneficiaires.Where(b => b.MatriculeBeneficiaire == matricule).Select(b => b.PrenomBeneficiaire).SingleOrDefault();
            return prenom;
        }

        public string GetFormationStagiaireByMatricule(string matricule)
        {
            var formation = _db.BeneficiaireOffreFormations.Where(b => b.MatriculeBeneficiaire == matricule)
                .Select(b => b.Id.LibelleOffreFormation).SingleOrDefault();
            return formation;
        }

    }
}
