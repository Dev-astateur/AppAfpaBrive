using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Pay
    {
        public Pay()
        {
            Entreprises = new HashSet<Entreprise>();
        }

        public string Idpays2 { get; set; }
        public string Idpays3 { get; set; }
        public int IdpaysNum { get; set; }
        public string LibellePays { get; set; }

        public virtual ICollection<Entreprise> Entreprises { get; set; }
    }
}
