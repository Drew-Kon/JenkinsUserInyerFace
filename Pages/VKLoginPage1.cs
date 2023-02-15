using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VKAPITask.Pages
{

    public class VKLoginPage1 : Form
    {
        private readonly ITextBox Login = ElementFactory.GetTextBox(By.XPath("//input[contains(@id, 'index_email')]"), "Email Field");
        private readonly IButton SignInButton = ElementFactory.GetButton(By.XPath("//button[contains(@type, 'submit')]"), "SignIn button");


        public VKLoginPage1() : base(By.XPath("//div[contains(@id, 'index_login')]//div[contains(@class, 'VkIdForm__icon')]"), "Login Icon")
        {

        }

        public void SignInClick()
        {
            SignInButton.Click();
        }

        public void EnterLogin(string login)
        {
            Login.SendKeys(login);
        }
    }
}