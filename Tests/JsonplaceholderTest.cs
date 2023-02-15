using Newtonsoft.Json;
using NUnit.Framework;
using RESTAPI_Task.Base;
using RESTAPITASK.Extension;
using RESTAPITASK.Models;
using RESTAPITASK.Util;
using RestSharp;
using System.Net;
using static RESTAPI_Task.Util.Config;
using static RESTAPI_Task.Util.RESTUtil;
using static RESTAPITASK.Util.ReadUtils;
using static RESTAPITASK.Util.TestData;

namespace RESTAPI_Task.Tests
{
    [Order(0)]
    internal class JsonplaceholderTest : BaseTest
    {
        string URL = GetURL();
        string postsEndpoint = GetAllPostsEndpoint();
        string postEndpoint = GetSinglePostEndpoint();
        string userEndpoint = GetSingleUserEndpoint();
        string usersEndpoint = GetAllUsersEndpoint();
        RestResponse? response;
        [Test]
        public void TEST()
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

            //STEP 2
            
            Post post99 = SendGET<Post>(URL, postEndpoint + GetPost99TestData());
            //Assert if id is 99
            Assert.IsTrue(post99.Id.Equals(99));
            //Assert if userid is 10
            Assert.IsTrue(post99.UserId.Equals(10));
            //Assert if Body and Title are not empty
            Assert.IsTrue(!string.IsNullOrEmpty(post99.Title));
            Assert.IsTrue(!string.IsNullOrEmpty(post99.Body));

            //STEP 3
            response = GetRequest(URL, postEndpoint + GetPost150TestData());
            //Assert if status code is "Not found" (404)
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.NotFound));
            //Assert if response body is empty
            Assert.IsTrue(response.Content.Contains("{}"));

            //STEP 4
            Post newPost = ReadDataFromFile<Post>("Data/Post.json");
            string seralizedPost = JsonConvert.SerializeObject(newPost);
            response = PostRequest(URL, "users", seralizedPost);
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.Created));
            Post targetPost = GetData<Post>(response);
            newPost.Id = targetPost.Id;
            Assert.IsTrue(targetPost.Serialize() == newPost.Serialize());


            //STEP 5
            User step5TestUser = ReadDataFromFile<User>("Data/User.json");
            response = GetRequest(URL, usersEndpoint);
            //Assert if successful
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.OK));
            //Assert if content is JSON
            Assert.IsTrue(response.ContentType.Equals("application/json"));
            List<User> users = GetDataToList<User>(response);
            User targetUser = users.ElementAt(4);

            //STEP 6
            response = GetRequest(URL, userEndpoint + GetUser5TestData());
            //Assert if successful
            Assert.IsTrue(response.StatusCode.Equals(HttpStatusCode.OK));
            //Assert if content is JSON
            Assert.IsTrue(response.ContentType.Equals("application/json"));
            User user = GetData<User>(response);
            Assert.IsTrue(user.Serialize()==step5TestUser.Serialize());
        }
    }
}