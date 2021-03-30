using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
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

    }
}
