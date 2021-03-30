using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class ContactModelView
    {
        [Key]
        public int IdContact { get; set; }

        [Required(ErrorMessage ="Merci d'entrer un nom de contact")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Merci d'entrer un prénom de contact")]
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Titre de civilité obligatoire")]
        public int IdTitreCivilite { get; set; }

        public TitreCivilite TitreCivilite { get; set; }


    }
}
