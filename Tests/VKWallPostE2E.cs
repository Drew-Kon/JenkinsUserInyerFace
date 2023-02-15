using AngleSharp.Dom;
using Aquality.Selenium.Browsers;
using NUnit.Framework;
using RestSharp;
using VKAPITask.Base;
using VKAPITask.Models;
using VKAPITask.Pages;
using VKAPITask.Utility;
using static VKAPITask.Utility.VKUtils;

namespace VKAPITask.Tests
{
    [Order(0)]
    public class VKWallPostE2E : BaseTest
    {
        [TestCase(BrowserName.Firefox)]
        [TestCase(BrowserName.Chrome)]
        public void TEST(BrowserName browserName)
        {
            Environment.SetEnvironmentVariable("browserName", browserName.ToString());
            ActiveBrowser = AqualityServices.Browser;
            ActiveBrowser.Maximize();
            //Step 1
            ActiveBrowser.GoTo(URL);

            //Step 2
            VKLoginPage1 VKLogin1 = new VKLoginPage1();
            Assert.That(VKLogin1.State.WaitForDisplayed(), Is.True, "Required page wasn't opened, expected: Home Login Page");
            VKLogin1.EnterLogin(Config.Login);
            VKLogin1.SignInClick();
            VKLoginPage2 VKLogin2 = new VKLoginPage2();
            Assert.That(VKLogin2.State.WaitForDisplayed(), Is.True, "Required page wasn't expected: Login Page");
            VKLogin2.EnterPassword(Config.Password);
            VKLogin2.ContinueButtonClick();

            //Step 3
            HomePage homePage = new HomePage();
            Assert.That(homePage.State.WaitForDisplayed(), Is.True, "Required page wasn't opened, expected: Home Page");
            homePage.MyPageLinkButtonClick();

            //Step 4
            Random random = new Random();
            string postText = StringUtils.GenerateString(100, random);
            RestResponse response = CreatePostOnWallNoPhoto(postText);
            string postNumberId = RESTUtil.GetData<WallPostResponse>(response).response.post_id;

            //Step 5
            MyPage myPage = new MyPage();
            Assert.That(myPage.State.WaitForDisplayed(), Is.True, "Required page wasn't opened, expected: My page");
            Assert.IsTrue(myPage.GetPostTextByPostID(postNumberId) == postText, "Post text does not equal to the control.");

            //Step 6;
            string PhotoId = UploadAndSavePhoto();
            string newPostText = StringUtils.GenerateString(100, random);
            myPage.EditPostNewTextAddPhoto(newPostText, PhotoId);

            //Step 7
            //Post is updated with a same photo,
            Assert.IsTrue(myPage.IsTheSamePhotoAdded());
            //Post is updated with a new text
            Assert.IsTrue(myPage.GetUpdatedPostText()==newPostText);

            //Step 8
            //[API]Add comment
            string newCommentText = StringUtils.GenerateString(100, random);
            myPage.AddComment(newCommentText);

            //Step 9
            //Check that there is a comment from my user
            Assert.True(myPage.GetMyCommentOwner()==TestData.OwnerId);
            Assert.True(myPage.GetMyCommentText() == newCommentText);

            //Step 10
            //Like your post
            myPage.LikePost();

            //Step 11
            //[API]Check for like
            Assert.IsTrue(myPage.GetLikeUser() == TestData.OwnerId);

            //Step 12
            //[API]Delete post
            myPage.DeleteMyPost();

            //Step 13
            //Check for deleted post/
            Assert.False(myPage.PostVisible());
        }
    }
}