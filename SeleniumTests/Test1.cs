using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class Test1
    {

        //private readonly IWebDriver _firefox = new FirefoxDriver();
        //private readonly IWebDriver _chrome = new ChromeDriver();
        private readonly IWebDriver _iexplorer = new InternetExplorerDriver();

        private string email = string.Join("", Guid.NewGuid().ToString().Take(6)) + "@gmail.com";
        private string password = "Ab*?" + string.Join("", Guid.NewGuid().ToString().Take(6));
        private string client = "Test_Client_" + string.Join("", Guid.NewGuid().ToString().Take(6));
        private string employee = "Test_Employee_" + string.Join("", Guid.NewGuid().ToString().Take(6));

        [TestMethod]
        public void Can_Create_Account_And_Login()
        {
            CreateUser(_iexplorer, email, password);
            LogOutUser(_iexplorer);
            LoginUser(_iexplorer, email, password);
            LogOutUser(_iexplorer);
        }
        [TestMethod]
        public void Can_loggin_and_add_Client()
        {
            CreateUser(_iexplorer, email, password);
            AddClient(_iexplorer, client);
            LogOutUser(_iexplorer);
        }
        //[TestMethod]
        //public void Can_loggin_and_add_Employee()
        //{
        //    CreateUser(_iexplorer, email, password);
        //    AddEmployee(_iexplorer, employee);
        //    LogOutUser(_iexplorer);
        //}

        [TestCleanup]
        public void TestCleanup()
        {
            if (_iexplorer != null)
                _iexplorer.Quit();
            //if (_firefox != null)
            //    _firefox.Quit();
            //if (_chrome != null)
            //    _chrome.Quit();
        }

        private void CreateUser(IWebDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://localhost/GoalNews/");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("registerLink")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.CssSelector(".btn.btn-info")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("loggedin")));
            var userWasCreated = driver.FindElement(By.Id("loggedin")).Text.Contains(email);

            Assert.IsTrue(userWasCreated);
        }

        private void LoginUser(IWebDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://localhost/GoalNews/");
            driver.FindElement(By.Id("loginLink")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.CssSelector(".btn.btn-info")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("loggedin")));
            var userWasCreated = driver.FindElement(By.Id("loggedin")).Text.Contains(email);
            Assert.IsTrue(userWasCreated);
        }

        private void LogOutUser(IWebDriver driver)
        {
            driver.FindElement(By.Id("logoutForm")).Submit();
        }

        private void AddClient(IWebDriver driver, string client)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.FindElement(By.Id("clients")).Click();
            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Cliente")));
            driver.FindElement(By.Id("Cliente")).SendKeys(client);
            driver.FindElement(By.CssSelector(".btn.btn-info")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".btn.btn-success")));
            Assert.IsTrue(driver.FindElement(By.ClassName("table")).Text.C‌​ontains(client));
        }

        private void AddEmployee(IWebDriver driver, string employee)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.FindElement(By.Id("employees")).Click();
            driver.FindElement(By.CssSelector(".btn.btn-success")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Empleado")));
            driver.FindElement(By.Id("Empleado")).SendKeys(employee);
            SelectElement Client = new SelectElement(driver.FindElement(By.Id("SelectClient")));
            Client.SelectByIndex(1);
            driver.FindElement(By.CssSelector(".btn.btn-info")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".btn.btn-success")));
            Assert.IsTrue(driver.FindElement(By.ClassName("table")).Text.C‌​ontains(client));

        }
    }
}

