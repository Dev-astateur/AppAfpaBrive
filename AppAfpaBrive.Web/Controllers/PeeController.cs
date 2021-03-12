using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class PeeController : Controller
    {
        private readonly PeeLayer _peeLayer = null;
        private readonly PaysLayer _paysLayer = null;

        public PeeController ( AFPANADbContext context )
        {
            _peeLayer = new PeeLayer(context);
            _paysLayer = new PaysLayer(context);    //-- pour test
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidationStage()
        {
            Pay pays = _paysLayer.GetPaysById("US");

            Entreprise entreprise = new Entreprise()
            {
                IdEntreprise = 1,
                RaisonSociale = "Apple Distribution",
                NumeroSiret = "FR18539565218",
                Ligne1Adresse = "One Apple Park Way",
                Ville = "Cupertino",
                CodePostal = "CA 95014",
                TelEntreprise = "1 408 996–1010",
                MailEntreprise = "apple@apple.com",
                Idpays2 = pays.Idpays2,
                Idpays2Navigation = pays
            };

            return View(new Pee()
            {
                IdEntreprise = entreprise.IdEntreprise,
                IdEntrepriseNavigation = entreprise
            });
        }
    }
}
