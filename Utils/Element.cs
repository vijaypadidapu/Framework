using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;


namespace LTICSharpAutoFramework.Utils
{
	public class Element
	{
		IWebDriver driver;
		ILog log = LogUtils.GetLogger(typeof(Element));

		public Element(IWebDriver driver)
		{
			this.driver = GlobalVariables.driver;
		}

		public IWebElement GetElement(String valueType, String value)
		{
			switch (valueType.ToLower())
			{
				case "id":
					return driver.FindElement(By.Id(value));

				case "name":
					return driver.FindElement(By.Name(value));

				case "classname":
					return driver.FindElement(By.ClassName(value));

				case "linktext":
					return driver.FindElement(By.LinkText(value));

				case "partiallinktext":
					return driver.FindElement(By.PartialLinkText(value));

				case "xpath":
					return driver.FindElement(By.XPath(value));

				case "tagname":
					return driver.FindElement(By.TagName(value));

				case "cssselector":
					return driver.FindElement(By.CssSelector(value));

				case "ng-model":
					return driver.FindElement(NgByModel.Id(value));

				case "ng-bind":
					return driver.FindElement(NgByBinding.Id(value));

				case "exactbinding":
					return driver.FindElement(NgByExactBinding.Id(value));

				//case "buttontext":
				//	return driver.FindElement(ByAngular.buttonText(value));

				//case "partialbuttontext":
				//  return driver.FindElement(ByAngular.partialButtontext(value));

				default:
					log.Debug("Select by : " + valueType + " is incorrect!");
					return null;

			}
		}
		public IWebElement GetElement(IWebElement parentElement, String valueType, String value)
		{
			switch (valueType.ToLower())
			{
				case "id":
					return parentElement.FindElement(By.Id(value));

				case "name":
					return parentElement.FindElement(By.Name(value));

				case "classname":
					return parentElement.FindElement(By.ClassName(value));

				case "linktext":
					return parentElement.FindElement(By.LinkText(value));

				case "partiallinktext":
					return parentElement.FindElement(By.PartialLinkText(value));

				case "xpath":
					return parentElement.FindElement(By.XPath(value));

				case "tagname":
					return parentElement.FindElement(By.TagName(value));

				case "cssselector":
					return parentElement.FindElement(By.CssSelector(value));

				case "ng-model":
					return parentElement.FindElement(NgByModel.Id(value));

				case "ng-bind":
					return parentElement.FindElement(NgByBinding.Id(value));

				case "exactbinding":
					return parentElement.FindElement(NgByExactBinding.Id(value));

				//case "buttontext":
				//	return parentElement.FindElement(ByAngular.buttonText(value));

				//case "partialbuttontext":
				//  return parentElement.FindElement(ByAngular.partialButtontext(value));

				default:
					log.Debug("Select by : " + valueType + " is incorrect!");
					return null;

			}

		}


		public IList<IWebElement> GetElements(String valueType, String value)
		{
			switch (valueType.ToLower())
			{
				case "id":
					return driver.FindElements(By.Id(value));

				case "name":
					return driver.FindElements(By.Name(value));

				case "classname":
					return driver.FindElements(By.ClassName(value));

				case "linktext":
					return driver.FindElements(By.LinkText(value));

				case "partiallinktext":
					return driver.FindElements(By.PartialLinkText(value));

				case "xpath":
					return driver.FindElements(By.XPath(value));

				case "tagname":
					return driver.FindElements(By.TagName(value));

				case "cssselector":
					return driver.FindElements(By.CssSelector(value));

				case "ng-model":
					return driver.FindElements(NgByModel.Id(value));

				case "ng-bind":
					return driver.FindElements(NgByBinding.Id(value));

				case "exactbinding":
					return driver.FindElements(NgByExactBinding.Id(value));

				//case "buttontext":
				//	return driver.FindElements(ByAngular.buttonText(value));

				//case "partialbuttontext":
				//  return driver.FindElements(ByAngular.partialButtontext(value));

				case "repeater":
					return driver.FindElements(NgByRepeater.Id(value));
				default:
					log.Debug("Select by : " + valueType + " is incorrect!");
					return null;
			}
		}

		public IList<IWebElement> GetElements(IWebElement parentElement, String valueType, String value)
		{
			switch (valueType.ToLower())
			{
				case "id":
					return parentElement.FindElements(By.Id(value));

				case "name":
					return parentElement.FindElements(By.Name(value));

				case "classname":
					return parentElement.FindElements(By.ClassName(value));

				case "linktext":
					return parentElement.FindElements(By.LinkText(value));

				case "partiallinktext":
					return parentElement.FindElements(By.PartialLinkText(value));

				case "xpath":
					return parentElement.FindElements(By.XPath(value));

				case "tagname":
					return parentElement.FindElements(By.TagName(value));

				case "cssselector":
					return parentElement.FindElements(By.CssSelector(value));

				case "ng-model":
					return parentElement.FindElements(NgByModel.Id(value));

				case "ng-bind":
					return parentElement.FindElements(NgByBinding.Id(value));

				case "exactbinding":
					return parentElement.FindElements(NgByExactBinding.Id(value));

				//case "buttontext":
				//	return parentElement.FindElements(ByAngular.buttonText(value));

				//case "partialbuttontext":
				//  return parentElement.FindElements(ByAngular.partialButtontext(value));

				case "repeater":
					return parentElement.FindElements(NgByRepeater.Id(value));
				default:
					log.Debug("Select by : " + valueType + " is incorrect!");
					break;

			}
			return null;

		}
	}
}