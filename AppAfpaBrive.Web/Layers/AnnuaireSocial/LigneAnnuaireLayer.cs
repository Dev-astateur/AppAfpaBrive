using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers.AnnuaireSocial
{
    public class LigneAnnuaireLayer
    {

        private readonly AFPANADbContext _context;
        

        public LigneAnnuaireLayer(AFPANADbContext context)
        {
            _context = context;
           
        }

        public async Task<PagingList<LigneAnnuaire>> GetPage(string filter, int page = 1, string sortExpression = "PublicConcerne")
        {

            var qry = _context.LigneAnnuaires
                .Include(e=>e.Structure).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.PublicConcerne.Contains(filter));
            }
            return await PagingList.CreateAsync<LigneAnnuaire>(qry, 20, page, sortExpression, "Nom");
        }

        public LigneAnnuaireModelView GetLigneAnnuaireModelViewById(int id)
        {
            var obj = _context.LigneAnnuaires.Select(e => new LigneAnnuaireModelView
            {

                IdLigneAnnuaire = e.IdLigneAnnuaire,
                PublicConcerne = e.PublicConcerne,
                ServiceAbrege = e.ServiceAbrege,
                Service = e.Service,
                Conditions = e.Conditions,
                CategorieLigneAnnuaires = e.CategorieLigneAnnuaires,
                ContactLigneAnnuaires = e.ContactLigneAnnuaires,
                IdStructure = e.IdStructure,
                Structure = e.Structure

            }).FirstOrDefault(s => s.IdLigneAnnuaire == id);

            return obj as LigneAnnuaireModelView;
        }


        public LigneAnnuaire GetLigneAnnuaire(int id)
        {
            return _context.LigneAnnuaires.Find(id);
        }

        public LigneAnnuaire Insert(LigneAnnuaire ligneAnnuaire)
        {
            _context.LigneAnnuaires.Add(ligneAnnuaire);
            _context.SaveChanges();
            return ligneAnnuaire;
        }

        public void Update(LigneAnnuaire ligneAnnuaire)
        {
            _context.LigneAnnuaires.Update(ligneAnnuaire);
            _context.SaveChanges();
        }

        public void Delete(LigneAnnuaire ligne)
        {
            _context.Remove(ligne);
            _context.SaveChanges();
        }

        public async Task<LigneAnnuaireEtape1ModelView> GetLigneAnnuaireByIdAsync( int id )
        {
            return await _context.LigneAnnuaires.Where(e => e.IdLigneAnnuaire == (int)id)
                .Include(e => e.Structure)
                .Select(e => new LigneAnnuaireEtape1ModelView()
                {
                    IdLigneAnnuaire = e.IdLigneAnnuaire,
                    PublicConcerne = e.PublicConcerne,
                    ServiceAbrege = e.ServiceAbrege,
                    Service = e.Service,
                    Conditions = e.Conditions,
                    IdStructure = e.IdStructure,
                    structure = e.Structure
                })
                .FirstOrDefaultAsync();
        }
    }

   
}
