using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Reflection;

namespace LTICSharpAutoFramework.Utils
{
	class EdgeBrowser
	{
		private IWebDriver driver;

		public IWebDriver getDriver()
		{
			string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string driverPath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\Resources\Drivers"));
			this.driver = new EdgeDriver(driverPath);
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
			driver.Manage().Window.Maximize();
			driver.Manage().Cookies.DeleteAllCookies();
			return driver;
		}
	}
}