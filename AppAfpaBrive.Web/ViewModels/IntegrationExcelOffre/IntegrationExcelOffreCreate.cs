using AppAfpaBrive.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ViewModels.IntegrationExcelOffre
{
    public class IntegrationExcelOffreCreate
    {
        #region Properties
        
        [Display(Name = "Produit de Formation")]
        [Required(ErrorMessage = "Produit de formation Requis")]
        public int CodeProduitFormation { get; set; }

        [Display(Name = "Formateur Référent")]
        [Required(ErrorMessage = "Formateur requis")]
        public string MatriculeCollaborateurAfpa { get; set; }

        [Required(ErrorMessage = "Fichier requis")]
        public FilesModel fileModel { get; set; }

        [Display(Name = "Path fichier")]
        [Required(ErrorMessage = "path requis")]
        public string PathFileIntegration { get; set; }
        #endregion
    }
}
