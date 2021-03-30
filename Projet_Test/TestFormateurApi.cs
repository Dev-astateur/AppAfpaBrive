﻿using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers.ApiControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    class TestFormateurApi
    {

        public AFPANADbContext  db = null;
        //AFPANADbContext dba;


        [SetUp]
        public void Setup()
        {
            db = DbContextMocker.GetAFPANADbContext("test");

            db.CollaborateurAfpas.Add(new CollaborateurAfpa
            {
                NomCollaborateur = "Titi",
                MatriculeCollaborateurAfpa = "96AA011"
            });

        }

        [Test]
        public void TestGetFormateurStartWith_EmptyParamsReturnsNothing()
        {

            //arrange
          
         
            var controller = new FormateurApiController(db);


            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["term"] = "";

            var result = controller.GetFormateurStartWith().Result as ObjectResult;

            Assert.That(result is OkObjectResult);
            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.OK);
            //Assert.That(result.Value);
            
 
        }

    }
}
