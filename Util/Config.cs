using Aquality.Selenium.Core.Utilities;

namespace RESTAPI_Task.Util
{
    public class Config
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetURL()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Config/TestConfig.json");
            return jsonSettingsFile.GetValue<string>("URL");
        }
        public static string GetSinglePostEndpoint()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Config/TestConfig.json");
            return jsonSettingsFile.GetValue<string>("post");
        }

        public static string GetAllPostsEndpoint()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Config/TestConfig.json");
            return jsonSettingsFile.GetValue<string>("posts");
        }
        public static string GetAllUsersEndpoint()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Config/TestConfig.json");
            return jsonSettingsFile.GetValue<string>("users");
        }

        public static string GetSingleUserEndpoint()
        {
            JsonSettingsFile jsonSettingsFile = new($"{ConfigPath}Config/TestConfig.json");
            return jsonSettingsFile.GetValue<string>("user");
        }
    }
}