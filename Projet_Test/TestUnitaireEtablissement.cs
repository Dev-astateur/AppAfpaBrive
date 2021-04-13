using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers.Etablissement;
using AppAfpaBrive.Web.Controllers.ProduitFormation;
using AppAfpaBrive.Web.ModelView;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    [TestFixture]
    public class TestUnitaireEtablissement
    {
        private readonly AFPANADbContext db = DbContextMocker.GetAFPANADbContext("bloub");


        [Test]
        public void IdEtablissementRequis()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement="0670673001",
                Ligne1Adresse="44 rue Hoche",
                CodePostal="37700",
                Ville="St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("IdEtablissement")
            && va.ErrorMessage.Contains("L'Id de l'établissement est requis")));
        }
        [Test]
        public void IdEtablissementFourni()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement="37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("IdEtablissement")
            && va.ErrorMessage.Contains("L'Id de l'établissement est requis")));
        }
        [Test]
        public void IdEtablissementRattachementFourni()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("IdEtablissementRattachement")
            && va.ErrorMessage.Contains("L'Id de l'établissement de rattachemen")));
        }

        [Test]
        public void IdEtablissementRattachementAbsent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
              
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("IdEtablissementRattachement")
            && va.ErrorMessage.Contains("L'Id de l'établissement de rattachemen")));
        }

        [Test]
        public void NomEtablissementAbsent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
               
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("NomEtablissement")
            && va.ErrorMessage.Contains("Le Nom de l'établissement")));
        }
        [Test]
        public void NomEtablissementPresent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("NomEtablissement")
            && va.ErrorMessage.Contains("Le Nom de l'établissement")));
        }
        [Test]
        public void TelephoneTropLong()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("TelEtablissement")
            && va.ErrorMessage.Contains("ne peut pas etre plus long que 20 caracteres")));
        }
        [Test]
        public void TelephoneValide()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "0670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("TelEtablissement")
            && va.ErrorMessage.Contains("ne peut pas etre plus long que 20 caracteres")));
        }
        [Test]
        public void Ligne1adresseAbsent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ligne1Adresse")
            && va.ErrorMessage.Contains("L'adresse est requise")));
        }
        [Test]
        public void Ligne1adressePresent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ligne1Adresse")
            && va.ErrorMessage.Contains("L'adresse est requise")));
        }
        [Test]
        public void Ligne1adressePasValideSiStringEmpty()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ligne1Adresse")
            && va.ErrorMessage.Contains("L'adresse est requise")));
        }

        [Test]
        public void CodePostalAbsent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
              
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("CodePostal")
            && va.ErrorMessage.Contains("Le code postal est requis")));
        }
        [Test]
        public void CodePostalPresent()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("CodePostal")
            && va.ErrorMessage.Contains("Le code postal est requis")));
        }
        [Test]
        public void CodePostalTropLong()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "377001231214",
                Ville = "St Pierre des Corps"
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("CodePostal")
            && va.ErrorMessage.Contains("Le Code Postal ne peut pas etre plus long que 10 caracteres")));
        }
        [Test]
        public void CodePostalValide()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("CodePostal")
            && va.ErrorMessage.Contains("Le Code Postal ne peut pas etre plus long que 10 caracteres")));
        }

        [Test]
        public void VilleAbsente()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700"
               
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ville")
            && va.ErrorMessage.Contains("La Ville est requise")));
        }
        [Test]
        public void VillePresente()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "St Pierre des Corps"
            };
            Assert.IsFalse(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ville")
            && va.ErrorMessage.Contains("La Ville est requise")));
        }
        [Test]
        public void VillePasValideSiVide()
        {
            EtablissementModelView etablissement = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = ""
            };
            Assert.IsTrue(ValidationService.ValidateModel(etablissement).Any(va =>
            va.MemberNames.Contains("Ville")
            && va.ErrorMessage.Contains("La Ville est requise")));
        }

        [Test]
        public void TestCreationEtablissement()
        {
            var bloub = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "Tours"
            };
            EtablissementController controleur = new EtablissementController(db);
            var view = controleur.Create(bloub);

            var result = db.Etablissements.Where(x => x.NomEtablissement == "MOMA");
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public void TestEditionEtablissement()
        {

            var bloub = new EtablissementModelView
            {
                IdEtablissement = "37700",
                IdEtablissementRattachement = "87000",
                NomEtablissement = "MOMA",
                MailEtablissement = "besancon.gabriel@hotmail.fr",
                TelEtablissement = "067067300106706730010670673001",
                Ligne1Adresse = "44 rue Hoche",
                CodePostal = "37700",
                Ville = "Tours"
            };
            var toto = db.Etablissements.Find(bloub.IdEtablissement);
            if ( toto is null)
            {
                db.Etablissements.Add(bloub.GetEtablissement());
                db.SaveChanges();
                
            }

            EtablissementController controleur = new EtablissementController(db);
            bloub.NomEtablissement = "EtablissementCentral";

            var view = controleur.Edit(bloub);

            var result = db.Etablissements.Where(x => x.NomEtablissement == "EtablissementCentral");
            Assert.IsTrue(result.Count() == 1);

        }
    }
}
