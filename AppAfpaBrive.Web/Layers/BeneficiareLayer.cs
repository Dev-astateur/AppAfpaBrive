using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
   public  class BeneficiaireLayer
    {
        private readonly AFPANADbContext _context;
        
        #region Constructeur
        public BeneficiaireLayer(AFPANADbContext context)
        {
            this._context = context;
        }
        #endregion
        #region Methode publique
        public ICollection<BOL.Beneficiaire> GetAllByOffredeFormation(int id)
        {
            return _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).Include(a => a.MatriculeBeneficiaireNavigation)
                 .Select(e => e.MatriculeBeneficiaireNavigation).ToList();
        }
        public async Task<PagingList<ModelView.BeneficiaireModelView>> GetPage(int id, int page = 1, string sortExpression = "NomBeneficiaire")
        {
            //var qry = _context.Beneficiaires.AsQueryable()
                var qry = _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation==id).Include(a => a.MatriculeBeneficiaireNavigation)
                .Select(e => new ModelView.BeneficiaireModelView()
                {
                    NomBeneficiaire = e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    MailBeneficiaire = e.MatriculeBeneficiaireNavigation.MailBeneficiaire,
                    MailingAutorise = e.MatriculeBeneficiaireNavigation.MailingAutorise
                }).AsQueryable() ;

            return await PagingList.CreateAsync<ModelView.BeneficiaireModelView>(qry, 20, page, sortExpression, "NomBeneficiaire");
        }
        #endregion 
    }
}
