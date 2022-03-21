using LTICSharpAutoFramework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Reflection;



namespace Unicorn.Browsers
{
	class IEBrowser
	{
		private IWebDriver driver;

		public IWebDriver getDriver()
		{
			string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string driverPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\Resources\Drivers"));
			this.driver = new InternetExplorerDriver(driverPath);
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(GlobalVariables.waitTime);
			driver.Manage().Cookies.DeleteAllCookies();

			return driver;
		}
	}
}