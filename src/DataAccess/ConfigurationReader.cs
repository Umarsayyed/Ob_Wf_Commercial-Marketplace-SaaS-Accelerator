using Microsoft.Extensions.Configuration;
using System;

namespace Marketplace.SaaS.Accelerator.DataAccess
{
    public sealed class ConfigurationReader
    {
        private ConfigurationReader()
        {
            GetConfiguration();
        }

        private static ConfigurationReader instance = null;
        public static ConfigurationReader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationReader();
                }
                return instance;
            }
        }
        public IConfiguration Configuration { get; set; }
        private void GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("obsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);           
            Configuration = builder.Build();
        }
    }
}
