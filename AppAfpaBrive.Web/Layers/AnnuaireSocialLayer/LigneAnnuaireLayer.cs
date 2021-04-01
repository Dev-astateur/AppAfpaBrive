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
    public class LigneAnnuaireLayer
    {

        private readonly AFPANADbContext _context;

        public LigneAnnuaireLayer(AFPANADbContext context)
        {
            _context = context;
        }

        public async Task<PagingList<LigneAnnuaire>> GetPage(string filter, int page = 1, string sortExpression = "Nom")
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
    }

    //public 
}
