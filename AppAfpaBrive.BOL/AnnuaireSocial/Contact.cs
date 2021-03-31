using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class Contact
    {

        public Contact()
        {
            ContactStructures = new HashSet<ContactStructure>();
            ContactLigneAnnuaires = new HashSet<ContactLigneAnnuaire>();
        }

        [Key]
        public int IdContact { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }

        public int IdTitreCivilite { get; set; }
        public TitreCivilite TitreCivilite { get; set; }

        public virtual ICollection<ContactStructure> ContactStructures { get; set; }
        public virtual ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }
    }
}
