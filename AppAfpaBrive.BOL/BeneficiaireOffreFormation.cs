using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class BeneficiaireOffreFormation :EntityBase
    {
        public string MatriculeBeneficiaire { get; set; }
        public int IdOffreFormation { get; set; }
        public string Idetablissement { get; set; }
        public DateTime DateEntreeBeneficiaire { get; set; }
        public DateTime? DateSortieBeneficiaire { get; set; }
        public string Certifie { get; set; }
        public bool? Consultable { get; set; }
        public bool? Delegue { get; set; }
        public bool? Suppleant { get; set; }

        public virtual CodeResultatCertification CertifieNavigation { get; set; }
        public virtual OffreFormation Id { get; set; }
        public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
    }
}
