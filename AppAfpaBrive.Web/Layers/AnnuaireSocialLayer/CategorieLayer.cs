using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers.AnnuaireSocialLayer
{
    public class CategorieLayer
    {
        private readonly AFPANADbContext _context;

        public CategorieLayer(AFPANADbContext context)
        {
            _context = context;
        }

        public async Task<PagingList<Categorie>> GetPage(string filter, int page = 1, string sortExpression = "LibelleCategorie")
        {
            var qry = _context.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.LibelleCategorie.Contains(filter));
            }

            return await PagingList.CreateAsync<Categorie>(qry, 20, page, sortExpression, "LibelleCategorie");
        }

        public CategorieModelView GetCategorieById(int id)
        {
            var cat = _context.Categories.Select(x => new CategorieModelView
            {
                IdCategorie = x.IdCategorie,
                LibelleCategorie = x.LibelleCategorie
            }).FirstOrDefault(x => x.IdCategorie == id);
            return cat as CategorieModelView;
        }

        public List<CategorieModelView> categories()
        {
            var cat = _context.Categories.ToList();

            return cat.Select(x => new CategorieModelView{ IdCategorie= x.IdCategorie, LibelleCategorie= x.LibelleCategorie, IsChecked = false }).ToList();

        }

        public Categorie GetCategorie(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Update(Categorie cat)
        {
            _context.Categories.Update(cat);
            _context.SaveChanges();
        }

        public void Delete(Categorie cat)
        {
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }

        public void Insert(Categorie cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
        }

    }
}
