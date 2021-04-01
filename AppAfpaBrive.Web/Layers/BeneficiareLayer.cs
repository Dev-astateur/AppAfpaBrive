using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Layers
{
    public class BeneficiaireLayer
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
        public async Task<PagingList<ModelView.BeneficiaireSpecifiqueModelView>> GetPage(int id, int page = 1)
        {
            //var qry = _context.Beneficiaires.AsQueryable()
            var qry = _context.BeneficiaireOffreFormations.Where(e => e.IdOffreFormation == id).Include(a => a.MatriculeBeneficiaireNavigation)
            .Select(e => new BeneficiaireSpecifiqueModelView()
            {
                NomBeneficiaire = e.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                PrenomBeneficiaire = e.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                MailBeneficiaire = e.MatriculeBeneficiaireNavigation.MailBeneficiaire,
                MailingAutorise = e.MatriculeBeneficiaireNavigation.MailingAutorise

            }).AsQueryable();

            return await PagingList.CreateAsync<ModelView.BeneficiaireSpecifiqueModelView>((IOrderedQueryable<BeneficiaireSpecifiqueModelView>)qry, 5, page);
        }

        public async Task<AppAfpaBrive.Web.ModelView.Navigation.BeneficiaireNavigationModelView> BeneficiaireByIdAsync(string matricule)
        {
            return await _context.Beneficiaires.Where(e=>e.MatriculeBeneficiaire==matricule).Select(e => new AppAfpaBrive.Web.ModelView.Navigation.BeneficiaireNavigationModelView()
            {
                MatriculeBeneficiaire = e.MatriculeBeneficiaire,
                NomBeneficiaire = e.NomBeneficiaire,
                PrenomBeneficiaire = e.PrenomBeneficiaire
            }).FirstOrDefaultAsync();
        }
        #endregion 
       
    }
}