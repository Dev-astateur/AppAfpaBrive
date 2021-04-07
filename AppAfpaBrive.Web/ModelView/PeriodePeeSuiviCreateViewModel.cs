using AppAfpaBrive.BOL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class PeriodePeeSuiviCreateViewModel
    {
        public PeriodePeeSuiviCreateViewModel()
        {
            PeeDocuments = new HashSet<PeeDocument>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IdPeriodePeeSuivi { get; set; }
        [ForeignKey("Fk_Pee_Periode_Pee_Suivi")]
        public decimal IdPee { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateDeSuivi { get; set; }
        public string ObjetSuivi { get; set; }
        public string TexteSuivi { get; set; }
        public IFormFile Fichier { get; set; }
        public virtual Pee IdPeeNavigation { get; set; }
        public ICollection<PeeDocument> PeeDocuments { get; set; }
    }
}
