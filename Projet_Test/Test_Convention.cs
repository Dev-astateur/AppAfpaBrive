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
using AppAfpaBrive.Web.CustomValidator;
using Xunit;
using System.Threading.Tasks;

namespace Projet_Test
{
    [TestFixture]
    public class Test_Convention
    {
        [Test]
        public void Test_Siret_True()
        {
            //arrange
            string Siret = "19870669900321";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void Test_Siret_Poste_True()
        {
            //arrange
            string Siret = "35600000014687";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void Test_Siret_Poste_False_Number()
        {
            //arrange
            string Siret = "35600000014684";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void Test_Siret_False_Number()
        {
            //arrange
            string Siret = "35600000014684";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void Test_Siret_False_Length_Inferieur()
        {
            //arrange
            string Siret = "356000000";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void Test_Siret_False_Length_Superieur()
        {
            //arrange
            string Siret = "35600000012312312";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void Test_Siret_False_Letter()
        {
            //arrange
            string Siret = "356000000123sdsd";
            var attribute = new CustomValidator_SiretAttribute();

            //act
            var result = attribute.IsValid(Siret);

            // assert
            Assert.That(result, Is.False);

        }

        

    }
}