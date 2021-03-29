using AppAfpaBrive.BOL.AnnuaireSocial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class StructureModelView
    {

        public int IdStructure { get; set; }

        [Required]
        public string NomStructure { get; set; }

        [Required]
        public string LigneAdresse1 { get; set; }

        public string LigneAdresse2 { get; set; }
        public string LigneAdresse3 { get; set; }

        [Required]
        public string CodePostal { get; set; }

        [Required]
        public string Ville { get; set; }

        public string Mail { get; set; }

        public string Telephone { get; set; }


        public ICollection<LigneAnnuaire> LigneAnnuaires { get; set; }
        public ICollection<ContactStructure> ContactStructures { get; set; }


    }
}
