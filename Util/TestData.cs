using Aquality.Selenium.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAPITASK.Util
{
    internal class TestData
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetPost99TestData()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Data/EndpointTestIds.json");
            return jsonSettingsFile.GetValue<string>("post99");
        }
        public static string GetPost150TestData()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Data/EndpointTestIds.json");
            return jsonSettingsFile.GetValue<string>("post150");
        }
        public static string GetUser5TestData()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Data/EndpointTestIds.json");
            return jsonSettingsFile.GetValue<string>("user5");
        }
    }
}
