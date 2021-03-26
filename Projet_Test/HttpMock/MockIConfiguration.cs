using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Projet_Test.HttpMock
{
    public class MockIConfiguration
    {
        public Mock<IConfiguration> Config { get; set; }
       
        public MockIConfiguration()
        {
            Config.SetupGet(e => e.Get()).Returns(Config);
        }
    }
}
