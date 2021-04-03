using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Linq;
using Xunit;

namespace PasswordManager.IntegrationTests
{
    [Collection("Sequential")]
    public class MainWindowTests : IDisposable
    {
        private WindowsDriver<WindowsElement> _driver;
        private WindowsElement _checkUpperCase;
        private WindowsElement _checkLowerCase;
        private WindowsElement _checkDigits;
        private WindowsElement _checkChars;
        private WindowsElement _btnGenerate;
        private WindowsElement _lengthKeyTextBox;

        public MainWindowTests()
        {
            _driver = CreateDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Fact]
        public void SholdBeDisabledIfEveryCriteriaAreUnsetTest()
        {
            Login();
            InitializeMainWindowControls();
            if (_checkUpperCase.Selected) _checkUpperCase.Click();
            if (_checkLowerCase.Selected) _checkLowerCase.Click();
            if (_checkDigits.Selected) _checkDigits.Click();
            if (_checkChars.Selected) _checkChars.Click();

            var gearateDisabled = _btnGenerate.Enabled;
            Assert.False(gearateDisabled);
            _driver.CloseApp();
        }

        [Fact]
        public void GenerateKeyShouldBeDisabledIfKeyLengthEqualZero()
        {
            Login();
            InitializeMainWindowControls();

            if (!_checkUpperCase.Selected) _checkUpperCase.Click();
            _lengthKeyTextBox.Clear();
            _lengthKeyTextBox.SendKeys("0");
            Assert.False(_btnGenerate.Enabled);
            _driver.CloseApp();

        }

        private void Login()
        {
            var loginTextBox = _driver.FindElementByAccessibilityId("Login");
            var passwordTextBox = _driver.FindElementByAccessibilityId("Password");
            var btnLogin = _driver.FindElementByAccessibilityId("BtnLogin");
            loginTextBox.Clear();
            loginTextBox.SendKeys("tomas");
            passwordTextBox.Clear();
            passwordTextBox.SendKeys("1234");
            btnLogin.Click();

            _driver.SwitchTo().Window(_driver.WindowHandles.First());
        }
        private void InitializeMainWindowControls()
        {
            _checkUpperCase = _driver.FindElementByAccessibilityId("checkUpperCase");
            _checkLowerCase = _driver.FindElementByAccessibilityId("checkLowerCase");
            _checkDigits = _driver.FindElementByAccessibilityId("checkDigits");
            _checkChars = _driver.FindElementByAccessibilityId("checkChars");
            _btnGenerate = _driver.FindElementByAccessibilityId("btnGenerate");
            _lengthKeyTextBox = _driver.FindElementByAccessibilityId("txtKeyLength");
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

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
