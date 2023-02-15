using AngleSharp.Dom;
using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace VKAPITask.Utility
{
    public class RESTUtil
    {
        private static readonly string DataPath = AppDomain.CurrentDomain.BaseDirectory;
        //}

        public static T GetData<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static RestResponse? GetRequestWithAccessToken(string URL, string path, string token)
        {
            RestClient restClient = new RestClient(URL);
            RestRequest restRequest = new RestRequest(path);
            restRequest.AddParameter("access_token", token);
            restRequest.AddParameter("v", "5.81");
            return restClient.Get(restRequest);
        }

        public static string DownloadPhoto(string photoUrl)
        {
            var client = new RestClient(photoUrl);
            var request = new RestRequest();
            var response = client.Get(request);
            byte[] imageBytes = response.RawBytes;
            File.WriteAllBytes($"{DataPath}/test.jpg", imageBytes);
            return $"{DataPath}/test.jpg";
        }
    }
}