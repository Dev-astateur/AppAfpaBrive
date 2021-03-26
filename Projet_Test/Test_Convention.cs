using NUnit.Framework;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web;
using AppAfpaBrive.Web.Controllers.Formateur;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using AppAfpaBrive.Web.Controllers;
using Moq;
using Microsoft.AspNetCore.Http;
using AppAfpaBrive.Web.Models;
using System;
using System.Linq;

namespace Projet_Test
{
    [TestFixture]
    public class Test_Convention
    {
        [Test]
        public void Test_Siret_True()
        {
            string Siret = "42868244700019";
            Entreprise_Siret entreprise_Siret = new Entreprise_Siret
            {
                NumeroSiret = Siret
            };
            Assert.IsTrue(ValidationService.ValidateModel(entreprise_Siret).Any(x => x.MemberNames.Contains("NumeroSiret")));
        }
    }
}