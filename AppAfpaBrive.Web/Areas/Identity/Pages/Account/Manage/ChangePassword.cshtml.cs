using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace AppAfpaBrive.Web.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly SignInManager<AppAfpaBriveUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<AppAfpaBriveUser> userManager,
            SignInManager<AppAfpaBriveUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Le mot de passe actuel est requis")]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe actuel")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Un nouveau mot de passe est requis")]
            [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} et a plus {1} caractères.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nouveau mot de passe")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmez votre mot de passe")]
            [Compare("NewPassword", ErrorMessage = "Nouveau mot de passe et confirmé doivent être identiques.")]
            public string ConfirmPassword { get; set; }

            public bool MotPasseAChanger { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Utilisateur inconnu avec le matricule '{_userManager.GetUserId(User)}'.");
            }
            Input.MotPasseAChanger = user.MotPasseAChanger;

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
           

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Utilisateur inconnu avec le matricule '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            if (user.MotPasseAChanger)
                {
                    user.MotPasseAChanger = false;
                    await _userManager.UpdateAsync(user);
                }
           
            _logger.LogInformation("Utilisateur a changé son mot de passe avec succès.");
            StatusMessage = "Votre mot de passe a été modifié.";

            return RedirectToPage();
        }
    }
}
