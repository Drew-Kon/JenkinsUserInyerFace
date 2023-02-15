using Aquality.Selenium.Core.Utilities;

namespace VKAPITask.Utility
{
    public class TestData
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory;
        static JsonSettingsFile userData = new($"{ConfigPath}Config/UserConfig.json");
        public static string OwnerId = userData.GetValue<string>("userid");
        public static string AuthToken = userData.GetValue<string>("token");
        public static string photoPath = $"{ConfigPath}Data/grid_0.png";
    }
}