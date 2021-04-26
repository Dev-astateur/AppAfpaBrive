using AppAfpaBrive.BOL;
using AppAfpaBrive.BOL.AnnuaireSocial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class ContactModelView
    {
        [Key]
        public int IdContact { get; set; }

        [Required(ErrorMessage ="Merci d'entrer un nom de contact")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Merci d'entrer un prénom de contact")]
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }


        [Required(ErrorMessage = "Titre de civilité obligatoire")]
        public int IdTitreCivilite { get; set; }

        public TitreCivilite TitreCivilite { get; set; }

        public ICollection<ContactStructure> ContactStructures { get; set; }

        public ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }

        public bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ContactModelView view &&
                   IdContact == view.IdContact;
        }

        public Contact GetContact()
        {
            return new Contact
            {
                IdContact = this.IdContact,
                Nom = this.Nom,
                Prenom = this.Prenom,
                Mail = this.Mail,
                Telephone = this.Telephone,
                IdTitreCivilite = this.IdTitreCivilite,
                TitreCivilite = this.TitreCivilite,
                ContactLigneAnnuaires = this.ContactLigneAnnuaires,
                ContactStructures = this.ContactStructures
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdContact);
        }
    }
}
