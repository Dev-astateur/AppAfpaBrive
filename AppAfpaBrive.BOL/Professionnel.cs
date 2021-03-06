using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Professionnel
    {
        public Professionnel()
        {
            PeeIdResponsableJuridiqueNavigations = new HashSet<Pee>();
            PeeIdTuteurNavigations = new HashSet<Pee>();
        }

        public int IdProfessionnel { get; set; }
        public string NomProfessionnel { get; set; }
        public string PrenomProfessionnel { get; set; }
        public int CodeTitreCiviliteProfessionnel { get; set; }

        public virtual ICollection<Pee> PeeIdResponsableJuridiqueNavigations { get; set; }
        public virtual ICollection<Pee> PeeIdTuteurNavigations { get; set; }
    }
}
