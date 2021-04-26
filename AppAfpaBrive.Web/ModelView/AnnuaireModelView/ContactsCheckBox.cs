using AppAfpaBrive.BOL;
using AppAfpaBrive.BOL.AnnuaireSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class ContactsCheckBox
    {
       
        public int IdContact { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public int IdTitreCivilite { get; set; }

        public TitreCivilite TitreCivilite { get; set; }

        public ICollection<ContactStructure> ContactStructures { get; set; }

        public ICollection<ContactLigneAnnuaire> ContactLigneAnnuaires { get; set; }

        public bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ContactsCheckBox box &&
                   IdContact == box.IdContact;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdContact);
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

        
    }
}
