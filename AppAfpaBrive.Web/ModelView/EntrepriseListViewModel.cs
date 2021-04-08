using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class EntrepriseListViewModel
    {
        public EntrepriseListViewModel()
        {
            Contrats = new HashSet<Contrat>();
        }

        public EntrepriseListViewModel(Entreprise entreprise)
        {
            IdEntreprise = entreprise.IdEntreprise;
            RaisonSociale = entreprise.RaisonSociale;
            NumeroSiret = entreprise.NumeroSiret;
            MailEntreprise = entreprise.MailEntreprise;
            TelEntreprise = entreprise.TelEntreprise;
            Ligne1Adresse = entreprise.Ligne1Adresse;
            Ligne2Adresse = entreprise.Ligne2Adresse;
            Ligne3Adresse = entreprise.Ligne3Adresse;
            CodePostal = entreprise.CodePostal;
            Ville = entreprise.Ville;
            Idpays2 = entreprise.Idpays2;
        }
        [Required]
        [DisplayName("Id de l'entreprise: ")]
        public int IdEntreprise { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "La raison sociale est requise")]
        [DisplayName("Raison sociale: ")]
        public string RaisonSociale { get; set; }

        [Required(ErrorMessage = "La raison sociale est requise")]
        [CustomValidator_Siret(ErrorMessage ="Le numéro de Siret est invalide")]
        [DisplayName("Numéro de SIRET: ")]
        public string NumeroSiret { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", ErrorMessage = "Veuillez entrer une addresse e-mail correcte")]
        [DisplayName("E-mail: ")]
        [EmailAddress]
        public string MailEntreprise { get; set; }

        
        [DisplayName("Téléphone: ")]
        public string TelEntreprise { get; set; }

        [Required(ErrorMessage = "L'adresse est requise")]
        [DisplayName("1ére ligne adresse: ")]
        public string Ligne1Adresse { get; set; }


        [DisplayName("2ème ligne adresse: ")]
        public string Ligne2Adresse { get; set; }


        [DisplayName("3ème ligne adresse: ")]
        public string Ligne3Adresse { get; set; }

        [Required(ErrorMessage ="Le code postal est obligatoire")]
        [DisplayName("Code postal: ")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage ="Le nom de la ville est requise")]
        [DisplayName("Ville: ")]
        public string Ville { get; set; }


        [Required(ErrorMessage ="L'id du pays est obligatoire")]
        [DisplayName("Pays: ")]
        public string Idpays2 { get; set; }

        public virtual PaysViewModel Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }

    }
}
