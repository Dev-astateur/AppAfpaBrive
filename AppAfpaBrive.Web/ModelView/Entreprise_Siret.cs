using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Siret
    {
        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string NumeroSiret { get; set; }
       
    }

}
