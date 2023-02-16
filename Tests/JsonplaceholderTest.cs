using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using RESTAPI_Task.Base;
using RESTAPITASK.Extension;
using RESTAPITASK.Models;
using RestSharp;
using System.Net;
using static RESTAPI_Task.Util.Config;
using static RESTAPI_Task.Util.RESTUtil;
using static RESTAPITASK.Util.ReadUtils;
using static RESTAPITASK.Util.TestData;

namespace RESTAPI_Task.Tests
{
    [AllureNUnit]
    [AllureSuite("JsonPlaceHolder")]
    [AllureFeature("JsonPlaceHolder test")]
    [Order(0)]
    internal class JsonplaceholderTest : BaseTest
    {
        string URL = GetURL();
        string postsEndpoint = GetAllPostsEndpoint();
        string postEndpoint = GetSinglePostEndpoint();
        string userEndpoint = GetSingleUserEndpoint();
        string usersEndpoint = GetAllUsersEndpoint();
        User step5TestUser;
        RestResponse? response;
        [Test(Description = "SendGetToposts")]
        [AllureStory("SendGetToposts")]
        [AllureStep("SendGetToposts")]
        public void TEST1()
        {
            //STEP 1
            response = GetRequest(URL, postsEndpoint);
            //Assert if successful
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.OK));
            //Assert if content is JSON
            Assert.IsTrue(response.ContentType.Equals("application/json"));
            //Assert if content is ordered
            List<Post> allposts = GetDataToList<Post>(response);
            List<Post> expectedList = new List<Post>(allposts);
            expectedList.Sort((x1, x2) => x1.Id.CompareTo(x2.Id));
            Assert.IsTrue(Enumerable.SequenceEqual(expectedList, allposts));
        }
        [Test(Description = "SendGetTopost99")]
        [AllureStory("SendGetTopost99")]
        [AllureStep("SendGetTopost99")]
        public void TEST2()
        {
            //STEP 2

            Post post99 = SendGET<Post>(URL, postEndpoint + GetPost99TestData());
            //Assert if id is 99
            Assert.IsTrue(post99.Id.Equals(99));
            //Assert if userid is 10
            Assert.IsTrue(post99.UserId.Equals(10));
            //Assert if Body and Title are not empty
            Assert.IsTrue(!string.IsNullOrEmpty(post99.Title));
            Assert.IsTrue(!string.IsNullOrEmpty(post99.Body));
        }
        [Test(Description = "SendGetTopost150")]
        [AllureStory("SendGetTopost150")]
        [AllureStep("SendGetTopost150")]
        public void TEST3()
        {
            //STEP 3
            response = GetRequest(URL, postEndpoint + GetPost150TestData());
            //Assert if status code is "Not found" (404)
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.NotFound));
            //Assert if response body is empty
            Assert.IsTrue(response.Content.Contains("{}"));
        }
        [Test(Description = "POSTDataToPosts")]
        [AllureStory("POSTDataToPosts")]
        [AllureStep("POSTDataToPosts")]
        public void TEST4()
        {
            //STEP 4
            Post newPost = ReadDataFromFile<Post>("Data/Post.json");
            string seralizedPost = JsonConvert.SerializeObject(newPost);
            response = PostRequest(URL, "users", seralizedPost);
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Created));
            Post targetPost = GetData<Post>(response);
            newPost.Id = targetPost.Id;
            Assert.IsTrue(targetPost.Serialize() == newPost.Serialize());

        }
        [Test(Description = "GETUsersData")]
        [AllureStory("GETUsersData")]
        [AllureStep("GETUsersData")]
        public void TEST5()
        {
            //STEP 5
            step5TestUser = ReadDataFromFile<User>("Data/User.json");
            response = GetRequest(URL, usersEndpoint);
            //Assert if successful
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.OK));
            //Assert if content is JSON
            Assert.IsTrue(response.ContentType.Equals("application/json"));
            List<User> users = GetDataToList<User>(response);
            User targetUser = users.ElementAt(4);
        }
        [Test(Description = "GETUsersDataNo5")]
        [AllureStory("GETUsersDataNo5")]
        [AllureStep("GETUsersDataNo5")]
        public void TEST6()
        {
            //STEP 6
            response = GetRequest(URL, userEndpoint + GetUser5TestData());
            //Assert if successful
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.OK));
            //Assert if content is JSON
            Assert.IsTrue(response.ContentType.Equals("application/json"));
            User user = GetData<User>(response);
            Assert.IsTrue(user.Serialize() == step5TestUser.Serialize());
        }
    }
}