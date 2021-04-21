using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AppAfpaBrive.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly SignInManager<AppAfpaBriveUser> _signInManager;
        private readonly IConfiguration _config;

        public IndexModel(
            UserManager<AppAfpaBriveUser> userManager,
            SignInManager<AppAfpaBriveUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        [Display(Name = "Matricule")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Le nom est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Nom")]
            public string Nom { get; set; }
            [Required(ErrorMessage = "Le thème est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Thème favori")]
            public string Theme { get; set; }
            [Required(ErrorMessage = "Le prénom est requis")]
            [DataType(DataType.Text)]
            [Display(Name = "Prénom")]
            public string Prenom { get; set; }

            [Display(Name = "Date Naissance")]
            [DataType(DataType.Date)]
            public DateTime? DateNaissance { get; set; }
            [Phone(ErrorMessage = "Numéro de téléphone invalide")]
            [Display(Name = "N° Téléphone")]
            public string PhoneNumber { get; set; }
            public List<SelectListItem> ListeThemes { get; set; }
            public IList<OffreFavorite> ListeOffresFavorites { get; set; }
        }

        private async Task LoadAsync(AppAfpaBriveUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nom = user.Nom,
                Prenom = user.Prenom,
                DateNaissance = user.DateNaissance,
                Theme = user.Theme,
                ListeOffresFavorites = user.ListeOffresFavorites.OrderByDescending(o => o.DateDebutOffreFormation).ToList()
            };
            if (user.OffresFavorites is not null) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                user.ListeOffresFavorites = JsonConvert.DeserializeObject<List<OffreFavorite>>(user.OffresFavorites, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            } 
            
            Input.ListeThemes = _config.GetSection("Themes").GetChildren().Select(sc => new SelectListItem() { Text = sc.Value, Value = sc.Value, Selected = sc.Value == user.Theme }).ToList();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de trouver l'utilisateur avec l'identifiant '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de trouver l'utilisateur avec l'identifiant '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erreur inattendue lors de la mise à jour du numéro de téléphone.";
                    return RedirectToPage();
                }
            }
            if (Input.Nom != user.Nom)
            {
                user.Nom = Input.Nom;
            }
            if (Input.Prenom != user.Prenom)
            {
                user.Prenom = Input.Prenom;
            }
            if (Input.Theme != user.Theme)
            {
                user.Theme = Input.Theme;
            }
            if (Input.DateNaissance != user.DateNaissance)
            {
                user.DateNaissance = Input.DateNaissance;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Votre profil a bien été mis à jour";
            return RedirectToPage();
        }
    }
}
