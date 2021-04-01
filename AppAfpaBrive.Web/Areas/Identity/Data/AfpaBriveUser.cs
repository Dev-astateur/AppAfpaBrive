using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Areas.Identity;
using Microsoft.AspNetCore.Identity;

namespace AppAfpaBrive.Web.Areas.Identity.Data
{
    public class AppAfpaBriveUser : IdentityUser
    {
        public AppAfpaBriveUser()
        {
            ListeOffresFavorites = new List<OffreFavorite>();
        }
        //[PersonalData]
        public string Nom { get; set; }
        //[PersonalData]
        public string Prenom { get; set; }
        [PersonalData]
        public DateTime? DateNaissance { get; set; }
        [PersonalData]
        public string Theme { get; set; }
        [PersonalData]
        public bool MotPasseAChanger { get; set; }
        public string OffresFavorites
        {
            get;
            set;
        }
        public IList<OffreFavorite> ListeOffresFavorites { get; set; }
    }
    [NotMapped]
    [Serializable]
    public class OffreFavorite
    {
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public string LibelleReduit { get; set; }
        public DateTime DateDebutOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }
    }
}
