using AppAfpaBrive.BOL;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class Entreprise_Creation_ViewModel
    {
        
        public int IdEntreprise { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        [Display(Name = "Raison sociale")]
        public string RaisonSociale { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string NumeroSiret { get; set; }
        public string MailEntreprise { get; set; }
        public string TelEntreprise { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Veuillez saisir le champ")]
        [Range(5,5,ErrorMessage ="entrer un code postal à 5 chiffre")]
        public string CodePostal { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Ville { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Idpays2 { get; set; }

        public virtual Pays Idpays2Navigation { get; set; }

    }
}
