using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class BeneficiaireNavigationModelView
    {
        [Display(Name ="Matricule")]
        public string MatriculeBeneficiaire { get; set; }
        public string NomBeneficiaire { get; set; }
        public string PrenomBeneficiaire { get; set; }

        public override string ToString()
        {
            return NomBeneficiaire + " " + PrenomBeneficiaire;
        }
    }
}
