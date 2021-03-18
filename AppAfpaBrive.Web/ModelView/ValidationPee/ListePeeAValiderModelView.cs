using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.ValidationPee
{
    public class ListePeeAValiderModelView
    {
        [Display(Name = "Nom du bénificiaire")]
        public string NomBeneficiaire { get; set; }
        public string PrenomBeneficiaire { get; set; }
        public string RaisonSociale { get; set; }
        public decimal IdPee { get; set; }

        public ListePeeAValiderModelView(){}

    }
}
