using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class ContactStructure
    {
        public ContactStructure ()
        {
            Structures = new HashSet<Structure>();
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public int IdContat { get; set; }
        public int IdStructure { get; set; }

        public ICollection<Structure> Structures { get; set; }
        public ICollection<Contact> Contacts { get; set; }


    }
}
