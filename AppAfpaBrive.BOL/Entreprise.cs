using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Entreprise
    {
        public Entreprise()
        {
            Contrats = new HashSet<Contrat>();
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
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Idpays2 { get; set; }

        public virtual Pay Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }
    }
}
