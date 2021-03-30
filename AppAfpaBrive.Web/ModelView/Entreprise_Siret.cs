using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Siret
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        [MinLength(14,ErrorMessage ="Le siret est composer de 14 charactère")]
        [MaxLength(14, ErrorMessage = "Le siret est composer de 14 charactère")]
        public string NumeroSiret { get; set; }
       
    }

}
