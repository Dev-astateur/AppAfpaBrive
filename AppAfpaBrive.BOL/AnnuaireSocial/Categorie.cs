using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class Categorie
    {
        [Key]
        public int IdCategorie { get; set; }
        public string LibelleCategorie { get; set; }

        public ICollection<CategorieLigneAnnuaire> CategorieLigneAnnuaires { get; set; }

    }
}
