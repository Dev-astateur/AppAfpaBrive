using AppAfpaBrive.DAL;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppAfpaBrive.Web.ModelView;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{
    public class EditerInfosStagiaire2Controller : Controller
    {
        private readonly StagiaireLayer _stagiaireLayer;
        //private AFPANADbContext _context = null;
        private readonly PaysLayer _paysLayer;

        public EditerInfosStagiaire2Controller(AFPANADbContext context)
        {
            _stagiaireLayer = new StagiaireLayer(context);
            _paysLayer = new PaysLayer(context);
        }

        // GET: EditerInfosStagiaire2Controller
        public ActionResult Index()
        {
           return View();
        }

        // GET: EditerInfosStagiaire2Controller/Details/5
        // Chargement de la page avec tous les champs
        public ActionResult ChargerStagiaire(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

                var beneficiaire = _stagiaireLayer.FinByMatricule(id);
                BeneficiaireModelView beneficiaireModelView = new BeneficiaireModelView(beneficiaire);
                IEnumerable<string> listePays = _paysLayer.GetAllLibelle();
                ViewBag.LibellePays = listePays;
            

            return View(beneficiaireModelView);
        }           

        // GET: EditerInfosStagiaire2Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditerInfosStagiaire2Controller/Create
        // Enregistrement des modifications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChargerStagiaire(Beneficiaire beneficiaire)
        {
            var btnRadioGenre = Request.Form["Genre"].ToString();
            var btnRadioMailing = Request.Form["Mailing"].ToString();

            if (ModelState.IsValid)
            {                       
                if (btnRadioMailing == "0")
                {
                    beneficiaire.MailingAutorise = true;
                }
                else beneficiaire.MailingAutorise = false;

                if (btnRadioGenre == "1")
                {
                    beneficiaire.CodeTitreCivilite = 1;
                }
                else beneficiaire.CodeTitreCivilite = 0;

                _stagiaireLayer.UpdateBeneficiaire(beneficiaire);
            }
            return RedirectToAction("ListeOffreFormation", "EditerInfosStagiaire");       
        }

        // GET: EditerInfosStagiaire2Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaire2Controller/Edit/5
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

        // GET: EditerInfosStagiaire2Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaire2Controller/Delete/5
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
