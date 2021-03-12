using AppAfpaBrive.DAL;
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
            _paysLayer = new 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidationStage()
        {
            return View();
        }
    }
}
