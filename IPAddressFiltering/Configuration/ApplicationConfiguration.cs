using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressFiltering.Configuration
{
    public class ApplicationConfiguration : IConfiguration
    {
        public IDictionary<string, string> Configurations
        {
            get
            {
                return ConfigurationManager.AppSettings.AllKeys
                    .Select(k => new KeyValuePair<String, String>(k, ConfigurationManager.AppSettings[k]))
                    .ToDictionary(pair=> pair.Key, pair => pair.Value);
            }
        }

        public string GetConfiguration(string key)
        {
            return this.Configurations[key];
        }
    }
}
