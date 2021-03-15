using AppAfpaBrive.BOL;
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

        public PaysViewModel( Pay pay )
        {
            Entreprises = new HashSet<EntrepriseModelView>();
            if ( pay is not null )
            {
                Idpays2 = pay.Idpays2;
                Idpays3 = pay.Idpays3;
                IdpaysNum = pay.IdpaysNum;
                LibellePays = pay.LibellePays;
            }
        }

        public string Idpays2 { get; set; }
        public string Idpays3 { get; set; }
        public int IdpaysNum { get; set; }
        [Display(Name = "Pays")]
        public string LibellePays { get; set; }

        public virtual ICollection<EntrepriseModelView> Entreprises { get; set; }
    }
}
