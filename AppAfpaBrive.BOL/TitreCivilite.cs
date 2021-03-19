using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class TitreCivilite
    {
        public TitreCivilite()
        {
            Beneficiaires = new HashSet<Beneficiaire>();
            CollaborateurAfpas = new HashSet<CollaborateurAfpa>();
            Professionnels = new HashSet<Professionnel>();
        }

        public int CodeTitreCivilite { get; set; }
        public string TitreCiviliteAbrege { get; set; }
        public string TitreCiviliteComplet { get; set; }

        public virtual ICollection<Beneficiaire> Beneficiaires { get; set; }
        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpas { get; set; }
        public virtual ICollection<Professionnel> Professionnels { get; set; }
    }
}
