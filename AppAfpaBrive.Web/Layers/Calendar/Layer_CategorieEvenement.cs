using AppAfpaBrive.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppAfpaBrive.Web.Layers.Calendar
{
    public class Layer_CategorieEvenement
    {
        private readonly AFPANADbContext _context;
        #region Constructeur
        public Layer_CategorieEvenement(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Méthode publique
        public IEnumerable<SelectListItem> GetTypeEvenements()
        {
            return _context.CategorieEvenements.Select
            (e => new SelectListItem() { Text = e.LibelleEvent, Value = e.IdCatEvent.ToString() }).ToList();
        }
        #endregion
    }
}
