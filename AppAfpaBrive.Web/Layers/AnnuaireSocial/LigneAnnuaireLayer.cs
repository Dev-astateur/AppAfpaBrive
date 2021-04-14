using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
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

            var qry = _context.LigneAnnuaires.AsQueryable();
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

        public void Insert(LigneAnnuaire ligneAnnuaire)
        {
            _context.LigneAnnuaires.Add(ligneAnnuaire);
            _context.SaveChanges();
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


    }

   
}
