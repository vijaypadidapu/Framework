using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using System.IO;

namespace LTICSharpAutoFramework.Utils
{
	class CaptureScreenshot
	{
		static int ScreenCounter = 0;

		public string CaptureScreen(IWebDriver Browser, string DirecPath, string FileName, ImageFormat Format)
		{
			StringBuilder TimeAndDate = new StringBuilder(DateTime.Now.ToString());
			TimeAndDate.Replace("/", "_");
			TimeAndDate.Replace(":", "_");
			DirectoryInfo Validation = new DirectoryInfo(DirecPath);
			string screenshotPath = DirecPath + "\\" + FileName + "_" + ScreenCounter.ToString() + "_" + TimeAndDate.ToString() + "." + Format;
			if (Validation.Exists == true)
			{
				((ITakesScreenshot)Browser).GetScreenshot().SaveAsFile(screenshotPath);

			}
			else
			{
				((ITakesScreenshot)Browser).GetScreenshot().SaveAsFile(screenshotPath);

			}
			return screenshotPath;
		}
	}
}