using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.Navigation
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
