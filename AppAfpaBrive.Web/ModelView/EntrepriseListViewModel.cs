using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
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

        public int IdEntreprise { get; set; }
        [Display(Name="Raison sociale")]
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

        public virtual Pays Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrats { get; set; }

    }
}
