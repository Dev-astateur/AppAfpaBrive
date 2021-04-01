using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.IHostEnvironnementMock
{
    public class IHostEnvironmentMock
    {
        public static IHostEnvironment GetHostEnvironment()
        {
            var mockEnvironment = new Mock<IHostEnvironment>();
            mockEnvironment.Setup(m => m.EnvironmentName)
                .Returns("Hosting:Projet_Test");
            //rajouter d'autre Hosting si besoin
            return mockEnvironment.Object;
        } 
    }
}
