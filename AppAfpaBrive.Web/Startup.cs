using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Identity.UI.Services;
using AppAfpaBrive.Web.Utilitaires;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Logging;


namespace AppAfpaBrive.Web
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DbSecurite")));
            services.AddDbContext<AFPANADbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DbAfpaNA"),
                    assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName)));
            services.AddDatabaseDeveloperPageExceptionFilter();
            

            services.AddSingleton<IFileProvider>(
           new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            ConfigurerAutorisations(services);
            

            services.AddControllersWithViews();
            services.AddTransient<IEmailSender, SendinBlueEmailSender>();
            services.AddMvc();
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = "<span class='text-primary'> <i class='fas fa-arrow-alt-circle-down'></i></span>";
                options.HtmlIndicatorUp = "<span class='text-primary'> <i class='fas fa-arrow-circle-up'></i></span>";
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }
        private void ConfigurerAutorisations(IServiceCollection services)
        {
        services.AddDefaultIdentity<AppAfpaBriveUser>(
        options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Configuration de la structure du mot de passe
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                // Paramètres de blovage
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Configuration de User Name (ici des matricules bénéficiaires ou collaborateurs AFPA)
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = false;
            });
        }

     
    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            GererAutorisations(serviceProvider);
            ConfigurerCultures(app, serviceProvider);

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               
                endpoints.MapRazorPages();
                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));
            });
        }
        private void GererAutorisations(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppAfpaBriveUser>>();
            
            RolesSettings roles = _configuration.GetSection("Roles").Get<RolesSettings>();
            string[] listeRoles = roles.Liste.Split(',');
            foreach (string roleName in listeRoles)
            {
                // Création du rôle si inexistant
                Task<bool> hasRole = roleManager.RoleExistsAsync(roleName);
                hasRole.Wait();

                if (!hasRole.Result)
                {
                    Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }
            AdministrateurSettings admin = _configuration.GetSection("Administrateur").Get<AdministrateurSettings>();
            

            //Création du compte administrateur si inexistant

            Task<AppAfpaBriveUser> adminUser = userManager.FindByNameAsync(admin.UserName);
            adminUser.Wait();
            

            if (adminUser.Result == null)
            {
                var user = new AppAfpaBriveUser
                {
                    UserName = admin.UserName,
                    Email = admin.Mail,
                    EmailConfirmed = true,
                    MotPasseAChanger = true,
                    Nom = "Bost",
                    Prenom = "Vincent",
                    DateNaissance = new DateTime(1962, 01, 13),
                    Theme = "cyborg"
                   
                };
            user.ListeOffresFavorites.Add(new OffreFavorite() { IdOffreFormation = 20102, IdEtablissement = "19011", DateDebutOffreFormation = new DateTime(2020, 09, 01), DateFinOffreFormation = new DateTime(2021, 06, 30), LibelleReduit = "CDA 01/09/2020" });
                user.OffresFavorites = JsonConvert.SerializeObject(user.ListeOffresFavorites);
    //v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
 

                Task<IdentityResult> userResult = userManager.CreateAsync(user, admin.InitialPassWord);
                userResult.Wait();
                if (userResult.Result.Succeeded)
                {
                    //Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(user, "Administrateur");
                    //newUserRole.Wait();
                    //Task<IdentityResult> newUserRole2 = userManager.AddToRoleAsync(user, "Formateur");
                    //newUserRole.Wait();
                }
            }
           

        }
        private void ConfigurerCultures(IApplicationBuilder app,IServiceProvider services)
        {
            
            var supportedCultures = new[]
    {

                new CultureInfo("fr-FR"),
                new CultureInfo("fr"),
                new CultureInfo("en-GB"),
                new CultureInfo("en")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr-FR"),
                // Mise en forme des nombres, dates, etc.
                SupportedCultures = supportedCultures,
                // Chaines d'interface localisées
                SupportedUICultures = supportedCultures,

                FallBackToParentCultures = true,
                FallBackToParentUICultures = true
            });
        }
    }
}
