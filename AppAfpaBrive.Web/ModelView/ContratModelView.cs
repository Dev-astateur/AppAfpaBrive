using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL ;
using AppAfpaBrive.Web.CustomValidator;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppAfpaBrive.Web.ModelView
{
    public class ContratModelView
    {
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez indiquer votre date d'entée en fonction")]
        [DateDebutDeContratValidator()]
        public DateTime DateEntreeFonction { get; set; }
        [DataType(DataType.Date)]
        [DateDeFinDeContratValidator()]
        public DateTime? DateSortieFonction { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez indiquer votre intitulé de poste")]
        [LibelleFonctionContratValidator()]
        public string LibelleFonction { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez sélectionner un type de contrat dans la liste déroulante")]
        public string TypeContrat { get; set; }
        public IEnumerable<SelectListItem>  TypesContrats { get; set; } 
    }
}
