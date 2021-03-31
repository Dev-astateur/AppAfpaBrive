using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers.AnnuaireSocialLayer
{
    public class ContactLayer
    {

        private readonly AFPANADbContext _context;

        public ContactLayer(AFPANADbContext context)
        {
            _context = context;
        }

        public async Task<PagingList<Contact>> GetPage(string filter, int page = 1, string sortExpression = "Nom")
        {
            
            Contact o= _context.Contacts.Include(c => c.TitreCivilite).First();
            
            var qry = _context.Contacts.Include(x => x.TitreCivilite);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Nom.Contains(filter));
            }

            return await PagingList.CreateAsync<Contact>(qry, 20, page, sortExpression, "Nom");
        }

        public ContactModelView GetContactModelViewById(int id)
        {
            
            var obj = _context.Contacts.Include(y => y.TitreCivilite).Select(x => new ContactModelView
            {
                IdContact = x.IdContact,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Mail = x.Mail,
                Telephone = x.Telephone,
                IdTitreCivilite = x.IdTitreCivilite,
                TitreCivilite = x.TitreCivilite,
                ContactLigneAnnuaires = x.ContactLigneAnnuaires,
                ContactStructures = x.ContactStructures
            });

            return obj as ContactModelView;
        }

        public Contact GetContact(int id)
        {
            var obj = _context.Contacts.Include(y => y.TitreCivilite).ToList().First(x => x.IdContact == id);
            return obj as Contact;
        }

        public void Insert(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
        }

        public void Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }


    }
}
