using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.HttpMock
{
    public class MockHTTPContext
    {
        public Mock<HttpContext> Http { get; private set; }
        public Mock<HttpResponse> Response { get; private set; }
        public Mock<HttpRequest> Request { get; private set; }
        public Mock<ISession> Session { get; private set; }

        public MockHTTPContext ()
        {
            Http = new ();
            Response = new ();
            Request = new ();
            Session = new ();

            Http.SetupGet(c => c.Request).Returns(Request.Object);
            Http.SetupGet(c => c.Response).Returns(Response.Object);
            Http.SetupGet(c => c.Session).Returns(Session.Object);
        }

    }
}
