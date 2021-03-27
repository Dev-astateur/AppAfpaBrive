using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL ;
using AppAfpaBrive.Web.CustomValidator;


namespace AppAfpaBrive.Web.ModelView
{
    public class ContratModelView
    {
        
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez indiquer votre date d'entée en fonction")]
        [DataType(DataType.Date)]
        public DateTime DateEntreeFonction { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateSortieFonction { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez indiquer votre intitulé de poste")]
        [LibelleFonctionContratValidator()]
        public string LibelleFonction { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez sélectionner un type de contrat dans la liste déroulante")]
        //public string TypeContrat { get; set; }



    }
}
