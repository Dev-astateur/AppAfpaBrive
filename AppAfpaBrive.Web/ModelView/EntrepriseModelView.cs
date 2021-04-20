using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class EntrepriseModelView
    {
        public EntrepriseModelView()
        {
            
        }

        public EntrepriseModelView(Entreprise entreprise)
        {
            if ( entreprise is not null )
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
        }

        public int IdEntreprise { get; set; }

        
        
        [Display(Name = "Raison Sociale")]
        public string RaisonSociale { get; set; }

        [Required(ErrorMessage = "La raison sociale est requise")]
        [StringLength(14,MinimumLength =14, ErrorMessage="Le numéro de SIRET doit etre compsé de 14 chiffres")]
        [Display(Name = "Numéro siret")]
        public string NumeroSiret { get; set; }

        
        [Display(Name = "Couriel")]
        public string MailEntreprise { get; set; }

        [RegularExpression("([0-9]+", ErrorMessage ="Le numéro de téléphone ne doit etre composé que de chiffres")]   
        [Display(Name = "Téléphone")]
        public string TelEntreprise { get; set; }

        [Display(Name = "Batiment")]
        public string Ligne1Adresse { get; set; }
        [Display(Name = "Complément")]
        public string Ligne2Adresse { get; set; }
        [Display(Name = "adresse")]
        public string Ligne3Adresse { get; set; }

        [Required]
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Required]
        [Display(Name = "Ville")]
        public string Ville { get; set; }


        [Required]
        public string Idpays2 { get; set; }
        public virtual PaysViewModel Idpays2Navigation { get; set; }
        public virtual ICollection<ContratModelView> Contrats { get; set; }

    }
}
