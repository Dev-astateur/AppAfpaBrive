using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AppAfpaBrive.BOL
{
    public partial class PeriodePeeSuivi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IdPeriodePeeSuivi { get; set; }
        //[ForeignKey("Fk_Pee_Periode_Pee_Suivi")]
        public decimal IdPee { get; set; }
        public string ObjetSuivi { get; set; }
        public string TexteSuivi {get; set;}
        [DataType(DataType.Date)]
        public DateTime DateDeSuivi { get; set ; }
        public virtual Pee IdPeeNavigation { get; set; }
        public ICollection<PeeDocument> PeeDocuments { get; set; }
        
    }
}
