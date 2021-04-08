using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace AppAfpaBrive.Web.Areas.Identity.Pages.Account.Manage
{
    public class _PageOffresFavoritesModel : PageModel
    {
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly SignInManager<AppAfpaBriveUser> _signInManager;
        
        public _PageOffresFavoritesModel(
            UserManager<AppAfpaBriveUser> userManager,
            SignInManager<AppAfpaBriveUser> signInManager           
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        public void OnGet()
        {
        }
    }
}
