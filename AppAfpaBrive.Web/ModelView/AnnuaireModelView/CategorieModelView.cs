using AppAfpaBrive.BOL.AnnuaireSocial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class CategorieModelView
    {
        [Key]
        public int IdCategorie { get; set; }
        public string LibelleCategorie { get; set; }

        public Categorie GetCategorie()
        {
            return new Categorie
            {
                IdCategorie = this.IdCategorie,
                LibelleCategorie = this.LibelleCategorie
            };
        }

    }
}
