using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.CustomValidator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class EvenementModelView
    {
        public EvenementModelView()
        {
            List<Evenement> liste = new List<Evenement>();
        }
        public int Month { get; set; }
        public int Year { get; set; }
        public ICollection<Evenement> CalendarEvents { get; set; }
        public decimal IdEvent { get; set; }
        public string IdCategorieEvent { get; set; }
        [CustomValidator_Evenement(ErrorMessage="problème")]
        public DateTime DateEvent { get; set; }
        public DateTime? DateEventFin { get; set; }
        public string IdEtablissement { get; set; }
        public string DétailsEvent { get; set; }

        public virtual CategorieEvenement IdCategorieEventNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual ICollection<EvenementDocument> EvenementDocuments { get; set; }
        public string IdCatEvent { get; set; }
        public string LibelleEvent { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le champ")]
        public string Titre { get; set; }

        public virtual ICollection<Evenement> Evenements { get; set; }

        public virtual IEnumerable<SelectListItem> SelectListItems { get; set; }

    }
}
