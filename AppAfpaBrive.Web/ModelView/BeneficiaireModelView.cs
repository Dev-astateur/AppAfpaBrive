using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class BeneficiaireModelView
    {
        public BeneficiaireModelView(Beneficiaire benef)
        {
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
            MatriculeBeneficiaire = benef.MatriculeBeneficiaire;
            // Contrats = new HashSet<Contrat>();
            // DestinataireEnquetes = new HashSet<DestinataireEnquete>();
            // Pees = new HashSet<Pee>();
            CodeTitreCivilite = benef.CodeTitreCivilite;
            NomBeneficiaire = benef.NomBeneficiaire;
            PrenomBeneficiaire = benef.PrenomBeneficiaire;
            DateNaissanceBeneficiaire = benef.DateNaissanceBeneficiaire;
            MailBeneficiaire = benef.MailBeneficiaire;
            TelBeneficiaire = benef.TelBeneficiaire;
            Ligne1Adresse = benef.Ligne1Adresse;
            Ligne2Adresse = benef.Ligne2Adresse;
            Ligne3Adresse = benef.Ligne3Adresse;
            CodePostal = benef.CodePostal;
            Ville = benef.Ville;
            UserId = benef.UserId;
            IdPays2 = benef.IdPays2;
            PathPhoto = benef.PathPhoto;
            MailingAutorise = benef.MailingAutorise;
            
            
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

       
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        



    }
}
