using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.BOL.AnnuaireSocial
{
    public partial class Structure
    {

        public Structure()
        {
            LigneAnnuaires = new HashSet<LigneAnnuaire>();
            ContactStructures = new HashSet<ContactStructure>();
        }

        [Key]
        public int IdStructure { get; set; }

        public string NomStructure {get; set;}
        public string LigneAdresse1 { get; set; }
        public string LigneAdresse2 { get; set; }
        public string LigneAdresse3 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
  
        public ICollection<LigneAnnuaire> LigneAnnuaires { get; set; }
        public ICollection<ContactStructure> ContactStructures { get; set; }


    }
}
