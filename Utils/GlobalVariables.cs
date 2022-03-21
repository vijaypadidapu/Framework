using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Browsers;

namespace LTICSharpAutoFramework.Utils
{
	class GlobalVariables
	{
		public static IWebDriver driver;
		public static String browserName;
		public static Browser browser;
		public static int waitTime = 20;
		public static TimeSpan waitTimeSpan = new TimeSpan(0, 0, 25);
		public static String currentScenarioName;
		public static List<DBRowTO> eleData = null;
		public static List<DBRowTO> testData = null;
		public static String applicationName;
		public static String applicationURL = null;

	}
}