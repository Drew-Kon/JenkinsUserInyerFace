using NUnit.Framework;
using RESTAPI_Task.Util;
using RestSharp;

namespace RESTAPI_Task.Base
{
    public class BaseTest
    {
        protected static Random Random = new();
        string? URL;
        RestResponse? response;

        [SetUp]
        public void Setup()
        {
            URL = Config.GetURL();
        }
    }
}