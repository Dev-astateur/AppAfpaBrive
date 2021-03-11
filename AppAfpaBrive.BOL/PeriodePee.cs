using System;
using System.Collections.Generic;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class PeriodePee
    {
        public decimal IdPee { get; set; }
        public int NumOrdre { get; set; }
        public DateTime DateDebutPeriodePee { get; set; }
        public DateTime DateFinPeriodePee { get; set; }

        public virtual Pee IdPeeNavigation { get; set; }
    }
}
