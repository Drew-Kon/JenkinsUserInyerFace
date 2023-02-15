using AngleSharp.Dom;
using Aquality.Selenium.Core.Utilities;

namespace VKAPITask.Utility
{
    public class Config
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory;
        static JsonSettingsFile configFile = new($"{ConfigPath}Config/TestConfig.json");
        static JsonSettingsFile userConfig = new($"{ConfigPath}Config/UserConfig.json");
        static JsonSettingsFile methodsList = new($"{ConfigPath}Data/Methods.json");
        public static string URL = new(configFile.GetValue<string>("URL"));
        public static string APIURL = new(configFile.GetValue<string>("APIURL"));
        public static string APIver = new(configFile.GetValue<string>("apiver"));
        public static string Login = new(userConfig.GetValue<string>("login"));
        public static string Password = new(userConfig.GetValue<string>("password"));
        public static string WallPostEndpoint = new(methodsList.GetValue<string>("wallpost"));
        public static string UploadPhotoEndpoint = new(methodsList.GetValue<string>("getWallUploadServer"));
        public static string SavePhotoEndpoint = new(methodsList.GetValue<string>("savephoto"));
        public static string EditPostEndpoint = new(methodsList.GetValue<string>("walledit"));
        public static string AddCommentEndpoint = new(methodsList.GetValue<string>("addcomment"));
        public static string LikesEndpoint = new(methodsList.GetValue<string>("wallgetlikes"));
    }
}