using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class CategorieLigneAnnuaire
    {
        public CategorieLigneAnnuaire()
        {
            LigneAnnuaires = new HashSet<LigneAnnuaire>();
            Categories = new HashSet<Categorie>();
        }

        public int Id { get; set; }
        public int IdAnnuaire { get; set; }
        public int IdCategorie { get; set; }

        public ICollection<LigneAnnuaire> LigneAnnuaires { get; set; }
        public ICollection<Categorie> Categories { get; set; }
    }
}
