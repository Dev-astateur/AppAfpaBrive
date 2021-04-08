using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.Utilitaire
{
    public static class UnitMVCTestRedirect
    {
        public static void ShouldBeRedirectionTo(this ActionResult actionResult, object exprectedRouteValues)
        {
            RouteValueDictionary actualValues = ((RedirectToRouteResult)actionResult).RouteValues;
            var expectedValues = new RouteValueDictionary(exprectedRouteValues);

            foreach (string key in expectedValues.Keys)
            {
                Assert.AreEqual(expectedValues[key], actualValues[key]);
            }
        }
    }
}
