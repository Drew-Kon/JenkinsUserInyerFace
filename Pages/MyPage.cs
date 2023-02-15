using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using RestSharp;
using VKAPITask.Models;
using VKAPITask.Utility;
using static VKAPITask.Utility.RESTUtil;
using static VKAPITask.Utility.VKUtils;

namespace VKAPITask.Pages
{

    public class MyPage : Form
    {
        private readonly ILink uploadedImageSource = ElementFactory.GetLink(By.XPath("//div[contains(@id, 'pv_photo')]/img"), "img link");
        private readonly IButton closeButton = ElementFactory.GetButton(By.XPath("//div[contains(@class, 'pv_close_btn')]"), "Close Button");
        private IButton NewCommentsExpand = ElementFactory.GetButton(By.XPath("//div[contains(@class,'replies')]//a"), "Expand new comments button");
        private ITextBox PostText;
        private ITextBox PostPhoto;
        private ITextBox PostComment;
        private ITextBox CommentUserID;
        private IButton LikeButton;
        private string activepostId;
        private string activecommentId;
        private string owner = TestData.OwnerId;
        public MyPage() : base(By.XPath("//div[contains(@class, 'ProfileIndicatorBadge')]"), "Avatar frame")
        {

        }

        public string GetPostTextByPostID(string postId)
        {
            activepostId = postId;
            string postpath = $"//div[contains(@id,'wpt{owner}_{postId}')]/div[contains(@class,'wall_post_text')]";
            PostText = ElementFactory.GetTextBox(By.XPath(postpath), "Post by ID");
            string recievedText = PostText.Text;
            return recievedText;
        }

        
        public void EditPostNewTextAddPhoto(string newPostText, string photoID)
        {
            EditPostOnWallAddPhoto(newPostText, activepostId, photoID);
        }

        public string GetUpdatedPostText()
        {
            string postpath = $"//div[contains(@id,'wpt{owner}_{activepostId}')]/div[contains(@class,'wall_post_text')]";
            PostText = ElementFactory.GetTextBox(By.XPath(postpath), "Post by ID");
            string recievedText = PostText.Text;
            return recievedText;
        }
        public bool IsTheSamePhotoAdded()
        {
            string postpath = $"//div[contains(@id,'wpt{owner}_{activepostId}')]//a[contains(@href,'photo')]";
            PostPhoto = ElementFactory.GetTextBox(By.XPath(postpath), "Post Media");
            PostPhoto.Click();
            string imgSource = uploadedImageSource.GetAttribute("src");
            closeButton.Click();
            string downloadedphotoPath = DownloadPhoto(imgSource);
            float percentagediff = CompareImages.GetPercentageDifference(downloadedphotoPath, TestData.photoPath);
            int roundedPercentagediff = (int)percentagediff;
            return roundedPercentagediff == 0;
        }

        public void AddComment(string newTestComment)
        {
            RestResponse commentid = AddCommentOnPost(newTestComment, activepostId);
            activecommentId = GetData<CommentResponse>(commentid).response.comment_id.ToString();
        }

        public string GetMyCommentText()
        {
            NewCommentsExpand.Click();
            PostComment = ElementFactory.GetTextBox(By.XPath($"//div[contains(@id,'post{TestData.OwnerId}_{activecommentId}')]//div[contains(@class,'wall_reply_text')]"), "Comment");
            string CommentText = PostComment.Text.Replace("[", "").Replace("]", "").Trim();
            return CommentText;
        }
        public string GetMyCommentOwner()
        {
            NewCommentsExpand.Click();
            CommentUserID = ElementFactory.GetTextBox(By.XPath($"//div[contains(@id,'post{TestData.OwnerId}_{activecommentId}')]/div[contains(@class,'_reply_content')]/a"), "CommentPoster");
            string Commenter = CommentUserID.GetAttribute("href").Replace("https://vk.com", "").Replace("/id", "");
            return Commenter;
        }

        public void LikePost()
        {
            LikeButton = ElementFactory.GetButton(By.XPath($"//div[contains(@id,'post{TestData.OwnerId}_{activepostId}')]//div[contains(@class,'PostButtonReactions')]/span[contains(@class,'_like_button_icon')]"), "LikeButton");
            LikeButton.Click();
        }

        public string GetLikeUser()
        {
            LikesResponse response = GetData<LikesResponse>(GetLikes(activepostId));
            return response.response.users[0].uid ;
        }

        public void DeleteMyPost()
        {
            DeletePost(activepostId);
        }

        public bool PostVisible()
        {
            return PostText.State.IsDisplayed;
        }
    }
}