using AppAfpaBrive.BOL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelViews
{
    public class ViewModelBeneficiaire
    {
        [Required]
        [Display(Name = "Civilité du bénéficiaire")]
        public int CodeTitreCivilite { get; set; }

        [Required]
        [Display(Name = "Matricule du bénéficiaire")]
        public string MatriculeBeneficiaire { get; set; }

        [Required]
        [Display(Name = "Nom du bénéficiaire")]
        public string NomBeneficiaire { get; set; }

        [Required]
        [Display(Name = "Prénom du bénéficiaire")]
        public string PrenomBeneficiaire { get; set; }

        [Required]
        [Display(Name = "Adresse email du bénéficiaire")]
        public string MailBeneficiaire { get; set; }

        [Required]
        [Display(Name = "Date de naissance du bénéficiaire")]
        public DateTime DateNaissanceBeneficiaire { get; set; }

        [Required]
        [Display(Name = "Adresse du bénéficiaire")]
        public string Ligne1Adresse { get; set; }

        [Display(Name = "Adresse du bénéficiaire")]
        public string Ligne2Adresse { get; set; }

        [Display(Name = "Adresse du bénéficiaire")]
        public string Ligne3Adresse { get; set; }
        [Required]
        [Display(Name = "Code postal du bénéficiaire")]
        public string CodePostal { get; set; }
        [Required]
        [Display(Name = "Ville du bénéficiaire")]
        public string Ville { get; set; }

    }
}
