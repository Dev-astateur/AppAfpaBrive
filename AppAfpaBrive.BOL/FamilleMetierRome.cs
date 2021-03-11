using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class FamilleMetierRome
    {
        public FamilleMetierRome()
        {
            DomaineMetierRomes = new HashSet<DomaineMetierRome>();
        }

        public string CodeFamilleMetierRome { get; set; }
        public string IntituleFamilleMetierRome { get; set; }

        public virtual ICollection<DomaineMetierRome> DomaineMetierRomes { get; set; }
    }
}
