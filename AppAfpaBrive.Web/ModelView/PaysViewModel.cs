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

        public PaysViewModel( Pays pays )
        {
            Entreprises = new HashSet<EntrepriseModelView>();
            if ( pays is not null )
            {
                Idpays2 = pays.Idpays2;
                Idpays3 = pays.Idpays3;
                IdpaysNum = pays.IdpaysNum;
                LibellePays = pays.LibellePays;
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
