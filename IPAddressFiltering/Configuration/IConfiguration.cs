using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressFiltering.Configuration
{
    public interface IConfiguration
    {
        IDictionary<String, String> Configurations { get; }

        String GetConfiguration(String key);
    }
}
