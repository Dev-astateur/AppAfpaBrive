using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Models.Layout;
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
        public PeeController ( AFPANADbContext context )
        {
            _peeLayer = new PeeLayer(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidationStage()
        {
            return View(_peeLayer.TestRetour());
        }
    }
}
