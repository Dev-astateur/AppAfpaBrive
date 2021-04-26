using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.EntityFrameworkCore;

namespace AppAfpaBrive.Web.Layers.AnnuaireSocial
{
    public class ContactLigneAnnuaireLayer
    {
        private readonly AFPANADbContext _context = null;

        public ContactLigneAnnuaireLayer(AFPANADbContext context )
        {
            _context = context;
        }

        public ICollection<ContactsCheckBox> GetContactsCheckByIdLigneAnnuaire( int id )
        {
            return _context.ContactLigneAnnuaires
                .Where(e => e.IdLigneAnnuaire == id)
                .Include(e => e.Contact).ThenInclude(e => e.TitreCivilite)
                .Select(e => new ContactsCheckBox()
                {
                    IdContact = e.IdContact,
                    IdTitreCivilite = e.Contact.IdTitreCivilite,
                    Nom = e.Contact.Nom,
                    Prenom = e.Contact.Prenom,
                    Mail = e.Contact.Mail,
                    Telephone = e.Contact.Telephone,
                    TitreCivilite = e.Contact.TitreCivilite,
                    IsChecked = true
                }).ToList();
        }
    }
}
