using AngleSharp.Text;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using VKAPITask.Utility;

namespace VKAPITask.Pages
{

    public class HomePage : Form
    {
        private readonly IButton MyPageLink = ElementFactory.GetButton(By.XPath("//li[contains(@id, 'l_pr')]"), "My Page button");

        public HomePage() : base(By.XPath("//div[contains(@id, 'main_feed')]"), "Main feed frame")
        {

        }

        public void MyPageLinkButtonClick()
        {
            MyPageLink.Click();
        }
    }
}