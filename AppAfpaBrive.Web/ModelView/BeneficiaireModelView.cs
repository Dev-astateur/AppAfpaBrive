using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class BeneficiaireModelView
    {
        public BeneficiaireModelView()
        {
            //BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            Contrats = new HashSet<ContratModelView>();
            //DestinataireEnquetes = new HashSet<DestinataireEnquete>();
            Pees = new HashSet<PeeModelView>();
        }
            
        public BeneficiaireModelView( Beneficiaire beneficiaire  )
        { 
            MatriculeBeneficiaire = beneficiaire.MatriculeBeneficiaire;
            CodeTitreCivilite = beneficiaire.CodeTitreCivilite;
            NomBeneficiaire = beneficiaire.NomBeneficiaire;
            PrenomBeneficiaire = beneficiaire.PrenomBeneficiaire;
            DateNaissanceBeneficiaire = beneficiaire.DateNaissanceBeneficiaire;
            MailBeneficiaire = beneficiaire.MailBeneficiaire;
            TelBeneficiaire = beneficiaire.TelBeneficiaire;
            Ligne1Adresse = beneficiaire.Ligne1Adresse;
            Ligne2Adresse = beneficiaire.Ligne2Adresse;
            Ligne3Adresse = beneficiaire.Ligne3Adresse;
            CodePostal = beneficiaire.CodePostal;
            Ville = beneficiaire.Ville;
            UserId = beneficiaire.UserId;
            IdPays2 = beneficiaire.IdPays2;
            PathPhoto = beneficiaire.PathPhoto;
            MailingAutorise = beneficiaire.MailingAutorise;
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            Contrats = new HashSet<ContratModelView>();
            //DestinataireEnquetes = new HashSet<DestinataireEnquete>();
            Pees = new HashSet<PeeModelView>();
        }

        [Display(Name ="Matricule du stagiaire")]
        public string MatriculeBeneficiaire { get; set; }


        public int? CodeTitreCivilite { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        public string NomBeneficiaire { get; set; }

        
        [Display(Name ="Prénom du stagiaire")]
        [Required(ErrorMessage = "Veuillez saisir un prénom")]
        public string PrenomBeneficiaire { get; set; }
        

        [Required]
        [Display(Name ="Date de naissance")]
        public DateTime? DateNaissanceBeneficiaire { get; set; }

        [Required]
        [RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b")]
        public string MailBeneficiaire { get; set; }

        [Required]
        [RegularExpression(@"(?:(?:\+|00)33|0)\s*[1-9](?:[\s.-]*\d{2}){4}")]
        public string TelBeneficiaire { get; set; }

        [Required]
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Doit être un caractère numérique")]
        public string CodePostal { get; set; }

        [Required]
        public string Ville { get; set; }
        public string UserId { get; set; }
        public string IdPays2 { get; set; }
        public string PathPhoto { get; set; }
        public bool? MailingAutorise { get; set; }

        //public virtual TitreCivilite CodeTitreCiviliteNavigation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual ICollection<ContratModelView> Contrats { get; set; }
        //public virtual ICollection<DestinataireEnquete> DestinataireEnquetes { get; set; }
        public virtual ICollection<PeeModelView> Pees { get; set; }

        public virtual ICollection<Pays> Pays { get; set; }
    }
}
