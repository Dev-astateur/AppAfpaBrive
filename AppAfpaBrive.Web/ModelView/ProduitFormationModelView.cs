using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView
{
    public class ProduitFormationModelView
    {
        public ProduitFormationModelView()
        {
            OffreFormations = new HashSet<OffreFormation>();
            ProduitFormationAppellationRomes = new HashSet<ProduitFormationAppellationRome>();
        }
        [Key]
        [Required(AllowEmptyStrings = false)]
        public int CodeProduitFormation { get; set; }

        public string NiveauFormation { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LibelleProduitFormation { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10, ErrorMessage ="Le Libelle Court Formation ne peut pas etre plus long que 10 caracteres")]
        public string LibelleCourtFormation { get; set; }

        public bool FormationContinue { get; set; }
        public bool FormationDiplomante { get; set; }

        public virtual ICollection<OffreFormation> OffreFormations { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRomes { get; set; }
    }
}
