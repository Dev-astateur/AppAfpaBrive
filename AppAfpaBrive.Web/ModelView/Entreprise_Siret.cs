using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Siret 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        public string NumeroSiret { get; set; }

    }

}
