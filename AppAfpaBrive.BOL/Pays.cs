using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class Pays
    {
        public Pays()
        {
            Entreprises = new HashSet<Entreprise>();
            Beneficiaires = new HashSet<Beneficiaire>();
        }

        public string Idpays2 { get; set; }
        public string Idpays3 { get; set; }
        public int IdpaysNum { get; set; }
        public string LibellePays { get; set; }

        public virtual ICollection<Entreprise> Entreprises { get; set; }
        public virtual ICollection<Beneficiaire> Beneficiaires { get; set; }
    }
}
