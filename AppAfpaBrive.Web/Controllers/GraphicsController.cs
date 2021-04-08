using Microsoft.AspNetCore.Mvc;
using AppAfpaBrive.Web.Helpers.GraphicsDataHelpers;
using System.Diagnostics;
using System.Collections.Generic;
using AppAfpaBrive.BOL;

namespace AppAfpaBrive.Web.Controllers
{
    public class GraphicsController : Controller
    {
        // GET: /<controller>/  
        public IActionResult Index()
        {
            Debug.WriteLine("dsqdsq");
            return View();
        }

        [HttpGet]
        public IActionResult GraphDatas(string filterMois, string filterYear, string filterEtablissement, string filterOffreFormation)
        {
            List<IInsertion> li = GetData.GetGraphData(filterMois, filterYear, filterEtablissement, filterOffreFormation);
            return Json(li);
            
        }

        public IActionResult AutoCompleteEtablissement(string prefix)
        {
            var etablissement = GetData.GetEtablissementData(prefix);
            return Json(etablissement);
        }

        public IActionResult AutoCompleteOffreFormation(string prefix)
        {
            var offreFormation = GetData.GetOffreFormationData(prefix);
            return Json(offreFormation);
        }
    }
    
}
