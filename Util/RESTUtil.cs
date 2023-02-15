using Newtonsoft.Json;
using RestSharp;

namespace RESTAPI_Task.Util
{
    public class RESTUtil
    {
        private static readonly string DataPath = AppDomain.CurrentDomain.BaseDirectory;

        public static T SendGET<T>(string URL, string path)
        {
            var response = GetRequest(URL, path);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static T GetData<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static List<T> GetDataToList<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<List<T>>(response.Content);
        }

        public static RestResponse? GetRequest(string URL, string path)
        {
            RestClient restClient = new RestClient(URL);
            RestRequest restRequest = new RestRequest(path);
            return restClient.Get(restRequest);
        }

        public static RestResponse? PostRequest(string URL, string path, string body)
        {
            RestClient restClient = new RestClient(URL);
            RestRequest restRequest = new RestRequest(path);
            restRequest.AddBody(body);
            return restClient.Post(restRequest);
        }
    }
}