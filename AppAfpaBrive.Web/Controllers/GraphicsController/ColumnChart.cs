using AppAfpaBrive.Web.Models.Layer.GraphicModel;
using Microsoft.AspNetCore.Mvc;


namespace AppAfpaBrive.Web.Controllers.GraphicsController
{

    public class ColumnChartController : Controller
    {
        // GET: /<controller>/  
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult PopulationChart()
        {
            var populationList = PopulationDataAccessaLayer.GetCityPopulationList();
            return Json(populationList);
        }
    }
    
}
