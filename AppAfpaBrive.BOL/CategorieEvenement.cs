using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class CategorieEvenement
    {
        public CategorieEvenement()
        {
            Evenements = new HashSet<Evenement>();
        }

        public string IdCatEvent { get; set; }
        public string LibelleEvent { get; set; }

        public virtual ICollection<Evenement> Evenements { get; set; }
    }
}
