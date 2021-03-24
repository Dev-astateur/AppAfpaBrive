using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class CategorieLigneAnnuaire
    {
        
        public int IdLigneAnnuaire { get; set; }
        public LigneAnnuaire LigneAnnuaire { get; set; }

        public int IdCategorie { get; set; }
        public Categorie Categorie { get; set; }



    }
}
