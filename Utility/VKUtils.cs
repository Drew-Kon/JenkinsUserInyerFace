using RestSharp;
using VKAPITask.Models;

namespace VKAPITask.Utility
{
    public static class VKUtils
    {
        static Random random = new Random();
        static RestClient restClient;
        static RestRequest restRequest;
        static string APIURL = Config.APIURL;
        static string apiver = Config.APIver;
        static string ownerId = TestData.OwnerId;
        static string accessToken = TestData.AuthToken;
        static string WallPostmethod = Config.WallPostEndpoint;
        static string getServerPhotoEndpoint = Config.UploadPhotoEndpoint;
        static string SavePhotoEndpoint = Config.SavePhotoEndpoint;
        static string EditPostEndpoint = Config.EditPostEndpoint;
        static string AddCommentEndpoint = Config.AddCommentEndpoint;
        static string WallGetLikesEndpoint = Config.LikesEndpoint;


        private static readonly string DataPath = AppDomain.CurrentDomain.BaseDirectory;

        public static RestResponse CreatePostOnWallNoPhoto(string postText)
        {
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(WallPostmethod);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("owner_id", ownerId);
            restRequest.AddParameter("message", postText);
            restRequest.AddParameter("v", apiver);
            RestResponse response = restClient.Get(restRequest);
            return response;
        }

        public static string GetPhotoServerUrl()
        {
            RestResponse rr = RESTUtil.GetRequestWithAccessToken(APIURL, getServerPhotoEndpoint, accessToken);
            PhotoServerResponse psrResponse = new PhotoServerResponse();
            psrResponse = RESTUtil.GetData<PhotoServerResponse>(rr);
            return psrResponse.response.upload_url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoServer">photo server</param>
        /// <returns>Array of settings</returns>
        public static UploadImageResponseValues UploadPhotoToServer(string photoServer)
        {
            restClient = new RestClient(photoServer);
            restRequest = new RestRequest(photoServer);
            restRequest.AddFile("photo", $"{DataPath}Data/grid_0.png", "multipart/form-data");
            RestResponse response = restClient.Post(restRequest);
            string[] settings = new string[3];
            UploadImageResponseValues uir = RESTUtil.GetData<UploadImageResponseValues>(response);
            return uir;
        }

        public static SaveWallPhotoResponse SavePhotoOnServer(string photoServer)
        {
            UploadImageResponseValues settings = UploadPhotoToServer(photoServer);
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(SavePhotoEndpoint);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("server", settings.server);
            restRequest.AddParameter("hash", settings.hash);
            restRequest.AddParameter("v", apiver);
            restRequest.AddParameter("photo", settings.photo);
            RestResponse response = restClient.Post(restRequest);
            SaveWallPhotoResponse responseContent = RESTUtil.GetData<SaveWallPhotoResponse>(response);
            return responseContent;
        }
        public static RestResponse EditPostOnWallAddPhoto(string newPostText, string PostId, string photoId)
        {
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(EditPostEndpoint);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("post_id", PostId);
            restRequest.AddParameter("message", newPostText);
            restRequest.AddParameter("v", apiver);
            restRequest.AddParameter("attachments", $"photo{ownerId}_{photoId}");
            RestResponse response = restClient.Post(restRequest);
            return response;
        }

        public static RestResponse AddCommentOnPost(string commentText, string postId)
        {
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(AddCommentEndpoint);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("post_id", postId);
            restRequest.AddParameter("owner_id", ownerId);
            restRequest.AddParameter("message", commentText);
            restRequest.AddParameter("v", apiver);
            RestResponse response = restClient.Post(restRequest);
            return response;
        }

        public static RestResponse GetLikes(string postId)
        {
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(WallGetLikesEndpoint);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("post_id", postId);
            restRequest.AddParameter("v", apiver);
            RestResponse response = restClient.Get(restRequest);
            return response;
        }

        public static void DeletePost(string postId)
        {
            restClient = new RestClient(APIURL);
            restRequest = new RestRequest(WallGetLikesEndpoint);
            restRequest.AddParameter("access_token", accessToken);
            restRequest.AddParameter("post_id", postId);
            restRequest.AddParameter("v", apiver);
            restClient.Post(restRequest);
        }

        public static string UploadAndSavePhoto()
        {
            string photosServer = GetPhotoServerUrl();
            SaveWallPhotoResponse swpr = SavePhotoOnServer(photosServer);
            return swpr.response.ElementAt(0).id.ToString();
        }
    }
}