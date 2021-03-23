using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL
{
    public partial class PeeDocument
    {
        
        public decimal IdPee { get; set; }
        
        public int NumOrdre { get; set; }
        public string PathDocument { get; set; }

        public virtual Pee idPeeNavigation { get; set; }
    }
}
