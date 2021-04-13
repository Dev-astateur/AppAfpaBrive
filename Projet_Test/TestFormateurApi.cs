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
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Projet_Test
{
    class TestFormateurApi
    {

        public AFPANADbContext  db = null;
        //AFPANADbContext dba;


        [SetUp]
        public void Setup()
        {
            if ( db is null )
            {
                db = DbContextMocker.GetAFPANADbContext("test");

                db.CollaborateurAfpas.Add(new CollaborateurAfpa()
                {
                    NomCollaborateur = "Titi",
                    MatriculeCollaborateurAfpa = "96AA011",
                });

                db.SaveChanges();
            }

        }

        [Test]
        public void TestGetFormateurStartWith_EmptyParamsReturnsNothing()
        {

            //arrange
          
         
            var controller = new FormateurApiController(db);


            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["term"] = "";

            var result = controller.GetFormateurStartWith() as ObjectResult;

            Assert.That(result is OkObjectResult);
            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.OK);
        }



        [Test]
        public void TestGetFormateurStartWith_StartWithRightInput()
        {

            //arrange
            var controller = new FormateurApiController(db);


            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["term"] = "T";
            controller.ControllerContext.HttpContext.Request.Method = HttpMethods.Get;
            //controller.ControllerContext.HttpContext.Session.Set


            var result = controller.GetFormateurStartWith() as ObjectResult;
            //JObject actualResult = JObject.Parse((string)result.Value);


            Assert.That(result is OkObjectResult);
            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.OK);
            //Assert.That(actualResult.ContainsKey("Nom"));
            
        }

    }
}
