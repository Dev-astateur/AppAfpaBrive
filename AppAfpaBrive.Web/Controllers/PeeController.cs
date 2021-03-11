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
        #region champ privé
        private AFPANADbContext _dbContext = null;
        #endregion
        #region Constructeur
        public PeeController(AFPANADbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

    }
}
