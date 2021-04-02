using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class LigneAnnuaireEtape1ModelView
    {
        [Required(ErrorMessage = "Public concerné nécessaire")]
        public string PublicConcerne { get; set; }

        [Required(ErrorMessage = "Description courte services nécessaire")]
        public string ServiceAbrege { get; set; }

        [Required(ErrorMessage = "Description des services nécessaire")]
        public string Service { get; set; }

        public string Conditions { get; set; }

    }
}
