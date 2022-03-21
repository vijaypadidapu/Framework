using LTICSharpAutoFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


namespace Unicorn.Browsers
{
    public class ChromeBrowser
    {
        private IWebDriver driver;


        public IWebDriver getDriver()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string driverPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\Resources"));
            this.driver = new ChromeDriver(driverPath);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }
    }
}
