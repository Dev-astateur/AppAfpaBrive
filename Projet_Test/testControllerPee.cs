using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System.IO;
using Microsoft.Extensions.Configuration;
using AppAfpaBrive.Web.Controllers;
using Microsoft.Extensions.Hosting;
using AppAfpaBrive.Web.Layers;
using Projet_Test.InMemoryDb;
using NUnit.Framework.Internal;
using Projet_Test.IHostEnvironnementMock;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using ReflectionIT.Mvc.Paging;
using System.Net;
using Projet_Test.Utilitaire;
using Moq;
using AppAfpaBrive.Web.Utilitaires;
using Microsoft.AspNetCore.Mvc;
using AppAfpaBrive.Web.ModelView;

namespace Projet_Test
{
    public class TestControllerPee
    {
        private AFPANADbContext _dbContext = null;
        private IConfiguration _configuration = null;
        private IHostEnvironment _hostEnvironment = null;
        private IMailSenderMock.MailSenderMock _mailSenderMock = new IMailSenderMock.MailSenderMock();

        public TestControllerPee()
        {
            Dictionary<string, string> keys = new();

            _configuration = IConfigurationMock.IConfigurationMock.GetIConfiguration(keys);
            _hostEnvironment = IHostEnvironmentMock.GetHostEnvironment();
            _dbContext = DbContextMocker.GetAFPANADbContext("AFPANA");
            AjoutEnregistrement();
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ListePeeAValiderIsValide()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock) {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.ListePeeAValider(null).Result;
            Assert.IsInstanceOf<ViewResult>(result);
            
        }

        [Test]
        public void ListePeeAValiderIsValidePagination()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };
            var result = controller.ListePeeAValider(null).Result as ViewResult;

            Assert.IsInstanceOf<PagingList<ListePeeAValiderModelView>>(result.Model);
        }

        [Test]
        public void ListePeeAValiderIdNoFound()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = controller.ListePeeAValider(null).Result as NotFoundResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void PeeEntrepriseValidationEntreprise()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("4");
            var result = controller.PeeEntrepriseValidation(4).Result;

            Assert.IsInstanceOf<ActionResult>(result);
            Assert.Pass();
        }

        [Test]
        public void PeeEntrepriseValidationIdNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = controller.PeeEntrepriseValidation(null).Result as NotFoundResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void PeeEntrepriseValidationEntrepriseNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("-1");
            var result = controller.PeeEntrepriseValidation(-1).Result as BadRequestResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void EnregistrementPeeInfoIsValid()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };
            var result = controller.EnregistrementPeeInfo(4).Result;

            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void EnregistrementPeeInfoIsIdNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.EnregistrementPeeInfo(null).Result;
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EnregistrementPeeInfoPeeIsNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("-1");
            var result = controller.EnregistrementPeeInfo(-1).Result;

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ValidationObjectPeeUpdate()
        {
            PeeModelView peeModelView = new PeeModelView()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto"
            };
            Assert.IsFalse(ValidationService.ValidateModel(peeModelView).Any
                (va => va.MemberNames.Contains("Remarque")
                && va.ErrorMessage.Contains("Les remarques ne doivent pas excéder 1024 caractères.")));
        }

        [Test]
        public void ValidationObjectPeeUpdateNoValid()
        {
            PeeModelView peeModelView = new PeeModelView()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632"
            };

            Assert.IsTrue(ValidationService.ValidateModel(peeModelView).Any
                (va => va.MemberNames.Contains("Remarque")
                && va.ErrorMessage.Contains("Les remarques ne doivent pas excéder 1024 caractères.")));
        }

        [Test]
        public void EnregistrementPeeInfo_Enregistrement_NonValide_Redirection()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            PeeModelView peeModelView = new PeeModelView()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632"
            };

            var result = controller.EnregistrementPeeInfo(4, peeModelView).Result as RedirectToActionResult;
            Assert.That(result.ActionName, Is.EqualTo("ListePeeAValider"));
        }

        [Test]
        public void ListeDocumentPeeValidPageNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.ListeDocumentPee(4, null).Result;
            Assert.IsInstanceOf<PartialViewResult>(result);

        }

        [Test]
        public void ListeDocumentPeeValidPageNullDocumentNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.ListeDocumentPee(5, null).Result as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("EnregistrementPeeInfo"));

        }


        [Test]
        public void ListeDocumentPeeValidPageNotNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.ListeDocumentPee(4, 1).Result;
            Assert.IsInstanceOf<PartialViewResult>(result);

        }
        [Test]
        public void ListeDocumentPeeValidPageNotNulldocumentNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            var result = controller.ListeDocumentPee(5, 1).Result as RedirectToActionResult;
            Assert.That(result.ActionName, Is.EqualTo("PeeEntrepriseValidation"));
        }

        private void AjoutEnregistrement()
        {
            _dbContext.Pees.Add(new Pee()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto"
            });

            _dbContext.Pees.Add(new Pee()
            {
                IdPee = 5,
                MatriculeBeneficiaire = "20035347",
                IdTuteur = 3,
                IdResponsableJuridique = 2,
                IdEntreprise = 4,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = null
            });

            _dbContext.OffreFormations.Add(new OffreFormation()
            {
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                MatriculeCollaborateurAfpa = "1603870",
                CodeProduitFormation = 12226,
                LibelleOffreFormation = "Technicien supérieur systèmes et réseaux JO2018.1",
                LibelleReduitOffreFormation = null,
                DateDebutOffreFormation = new DateTime(2020, 09, 07),
                DateFinOffreFormation = new DateTime(2021, 06, 11)
            });

            _dbContext.Entreprises.Add( new Entreprise() {
                IdEntreprise = 2,
                RaisonSociale = "CAI",
                NumeroSiret = "42159769100029",
                MailEntreprise = null,
                TelEntreprise = null,
                Ligne1Adresse = "5, boulevard Mirabeau",
                Ligne2Adresse = null,
                Ligne3Adresse = null,
                CodePostal = "19100",
                Ville = "Brive la Gaillarde",
                Idpays2 = "FR"
            });

            _dbContext.Entreprises.Add(new Entreprise()
            {
                IdEntreprise = 4,
                RaisonSociale = "ANDROS SNC",
                NumeroSiret = "42868244700019",
                MailEntreprise = null,
                TelEntreprise = null,
                Ligne1Adresse = "ZI",
                Ligne2Adresse = null,
                Ligne3Adresse = null,
                CodePostal = "46130",
                Ville = "Biars-sur-Cère",
                Idpays2 = "FR"
            });

            _dbContext.Beneficiaires.Add(new Beneficiaire()
            {
                MatriculeBeneficiaire = "20022801",
                CodeTitreCivilite = 0,
                NomBeneficiaire = "ABRAHAM",
                PrenomBeneficiaire = "DENZEL",
                DateNaissanceBeneficiaire = new DateTime(1996, 09, 13),
                MailBeneficiaire = "louloulabeille@hotmail.com",
                TelBeneficiaire = "0690521150",
                Ligne1Adresse = null,
                Ligne2Adresse = null,
                Ligne3Adresse = null,
                CodePostal = null,
                Ville = null,
                UserId = null,
                IdPays2 = null,
                PathPhoto = null,
                MailingAutorise = true,
            });

            _dbContext.Beneficiaires.Add(new Beneficiaire()
            {
                MatriculeBeneficiaire = "20035347",
                CodeTitreCivilite = 0,
                NomBeneficiaire = "ETCHART",
                PrenomBeneficiaire = "PIERRE",
                DateNaissanceBeneficiaire = new DateTime(1985, 12, 19),
                MailBeneficiaire = "loudieres@mailo.com",
                TelBeneficiaire = "0781760241",
                Ligne1Adresse = null,
                Ligne2Adresse = null,
                Ligne3Adresse = null,
                CodePostal = null,
                Ville = null,
                UserId = null,
                IdPays2 = null,
                PathPhoto = null,
                MailingAutorise = true,
            });


            _dbContext.Professionnels.Add(new Professionnel()
            {
                IdProfessionnel = 1,
                NomProfessionnel = "Domme",
                PrenomProfessionnel = "Sébastien",
                CodeTitreCiviliteProfessionnel = 0,
            });

            _dbContext.Professionnels.Add(new Professionnel()
            {
                IdProfessionnel = 2,
                NomProfessionnel = "Domme",
                PrenomProfessionnel = "Sébastien",
                CodeTitreCiviliteProfessionnel = 0,
            });

            _dbContext.Professionnels.Add(new Professionnel()
            {
                IdProfessionnel = 3,
                NomProfessionnel = "Domme",
                PrenomProfessionnel = "Sébastien",
                CodeTitreCiviliteProfessionnel = 0,
            });

            _dbContext.PeeDocuments.Add(new PeeDocument()
            {
                IdPee = 4,
                NumOrdre = 0,
                PathDocument = "/Pee/4/external-content.duckduckgo.com.jpg",
            });

            _dbContext.PeriodePees.Add(new PeriodePee()
            {
                IdPee = 4,
                NumOrdre = 0,
                DateDebutPeriodePee = new DateTime(2020, 09, 01),
                DateFinPeriodePee = new DateTime(2021,06,30),
            });

            _dbContext.TitreCivilites.Add(new TitreCivilite()
            {
                CodeTitreCivilite = 0,
                TitreCiviliteAbrege = "",
                TitreCiviliteComplet = "",
            });

            _dbContext.SaveChanges();
        }
    }
}
