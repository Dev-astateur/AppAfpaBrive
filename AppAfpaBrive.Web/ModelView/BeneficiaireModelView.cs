using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class BeneficiaireModelView : ModelViewBase
    {
        public BeneficiaireModelView()
        {
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            Contrats = new HashSet<ContratModelView>();
            //DestinataireEnquetes = new HashSet<DestinataireEnquete>();
            Pees = new HashSet<PeeModelView>();    
        }
            
        public BeneficiaireModelView( Beneficiaire beneficiaire  )
        {
            if ( beneficiaire is not null )
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
            
        }
        [Display(Name ="Matricule du stagiaire")]
        public string MatriculeBeneficiaire { get; set; }

        public int? CodeTitreCivilite { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un nom")]
        public string NomBeneficiaire { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un prénom")]
        public string PrenomBeneficiaire { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateNaissanceBeneficiaire { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage ="Veuillez saisir une adresse email valide")]
        public string MailBeneficiaire { get; set; }

        [Required(ErrorMessage ="Veuillez saisir un numéro de téléphone")]
        [RegularExpression(@"(?:(?:\+|00)33|0)\s*[1-9](?:[\s.-]*\d{2}){4}", ErrorMessage ="Veuillez saisir un numéro de téléphone valide")]
        public string TelBeneficiaire { get; set; }
        [Display(Name ="Batiment")]
        public string Ligne1Adresse { get; set; }
        [Display(Name = "Complement")]
        public string Ligne2Adresse { get; set; }
        [Display(Name = "Adresse")]
        public string Ligne3Adresse { get; set; }

        [Required(ErrorMessage ="Veuillez saisir un code postal")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Code postal invalide")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Veuillez séléctionner une ville")]
        public string Ville { get; set; }
        public string UserId { get; set; }
        public string IdPays2 { get; set; }
        public string PathPhoto { get; set; }
        public bool? MailingAutorise { get; set; }

        public virtual TitreCiviliteModelView CodeTitreCiviliteNavigation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual ICollection<ContratModelView> Contrats { get; set; }
        //public virtual ICollection<DestinataireEnquete> DestinataireEnquetes { get; set; }
        public virtual ICollection<PeeModelView> Pees { get; set; }

        public virtual ICollection<Pays> Pays { get; set; }
    }
}
