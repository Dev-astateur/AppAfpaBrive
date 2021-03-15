using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
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
            //BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            Contrats = new HashSet<ContratModelView>();
            //DestinataireEnquetes = new HashSet<DestinataireEnquete>();
            Pees = new HashSet<PeeModelView>();
        }

        public string MatriculeBeneficiaire { get; set; }
        public int CodeTitreCivilite { get; set; }
        public string NomBeneficiaire { get; set; }
        public string PrenomBeneficiaire { get; set; }
        public DateTime? DateNaissanceBeneficiaire { get; set; }
        public string MailBeneficiaire { get; set; }
        public string TelBeneficiaire { get; set; }
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string UserId { get; set; }
        public string IdPays2 { get; set; }
        public string PathPhoto { get; set; }
        public bool? MailingAutorise { get; set; }

        //public virtual TitreCivilite CodeTitreCiviliteNavigation { get; set; }
        //public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual ICollection<ContratModelView> Contrats { get; set; }
        //public virtual ICollection<DestinataireEnquete> DestinataireEnquetes { get; set; }
        public virtual ICollection<PeeModelView> Pees { get; set; }
    }
}
