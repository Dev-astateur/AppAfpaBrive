using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers.Calendar
{
    public class Layer_Evenement
    {
        private readonly AFPANADbContext _context;
        #region Constructeur
        public Layer_Evenement(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Méthode publique
        public ICollection<Evenement> GetEvenements(int? month, int year)
        {
            return _context.Evenements.Where(e => e.DateEvent.Year == year && e.DateEvent.Month == month)
                .Where(e => e.IdEtablissement == "19011")
                .ToList();
            #endregion
        }
        public void AddEvenement(Evenement evenement)
        {
            _context.Evenements.Add(evenement);
            _context.SaveChanges();
        }

    }
}
