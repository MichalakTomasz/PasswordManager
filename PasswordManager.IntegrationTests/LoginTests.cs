using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using PasswordManager.Models;
using System;
using Xunit;

namespace PasswordManager.IntegrationTests
{
    [Collection("Sequential")]
    public class LoginTests
    {
        private WindowsDriver<WindowsElement> _driver;

        public LoginTests()
        {
            _driver = CreateDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        private WindowsDriver<WindowsElement> CreateDriver()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"D:\Repos\PasswordManager\PasswordManager\bin\Debug\netcoreapp3.1\PasswordManager.exe");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");

            var service = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            service.Start();
            var driver = new WindowsDriver<WindowsElement>(service, capabilities);
            return driver;
        }

        [Fact]
        public void ShouldDisplayErrorMessage()
        {
            var loginTextBox = _driver.FindElementByAccessibilityId("Login");
            var passwordTextBox = _driver.FindElementByAccessibilityId("Password");
            var btnLogin = _driver.FindElementByAccessibilityId("BtnLogin");
            var errorMessageTextBlock = _driver.FindElementByAccessibilityId("ErrorMessage");
            loginTextBox.Clear();
            loginTextBox.SendKeys("example");
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("1111");
            btnLogin.Click();
            Assert.Equal(errorMessageTextBlock.Text, Literals.MessageLoginError);
            _driver.CloseApp();
            _driver.Quit();
        }


    }
}
