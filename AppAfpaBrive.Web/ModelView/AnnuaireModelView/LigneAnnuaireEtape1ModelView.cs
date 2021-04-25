using AppAfpaBrive.BOL.AnnuaireSocial;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.ModelView.AnnuaireModelView
{
    public class LigneAnnuaireEtape1ModelView
    {
        public int IdLigneAnnuaire { get; set; }
        public string PublicConcerne { get; set; }
        [Required(ErrorMessage = "Description courte services nécessaire")]
        public string ServiceAbrege { get; set; }
        [Required(ErrorMessage = "Description des services nécessaire")]
        public string Service { get; set; }

        public string Conditions { get; set; }

        [Required(ErrorMessage = "Categorie requise")]
        public int idCategorie { get; set; }

        public ICollection<Categorie> categories {get; set;} 
        public List<CategorieModelView> listCategories { get; set; }

        public IEnumerable<SelectListItem> listStructures { get; set; }
        
        public int IdStructure { get; set; }
        public Structure structure;
        
        public IEnumerable<ContactsCheckBox> listContacts { get; set; }
        public ICollection<Contact> contacts { get; set; }

        public LigneAnnuaireEtape1ModelView ()
        {
            categories = new HashSet<Categorie>();
            listCategories = new List<CategorieModelView>();
            listStructures = new List<SelectListItem>();
            listContacts = new List<ContactsCheckBox>();
            contacts = new HashSet<Contact>();
        }

        public LigneAnnuaire ToLigneAnnuaire()
        {
            LigneAnnuaire la = new LigneAnnuaire
            {
                Conditions = this.Conditions,
                PublicConcerne = this.PublicConcerne,
                Service = this.Service,
                ServiceAbrege = this.ServiceAbrege,
                //Structure = this.structure,
                IdStructure = this.structure.IdStructure
            };


            foreach (var cat in this.categories)
            {
                //la.CategorieLigneAnnuaires.Add(new CategorieLigneAnnuaire { Categorie = cat, IdCategorie = cat.IdCategorie, IdLigneAnnuaire = la.IdLigneAnnuaire, LigneAnnuaire = la });
                la.CategorieLigneAnnuaires.Add(new CategorieLigneAnnuaire { IdCategorie = cat.IdCategorie, IdLigneAnnuaire = la.IdLigneAnnuaire});
            }


            foreach (var contact in this.contacts)
            {
                la.ContactLigneAnnuaires.Add(new ContactLigneAnnuaire { IdContact = contact.IdContact, IdLigneAnnuaire = la.IdLigneAnnuaire});
            }

            return la;
        }
    }
}
