using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public partial class PeriodePeeModelView
    {
        public decimal IdPee { get; set; }
        public int NumOrdre { get; set; }
        public DateTime DateDebutPeriodePee { get; set; }
        public DateTime DateFinPeriodePee { get; set; }

        public virtual PeeModelView IdPeeNavigation { get; set; }

        public PeriodePeeModelView ( PeriodePee periodePee )
        {
            IdPee = periodePee.IdPee;
            NumOrdre = periodePee.NumOrdre;
            DateDebutPeriodePee = periodePee.DateDebutPeriodePee;
            DateFinPeriodePee = periodePee.DateFinPeriodePee;

            if ( periodePee.IdPeeNavigation is not null )
            {
                IdPeeNavigation = new PeeModelView( periodePee.IdPeeNavigation);
            }
        }
    }
}
