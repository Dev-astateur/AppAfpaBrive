using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Utilitaires;

namespace AppAfpaBrive.Web.ModelView.ValidationPee
{
    public class MessageModelView
    {
        public string MatriculeCollaborateurAfpa { get; set; }
        public string Remarque { get; set; }
        public int EtatPee { get; set; }

        [Display(Name = "Nom du stagiaire :")]
        public string NomBeneficiaire { get; set; }
        [Display(Name = "Prénom du stagiaire :")]
        public string PrenomBeneficiaire { get; set; }
        [Display(Name = "Courriel du bénéficiaire :")]
        public string MailBeneficiaire { get; set; }
        [Display(Name = "Raison sociale de l'entreprise :")]
        public string RaisonSociale { get; set; }

        [Display(Name = "Message envoyé au bénéficiaire dans son courriel :")]
        public string Message { 
            get{
                return GetMessageCourriel();
            }
        }

        public MessagePee MessagePee { get; set; }

        public virtual ICollection<PeriodePeeModelView> periodes { get; set; }
        public virtual TitreCiviliteModelView CodeTitreCiviliteNavigation { get; set; }

        private string GetMessageCourriel()
        {
            string message = MessagePee.GetText(EtatPee);
            message += "pour l'entreprise " + RaisonSociale + ".";

            message += string.IsNullOrWhiteSpace(Remarque) ? "" : Environment.NewLine + Environment.NewLine + "Voici le message qui est spécifié: ";
            message += Environment.NewLine + "\"" + Remarque + "\"" + Environment.NewLine;
            message += Environment.NewLine + "Veuillez ne pas répondre à ce mail qui est envoyé automatiquement.";
            message += Environment.NewLine + "Si vous avez des questions, veuillez contacter votre formateur.";
            return message;
        }
    }
}
