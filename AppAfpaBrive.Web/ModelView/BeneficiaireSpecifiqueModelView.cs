using AppAfpaBrive.BOL;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class BeneficiaireSpecifiqueModelView
    {
        public BeneficiaireSpecifiqueModelView()
        {

        }

        public BeneficiaireSpecifiqueModelView(Beneficiaire beneficiaire)
        {
            MailBeneficiaire = beneficiaire.MatriculeBeneficiaire;
            NomBeneficiaire = beneficiaire.NomBeneficiaire;
            PrenomBeneficiaire = beneficiaire.PrenomBeneficiaire;
            MailBeneficiaire = beneficiaire.MailBeneficiaire;
            MailingAutorise = beneficiaire.MailingAutorise;
            
           
        }
        public BeneficiaireSpecifiqueModelView(OffreFormation offre)
        {
            IdOffreFormation = offre.IdOffreFormation;
            MatriculeCollaborateurAfpa = offre.MatriculeCollaborateurAfpa;
            LibelleOffreFormation = offre.LibelleOffreFormation;
            LibelleReduitOffreFormation = offre.LibelleReduitOffreFormation;
            

        }
        [Display(Name = "Matricule du stagiaire")]
        public string MatriculeBeneficiaire { get; set; }
        [Display(Name = "Nom du stagiaire")]
        public string NomBeneficiaire { get; set; }
        [Display(Name = "Prénom du stagiaire")]
        public string PrenomBeneficiaire { get; set; }
        [Display(Name = "Date de naissance")]
       
        public string MailBeneficiaire { get; set; }
     
        public bool? MailingAutorise { get; set; }
        public int IdOffreFormation { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        public virtual PagingList<BeneficiaireSpecifiqueModelView> PagingBeneficiaires { get; set; }
        public virtual IEnumerable<SelectListItem> SelectListItems { get; set; }
    }
}
