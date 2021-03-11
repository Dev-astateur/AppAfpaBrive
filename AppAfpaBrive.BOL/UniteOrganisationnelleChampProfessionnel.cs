using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class UniteOrganisationnelleChampProfessionnel
    {
        public string Uo { get; set; }
        public string IdChampProfessionnel { get; set; }

        public virtual ChampProfessionnnel IdChampProfessionnelNavigation { get; set; }
        public virtual UniteOrganisationnelle UoNavigation { get; set; }
    }
}
