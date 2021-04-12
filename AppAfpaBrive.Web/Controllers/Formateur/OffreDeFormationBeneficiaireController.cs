using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.ModelView;
using AppAfpaBrive.Web.Layers;

namespace AppAfpaBrive.Web.Controllers.Formateur
{
    public class OffreDeFormationBeneficiaireController : Controller
    {
        private Layer_OffreFormation _offreDeFormationLayer;
        private BeneficiaireOffreFormationLayer _beneficiaireOffreFormationLayer;
        private BeneficiaireLayer _beneficiaireLayer;


        public OffreDeFormationBeneficiaireController(AFPANADbContext context)
        {
            _offreDeFormationLayer = new Layer_OffreFormation(context);
            _beneficiaireOffreFormationLayer = new BeneficiaireOffreFormationLayer(context);
            _beneficiaireLayer = new BeneficiaireLayer(context);

        }
        // GET: StagiaireParOffredeFormationController

        //[HttpGet]
        //public ActionResult OffreDeFormationBeneficiaire()
        //{

        //    this.ViewBag.MonTitre = "OffreDeFormationBeneficiaire";

        //    var query = _offreDeFormationLayer.GetByMatriculeCollaborateurAFPA("96GB011");

        //    OffreFormationSpecifiqueModelView model = new OffreFormationSpecifiqueModelView(query);
        //    int selectedvalue = model.IdOffreFormation;
        //    query.BeneficiaireOffreFormations = _beneficiaireOffreFormationLayer.GetAllByOffreFormation(selectedvalue);
        //    model.AlimenterListeOffreFormations(_offreDeFormationLayer.GetAllbyMatricule("96GB011"));



        //    foreach (var item in query.BeneficiaireOffreFormations)
        //    {
        //        model.BeneficiaireOffreFormations.Add(item);
        //    }


        //    return View(model);
        //}
        //[HttpGet]
        //public ActionResult OffreDeFormationBeneficiaire(int id, string matricule)
        //{

        //    this.ViewBag.MonTitre = "OffreDeFormationBeneficiaire";
        //    this.ViewData["OffreFormation"] = _offreDeFormationLayer.GetAllbyMatricule(matricule);

        //    var offreFormations = _offreDeFormationLayer.GetAllbyMatricule("96GB011");

        //    List<BeneficiaireSpecifiqueModelView> liste = new();
        //    foreach (var item in offreFormations )
        //    {
        //        liste.Add(new BeneficiaireSpecifiqueModelView(item));
        //    }
        //    //int selectedvalue = model.IdOffreFormation;
        //    //id = selectedvalue;
        //    //query.BeneficiaireOffreFormations = _beneficiaireOffreFormationLayer.GetAllByOffreFormation(id);
        //   //model.AlimenterListeOffreFormations(_offreDeFormationLayer.GetAllbyMatricule("96GB011"));



        //    //foreach (var item in query.BeneficiaireOffreFormations)
        //    //{
        //    //    model.BeneficiaireOffreFormations.Add(item);
        //    //}


        //    return View(liste);
        //}
        //[HttpGet]
        //public IActionResult ListeBeneficiaire(int id)
        //{
        //    var beneficiaires = _beneficiaireLayer.GetAllByOffredeFormation(id);
        //    List<BeneficiaireModelView> liste = new();
        //    foreach (var item in beneficiaires)
        //    {
        //        liste.Add(new BeneficiaireModelView(item));
        //    }

        //    return PartialView("~/Views/Shared/Beneficiaire/_BeneficiairePartial.cshtml",liste );
        //}
        [HttpGet]
        public async Task<IActionResult> OffredeFormationBeneficiaire(int id, int? pageIndex)
        {
           
            //var offreFormations = _offreDeFormationLayer.GetAllbyMatricule("96GB011");
            //var beneficiaires = _beneficiaireLayer.GetAllByOffredeFormation(id);
            if (pageIndex is null) 
            {
                pageIndex = 1;
            }
            BeneficiaireSpecifiqueModelView modelView = new BeneficiaireSpecifiqueModelView();
            modelView.SelectListItems = _offreDeFormationLayer.GetAllbyMatricule("96GB011");
            var selected =  modelView.SelectListItems.Where(e => e.Value == id.ToString()).FirstOrDefault();
            
            if ( selected is not null )
                selected.Selected = true;

            modelView.PagingBeneficiaires = await _beneficiaireLayer.GetPage(id, (int)pageIndex);
            modelView.PagingBeneficiaires.Action = "OffredeFormationBeneficiaire";
            //modelView.Action = "OffreDeFormationBeneficiaire";
            return View(modelView);
        }

        // GET: OffreDeFormationBeneficiaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OffreDeFormationBeneficiaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OffreDeFormationBeneficiaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: OffreDeFormationBeneficiaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Edit/5
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

        // GET: OffreDeFormationBeneficiaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Delete/5
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
