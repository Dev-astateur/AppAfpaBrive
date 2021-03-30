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
        [Key]
        public int IdStructure { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom de la structure est requis")]
        public string NomStructure { get; set; }

        public string LigneAdresse1 { get; set; }

        public string LigneAdresse2 { get; set; }
        public string LigneAdresse3 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le code postal est requis")]
        public string CodePostal { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La ville est requise")]
        public string Ville { get; set; }

        public string Mail { get; set; }

        public string Telephone { get; set; }


        public ICollection<LigneAnnuaire> LigneAnnuaires { get; set; }
        public ICollection<ContactStructure> ContactStructures { get; set; }

        public Structure GetStructure()
        {
            return new Structure
            {
                IdStructure = this.IdStructure,
                NomStructure = this.NomStructure,
                LigneAdresse1 = this.LigneAdresse1,
                LigneAdresse2 = this.LigneAdresse2,
                LigneAdresse3 = this.LigneAdresse3,
                CodePostal = this.CodePostal,
                Ville = this.Ville,
                Mail = this.Mail,
                Telephone = this.Telephone
            };
        }
    }
}
