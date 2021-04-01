using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers.CollaborateurAfpa;
using AppAfpaBrive.Web.ModelView;
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
    public class TestCollaborateurAfpa
    {
        private readonly AFPANADbContext db = DbContextMocker.GetAFPANADbContext("blob");

        [Test]
        public void MatriculeCollaborateurAbsente()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo="912",
                UserId="bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("MatriculeCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le matricule du collaborateur est requis")));
        }
        [Test]
        public void MatriculeCollaborateurPresente()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa="1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("MatriculeCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le matricule du collaborateur est requis")));
        }
        [Test]
        public void MatriculeCollaborateurValide()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("MatriculeCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le matricule du collaborateur ne peut pas etre plus long que 7 caracteres")));
        }
        [Test]
        public void MatriculeCollaborateurTropLong()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567123",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("MatriculeCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le matricule du collaborateur ne peut pas etre plus long que 7 caracteres")));
        }

        [Test]
        public void IdEtablissementTropLong()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "1910045",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("IdEtablissement")
            && va.ErrorMessage.Contains("L'Id etablissement du collaborateur ne peut pas etre plus long que 5 caracteres")));
        }

        [Test]
        public void IdEtablissementValide()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("IdEtablissement")
            && va.ErrorMessage.Contains("L'Id etablissement du collaborateur ne peut pas etre plus long que 5 caracteres")));
        }
        [Test]
        public void NomCollaborateurAbsent()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("NomCollaborateur")
            && va.ErrorMessage.Contains("Le nom du Collaborateur est requis")));
        }
        [Test]
        public void NomCollaborateurPresent()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("NomCollaborateur")
            && va.ErrorMessage.Contains("Le nom du Collaborateur est requis")));
        }
        [Test]
        public void CodeTitreCivilitePresent()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("CodeTitreCivilite")
            && va.ErrorMessage.Contains("Le CodeTitreCivilite est requis")));
        }
        //[Test]
        //public void CodeTitreCiviliteAbsent()
        //{
        //    CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
        //    {
        //        MatriculeCollaborateurAfpa = "1324567",
        //        CodeTitreCivilite=-1,
        //        IdEtablissement = "19100",
        //        NomCollaborateur = "Besancon",
        //        PrenomCollaborateur = "Gabriel",
        //        MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
        //        TelCollaborateurAfpa = "0670673001",
        //        Uo = "912",
        //        UserId = "bloub"
        //    };
        //    Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
        //    va.MemberNames.Contains("CodeTitreCivilite")
        //    && va.ErrorMessage.Contains("Le CodeTitreCivilite est requis")));
        //}
        [Test]
        public void PrenomCollaborateurPresent()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("PrenomCollaborateur")
            && va.ErrorMessage.Contains("Le prénom du collaborateur est requis")));
        }
        [Test]
        public void PrenomCollaborateurAbsent()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {

                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("PrenomCollaborateur")
            && va.ErrorMessage.Contains("Le prénom du collaborateur est requis")));
        }
        [Test]
        public void TelephoneCollaborateurTropLong()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {

                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "067067300106706730010670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("TelCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le telephone du collaborateur ne peut pas etre plus long que 20 caracteres")));
        }
        [Test]
        public void TelephoneCollaborateurValide()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {

                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("TelCollaborateurAfpa")
            && va.ErrorMessage.Contains("Le telephone du collaborateur ne peut pas etre plus long que 20 caracteres")));
        }
        [Test]
        public void UOValide()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {

                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            Assert.IsFalse(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("Uo")
            && va.ErrorMessage.Contains("L'unité organisationnelle du collaborateur ne peut pas etre plus longue que 3 caracteres")));
        }
        [Test]
        public void UOTropLong()
        {
            CollaborateurAfpaModelView collaborateur = new CollaborateurAfpaModelView
            {

                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "91212",
                UserId = "bloub"
            };
            Assert.IsTrue(ValidationService.ValidateModel(collaborateur).Any(va =>
            va.MemberNames.Contains("Uo")
            && va.ErrorMessage.Contains("L'unité organisationnelle du collaborateur ne peut pas etre plus longue que 3 caracteres")));
        }
        [Test]
        public void TestCreationCollaborateur()
        {
            var bloub = new CollaborateurAfpaModelView
            {
                MatriculeCollaborateurAfpa = "1324567",
                IdEtablissement = "19100",
                CodeTitreCivilite = 0,
                NomCollaborateur = "Besancon",
                PrenomCollaborateur = "Gabriel",
                MailCollaborateurAfpa = "besancon.gabriel@hotmail.fr",
                TelCollaborateurAfpa = "0670673001",
                Uo = "912",
                UserId = "bloub"
            };
            CollaborateurAfpaController controleur = new CollaborateurAfpaController(db);
            var view = controleur.Create(bloub);

            var result = db.CollaborateurAfpas.Where(x => x.NomCollaborateur == "Besancon");
            Assert.IsTrue(result.Count() == 1);
        }
    }
}
