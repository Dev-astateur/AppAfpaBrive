using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class ChampProfessionnnel
    {
        public ChampProfessionnnel()
        {
            UniteOrganisationnelleChampProfessionnels = new HashSet<UniteOrganisationnelleChampProfessionnel>();
        }

        public string IdChampProfessionnel { get; set; }
        public string LibelleChampProfessionnel { get; set; }

        public virtual ICollection<UniteOrganisationnelleChampProfessionnel> UniteOrganisationnelleChampProfessionnels { get; set; }
    }
}
