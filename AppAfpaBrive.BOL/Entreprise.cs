using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Entreprise
    {
        public Entreprise()
        {
            Contrats = new HashSet<Contrat>();
            pees = new HashSet<Pee>();
        }

        public int IdEntreprise { get; set; }
        public string RaisonSociale { get; set; }
        public string NumeroSiret { get; set; }
        public string MailEntreprise { get; set; }
        public string TelEntreprise { get; set; }
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Idpays2 { get; set; }

        public virtual Pay Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }
        public virtual ICollection<Pee> pees { get; set; }
    }
}
