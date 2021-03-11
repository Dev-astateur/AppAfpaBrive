using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Rome
    {
        public Rome()
        {
            AppelationRomes = new HashSet<AppelationRome>();
            ProduitFormationAppellationRomes = new HashSet<ProduitFormationAppellationRome>();
        }

        public string CodeRome { get; set; }
        public string IntituleCodeRome { get; set; }
        public string CodeDomaineRome { get; set; }

        public virtual DomaineMetierRome CodeDomaineRomeNavigation { get; set; }
        public virtual ICollection<AppelationRome> AppelationRomes { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRomes { get; set; }
    }
}
