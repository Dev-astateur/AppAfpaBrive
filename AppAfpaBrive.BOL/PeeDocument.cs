using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL
{
    public partial class PeeDocument
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IdPeeDocument { get; set; }
        [ForeignKey("Fk_PeeDocument_Periode_Pee_Suivi")]
        public decimal IdPeriodePeeSuivi { get; set; }
        [ForeignKey("Fk_PeeDocument_Pee")]
        public decimal IdPee { get; set; }
        public int NumOrdre { get; set; }
        public string PathDocument { get; set; }
        public virtual PeriodePeeSuivi IdPeriodePeeSuiviNavigation { get; set; }
        public virtual Pee IdPeeNavigation { get; set; }
    }
}
