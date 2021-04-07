using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Siret 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Le siret doit être composé uniquement de chiffres")]
        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        [MinLength(14, ErrorMessage = "Le siret est composé de 14 charactères")]
        [MaxLength(14, ErrorMessage = "Le siret est composeé de 14 charactères")]
        public string NumeroSiret { get; set; }

    }

}
