using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class PaysViewModel
    {
        public PaysViewModel()
        {
            Entreprises = new HashSet<EntrepriseModelView>();
        }

        public string Idpays2 { get; set; }
        public string Idpays3 { get; set; }
        public int IdpaysNum { get; set; }
        [Display(Name = "Pays")]
        public string LibellePays { get; set; }

        public virtual ICollection<EntrepriseModelView> Entreprises { get; set; }
    }
}
