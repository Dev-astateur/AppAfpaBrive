using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class ContactLigneAnnuaire
    {
        public int IdContact { get; set; }
        public virtual Contact Contact { get; set; }

        public int IdLigneAnnuaire { get; set; }
        public virtual LigneAnnuaire LigneAnnuaire { get; set; }
    }
}
