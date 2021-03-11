using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class EvenementDocument
    {
        public decimal IdEvent { get; set; }
        public int NumOrdre { get; set; }
        public string PathDocument { get; set; }

        public virtual Evenement IdEventNavigation { get; set; }
    }
}
