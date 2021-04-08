using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace AppAfpaBrive.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly SignInManager<AppAfpaBriveUser> _signInManager;

        public ConfirmEmailChangeModel(UserManager<AppAfpaBriveUser> userManager, SignInManager<AppAfpaBriveUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Utilisateur inconnu avec ce matricule '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                StatusMessage = "Erreur lors du changement de mail.";
                return Page();
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            //var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
            //if (!setUserNameResult.Succeeded)
            //{
            //    StatusMessage = "Erreur lors du changement.";
            //    return Page();
            //}

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Merci d'avoir confirmé votre changement de mail.";
            return Page();
        }
    }
}
