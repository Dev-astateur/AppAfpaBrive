using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace AppAfpaBrive.Web.ModelView
{
    public class OffreDeFormation
    {
        [Required]
        [Display(Name ="Libelle offre de formation")]
        public int IdOffreFormation { get; set; }
        public IEnumerable<SelectListItem> LibelleOffreFormation { get; set; }
    }
}
