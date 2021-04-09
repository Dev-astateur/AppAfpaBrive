using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using ReflectionIT.Mvc.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AppAfpaBrive.Web.Areas.Identity.Data;

namespace AppAfpaBrive.Web.Layers
{
    public class CollaborateurAfpaLayer
    {

        private readonly AFPANADbContext _context;
       
        public CollaborateurAfpaLayer(AFPANADbContext context)
        {
            _context = context;
            
        }

        // Récupère les formateurs selon le début du nom
        public List<CollaborateurAfpa> GetCollaborateurStartWith(string name)
        {
            return _context.CollaborateurAfpas.Where(x => x.NomCollaborateur.StartsWith(name)).ToList();
        }

        public async Task<PagingList<CollaborateurAfpa>> GetPage(string filter, int page = 1, string sortExpression = "NomCollaborateur")
        {
            var qry = _context.CollaborateurAfpas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.NomCollaborateur.Contains(filter));
            }

            return await PagingList.CreateAsync<CollaborateurAfpa>(qry, 10, page, sortExpression, "NomCollaborateur");
        }

        public void InsertProduit(CollaborateurAfpa prod)
        {
            _context.CollaborateurAfpas.Add(prod);
            _context.SaveChanges();

        }
        public void Remove(CollaborateurAfpa prod)
        {
            _context.CollaborateurAfpas.Remove(prod);
            _context.SaveChanges();
        }
        public void Update(CollaborateurAfpa prod)
        {
            _context.CollaborateurAfpas.Update(prod);
            _context.SaveChanges();
        }
        public CollaborateurAfpaModelView GetByMatriculeCollaborateur(string id)
        {
            var obj = _context.CollaborateurAfpas.Select(e => new CollaborateurAfpaModelView()
            {
                MatriculeCollaborateurAfpa = e.MatriculeCollaborateurAfpa,
                IdEtablissement = e.IdEtablissement,
                CodeTitreCivilite = e.CodeTitreCivilite,
                NomCollaborateur = e.NomCollaborateur,
                PrenomCollaborateur = e.PrenomCollaborateur,
                MailCollaborateurAfpa = e.MailCollaborateurAfpa,
                TelCollaborateurAfpa = e.TelCollaborateurAfpa,
                Uo = e.Uo,
                UserId = e.UserId
            }).First(p => p.MatriculeCollaborateurAfpa == id);
            return obj as CollaborateurAfpaModelView;
        }
        public CollaborateurAfpa GetByMatriculeCollaborateurDelete(string matriculeCollaborateur)
        {
            return _context.CollaborateurAfpas.Find(matriculeCollaborateur);
        }

        public bool CheckMatriculeCollaborateurExiste(string id)
        {
            return _context.CollaborateurAfpas.Find(id) != null;

            if (matriculeCollaborateur == null)
            {
                return true;
            }
            return false;
        }

        public CollaborateurNavigationModelView GetCollaborateurById(string matricule)
        {
            return _context.CollaborateurAfpas.Where(e => e.MatriculeCollaborateurAfpa == matricule)
                .Select(e => new CollaborateurNavigationModelView() {
                    MatriculeCollaborateurAfpa = e.MatriculeCollaborateurAfpa,
                    NomCollaborateur = e.NomCollaborateur,
                    PrenomCollaborateur = e.PrenomCollaborateur,
                }).FirstOrDefault();
        }
    }
}
