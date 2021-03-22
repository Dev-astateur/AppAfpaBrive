using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AppAfpaBrive.Web.ModelView
{
    public partial class ProduitFormationModelView
    {
       public ProduitFormationModelView()
        {
            OffreFormations = new HashSet<OffreFormation>();
            ProduitFormationAppellationRomes = new HashSet<ProduitFormationAppellationRome>();
        }

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Le code produit formation est requis")]
        public int CodeProduitFormation { get; set; }

        public string NiveauFormation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Le libelle du produit de formation est requis")]
        public string LibelleProduitFormation { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage ="Le libelle Court du produit de formation est requis")]
        [StringLength(10, ErrorMessage ="Le Libelle Court Formation ne peut pas etre plus long que 10 caracteres")]
        public string LibelleCourtFormation { get; set; }

        //[FormationValidator(ErrorMessage ="un des champs seulement doit etre selectionné")]
        //[Display(Name ="FormationContinue")]
        [Required]
        public bool FormationContinue { get; set; }

        //[FormationValidator(ErrorMessage = "un des champs seulement doit etre selectionné")]
        //[Display(Name ="FormationDiplomante")]
        [Required]
        public bool FormationDiplomante {get;set;}

        public virtual ICollection<OffreFormation> OffreFormations { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRomes { get; set; }
    }
}
