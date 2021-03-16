using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{

    
    public class EditerInfosStagiaireController : Controller
    {
        private AFPANADbContext _context = null;
        private readonly StagiaireLayer _stagiaireLayer;
        private readonly OffreFormationLayer _offreFormation;


        public EditerInfosStagiaireController(AFPANADbContext context)
        {
            _offreFormation = new OffreFormationLayer(context);
            _stagiaireLayer = new StagiaireLayer(context);
        }    

        //GET: EditerInfosStagiaireController
        public async Task<ActionResult> ListeOffreFormation(string tbRechercherOFormation)
        {
            this.ViewBag.MonTitre = "ListeOffreFormation";
            //var query = _offreFormation.GetAllOffreFormation();
            //var query3 = _offreFormation.GetOffreFormationStartsWith(tbRechercherOFormation);
            var query2 = _offreFormation.GetOffreFormationByContains(tbRechercherOFormation);
            //var query3 = _stagiaireLayer.GetAllStagiaires();

            return View(query2);
        }

        public ActionResult Create()
        {
            return View();
        }

        //// GET: EditerInfosStagiaireController/Details/5
        //public ActionResult Details()
        //{
        //    this.ViewBag.MonTitre = "ListeOffreFormation";
        //    BeneficiaireModelView beneficiare = new BeneficiaireModelView();

        //    return View();

        //}

        //public ActionResult Beneficiaire(int id)
        //{
        //    IEnumerable<Beneficiaire> modelList = new List<Beneficiaire>();
        //    using (DAL.AFPANADbContext context = new DAL.AFPANADbContext())
        //    {              
        //        var beneficiaires = context.Beneficiaires.ToList();
        //        modelList = beneficiaires.Select(x =>
        //                                    new Beneficiaire()
        //                                    {
        //                                        NomBeneficiaire = x.NomBeneficiaire,
        //                                        PrenomBeneficiaire = x.PrenomBeneficiaire,
        //                                        MatriculeBeneficiaire = x.MatriculeBeneficiaire                                              
        //                                    });
        //    }
        //    return PartialView(modelList);
        //}


        // GET: EditerInfosStagiaireController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: EditerInfosStagiaireController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: EditerInfosStagiaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View();
        }

        // GET: EditerInfosStagiaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EditerInfosStagiaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaireController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
