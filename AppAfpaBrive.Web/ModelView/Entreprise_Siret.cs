using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Siret
    {
        [Required(ErrorMessage = "Veuillez saisir le champ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Le Siret doit etre composer uniquement de chiffre")]
        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        [MinLength(14,ErrorMessage ="Le siret est composer de 14 charactère")]
        [MaxLength(14, ErrorMessage = "Le siret est composer de 14 charactère")]
        public string NumeroSiret { get; set; }
       
    }

}
