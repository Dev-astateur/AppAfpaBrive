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

        public int IdEntreprise { get; set; }
        [Display(Name = "Raison Sociale")]
        public string RaisonSociale { get; set; }
        [Display(Name = "Numéro siret")]
        public string NumeroSiret { get; set; }
        [Display(Name = "Couriel")]
        public string MailEntreprise { get; set; }
        [Display(Name = "Téléphone")]
        public string TelEntreprise { get; set; }
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }
        [Display(Name = "Ville")]
        public string Ville { get; set; }
        public string Idpays2 { get; set; }
        public virtual PaysViewModel Idpays2Navigation { get; set; }
        public virtual ICollection<ContratViewModel> Contrats { get; set; }

    }
}
