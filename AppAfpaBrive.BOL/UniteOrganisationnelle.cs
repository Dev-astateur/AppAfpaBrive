using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class UniteOrganisationnelle
    {
        public UniteOrganisationnelle()
        {
            CollaborateurAfpas = new HashSet<CollaborateurAfpa>();
            UniteOrganisationnelleChampProfessionnels = new HashSet<UniteOrganisationnelleChampProfessionnel>();
        }

        public string Uo { get; set; }
        public string LibelleUo { get; set; }

        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpas { get; set; }
        public virtual ICollection<UniteOrganisationnelleChampProfessionnel> UniteOrganisationnelleChampProfessionnels { get; set; }
    }
}
