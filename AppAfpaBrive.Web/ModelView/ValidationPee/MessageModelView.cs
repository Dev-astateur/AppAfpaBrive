using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.ValidationPee
{
    public class MessageModelView
    {
        public string MatriculeCollaborateurAfpa { get; set; }
        public string Message { get; set; }
        
        public string Remarque { get; set; }
        public int EtatPee { get; set; }
        [Display(Name = "Nom du stagiaire.")]

        public string NomBeneficiaire { get; set; }
        [Display(Name = "Prénom du stagiaire.")]
        public string PrenomBeneficiaire { get; set; }
        [Display(Name = "Courriel du bénéficiaire.")]
        public string MailBeneficiaire { get; set; }
        public virtual TitreCiviliteModelView CodeTitreCiviliteNavigation { get; set; }
    }
}
