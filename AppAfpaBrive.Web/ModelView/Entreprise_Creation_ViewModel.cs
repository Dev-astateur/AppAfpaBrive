using AppAfpaBrive.BOL;
using System.ComponentModel.DataAnnotations;
using AppAfpaBrive.Web.CustomValidator;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Creation_ViewModel
    {
        
        public int IdEntreprise { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        [Display(Name = "Raison sociale")]
        public string RaisonSociale { get; set; }

        [CustomValidator_Siret(ErrorMessage = "Siret invalide")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string NumeroSiret { get; set; }
        public string MailEntreprise { get; set; }
        public string TelEntreprise { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Le code postal doit etre composer uniquement de chiffre")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Veuillez saisir le champ")]
        [MinLength(5,ErrorMessage ="Le code postal à 5 chiffres")]
        [MaxLength(5,ErrorMessage = "Le code postal à 5 chiffres")]
        public string CodePostal { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Ville { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Idpays2 { get; set; }

        public virtual Pays Idpays2Navigation { get; set; }

    }
}
