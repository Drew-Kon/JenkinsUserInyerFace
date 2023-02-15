using Aquality.Selenium.Browsers;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using VKAPITask.Utility;

namespace VKAPITask.Base
{
    public class BaseTest
    {
        protected static Browser? ActiveBrowser;
        protected static Random Random = new();
        protected static string URL = Config.URL;

        [TearDown]
        public void TearDown()
        {
            ActiveBrowser.Quit();
        }
    }
}