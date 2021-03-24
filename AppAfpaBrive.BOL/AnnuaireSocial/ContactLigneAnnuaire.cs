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
        public Contact Contact { get; set; }

        public int IdAnnuaire { get; set; }
        public LigneAnnuaire LigneAnnuaire { get; set; }
    }
}
