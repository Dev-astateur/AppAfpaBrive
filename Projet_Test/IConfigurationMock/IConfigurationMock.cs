using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.IConfigurationMock
{
    /// <summary>
    /// classe qui mock le IConfiguration
    /// </summary>
    public static class IConfigurationMock
    {
        public static IConfiguration GetIConfiguration( Dictionary<string,string> keys  )
        {
            return new ConfigurationBuilder().AddInMemoryCollection(keys).Build();
        }
    }
}
