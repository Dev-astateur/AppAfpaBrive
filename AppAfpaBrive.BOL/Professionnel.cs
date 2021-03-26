using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Professionnel
    {
        
        public Professionnel()
        {
            PeeIdResponsableJuridiqueNavigations = new HashSet<Pee>();
            EntrepriseProfessionnels = new HashSet<EntrepriseProfessionnel>();
            PeeIdTuteurNavigations = new HashSet<Pee>();
        }
        
        public int IdProfessionnel { get; set; }
        public string NomProfessionnel { get; set; }
        public string PrenomProfessionnel { get; set; }

        public int CodeTitreCiviliteProfessionnel { get; set; }
        public virtual TitreCivilite TitreCiviliteNavigation { get; set; }

        public virtual ICollection<Pee> PeeIdResponsableJuridiqueNavigations { get; set; }
        
        public virtual ICollection<Pee> PeeIdTuteurNavigations { get; set; }
        public virtual ICollection<EntrepriseProfessionnel> EntrepriseProfessionnels { get; set; }

    }
}
