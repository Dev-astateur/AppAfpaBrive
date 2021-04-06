using AppAfpaBrive.BOL.AnnuaireSocial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class LigneAnnuaireEtape1ModelView
    {

        public string PublicConcerne { get; set; }

        [Required(ErrorMessage = "Description courte services nécessaire")]
        public string ServiceAbrege { get; set; }

        [Required(ErrorMessage = "Description des services nécessaire")]
        public string Service { get; set; }

        public string Conditions { get; set; }

        [Required(ErrorMessage = "Categorie requise")]
        public int idCategorie { get; set; }

        public ICollection<Categorie> categories {get; set;}
            
        public IEnumerable<CategorieCheckBox> listCategories { get; set; }

        public IEnumerable<SelectListItem> listStructures { get; set; }
        public Structure structure;

    }
}
