using AppAfpaBrive.BOL;
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
            Idpays2Navigation = new PaysViewModel(entreprise.Idpays2Navigation);
        }
        [Required]
        [DisplayName("Id de l'entreprise")]
        public int IdEntreprise { get; set; }

        [Required(ErrorMessage ="Entrez un nom d'entreprise")]
        [DisplayName("Raison sociale")]
        public string RaisonSociale { get; set; }

        [Required]
        [StringLengthAttribute(14, ErrorMessage ="Un numéro de SIRET est composé de 14 chiffres")]
        [DisplayName("Numéro de SIRET")]
        public string NumeroSiret { get; set; }
        [DisplayName("E-mail")]
        public string MailEntreprise { get; set; }
        [DisplayName("Téléphone")]
        public string TelEntreprise { get; set; }
        [DisplayName("1ére ligne adresse")]
        public string Ligne1Adresse { get; set; }
        [DisplayName("2ème ligne adresse")]
        public string Ligne2Adresse { get; set; }
        [DisplayName("3ème ligne adresse")]
        public string Ligne3Adresse { get; set; }
        [DisplayName("Code postal")]
        public string CodePostal { get; set; }
        [DisplayName("Ville")]
        public string Ville { get; set; }


        [Required]
        [DisplayName("Pays")]
        public string Idpays2 { get; set; }

        public virtual PaysViewModel Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }

    }
}
