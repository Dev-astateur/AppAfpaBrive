using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class ContactStructure
    {
       
        public int IdContact { get; set; }
        public Contact Contact { get; set; }


        public int IdStructure { get; set; }
        public Structure Structure { get; set; }


    }
}
