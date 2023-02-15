using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VKAPITask.Pages
{

    public class VKLoginPage2 : Form
    {
        private readonly ITextBox Password = ElementFactory.GetTextBox(By.XPath("//input[contains(@name, 'password')]"), "Email Field");
        private readonly IButton ContinueButton = ElementFactory.GetButton(By.XPath("//span[contains(@class, 'vkuiButton__in')]"), "Continue button");


        public VKLoginPage2() : base(By.XPath("//div[contains(@class, \"vkc__AuthRoot__contentIn\")]"), "Login Icon")
        {

        }

        public void ContinueButtonClick()
        {
            ContinueButton.Click();
        }

        public void EnterPassword(string pwd)
        {
            Password.SendKeys(pwd);
        }
    }
}