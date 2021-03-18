using AppAfpaBrive.BOL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public partial class Professionnel_ModelView
    {
        [Required(ErrorMessage ="Veuillez remplir le champ")]
        public string NomProfessionnel { get; set; }

        [Required(ErrorMessage = "Veuillez remplir le champ")]
        public string PrenomProfessionnel { get; set; }

        [Required(ErrorMessage = "Veuillez remplir le champ")]
        public int CodeTitreCiviliteProfessionnel { get; set; }

        [EmailAddress(ErrorMessage ="Veuillez saisir une adresse mail valide.")]
        public string AdresseMail { get; set; }

        [Phone(ErrorMessage ="Veuillez saisir un numéros de telephone valide.")]
        public string NumerosTel { get; set; }
        public string Fonction { get; set; }

    }
}
