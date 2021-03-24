using System;
using System.Collections.Generic;
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


        public int IdStructure { get; set; }
        public string NomStructure {get; set;}
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
  
        public ICollection<LigneAnnuaire> LigneAnnuaires { get; set; }
        public ICollection<ContactStructure> ContactStructures { get; set; }


    }
}
