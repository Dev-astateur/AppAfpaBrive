using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class CodeResultatCertification
    {
        public CodeResultatCertification()
        {
            BeneficiaireOffreFormations = new HashSet<BeneficiaireOffreFormation>();
        }

        public string CodeResultatCertification1 { get; set; }
        public string LibelleResultatCertification { get; set; }

        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
    }
}
