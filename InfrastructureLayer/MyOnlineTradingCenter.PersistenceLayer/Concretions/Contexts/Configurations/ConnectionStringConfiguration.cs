using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineTradingCenter.PersistenceLayer.Concretions.Contexts.Configurations
{
    static class ConnectionStringConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../PresentationLayer/MyOnlineTradingCenter.RestfulApplicationInterfaceLayer"));
                configurationManager
                    .AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSql");
            }
        }
    }
}
