using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AppAfpaBrive.Web.Areas.Identity.Data;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly SignInManager<AppAfpaBriveUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly BeneficiaireLayer _layer = null;

        public LoginModel(SignInManager<AppAfpaBriveUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<AppAfpaBriveUser> userManager,
            AFPANADbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _layer = new BeneficiaireLayer(dbContext);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Le matricule est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Matricule")]
            public string UserName { get; set; }

            [Required(ErrorMessage ="Le mot de passe est requis")]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [Display(Name = "Se souvenir de moi ?")]
            public bool RememberMe { get; set; }

          
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Utilisateur connecté");
                    AppAfpaBriveUser user =  await _userManager.FindByNameAsync(Input.UserName);
                    if (user.MotPasseAChanger)
                    {
                        return RedirectToPage("./Manage/ChangePassword");
                    }
                    if ( _layer.BoolStagiaire(user.UserName) )
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Utilisateur bloqué.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Connexion invalide");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
