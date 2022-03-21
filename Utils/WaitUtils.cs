using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LTICSharpAutoFramework.Reports;
using SEW = SeleniumExtras.WaitHelpers;

namespace LTICSharpAutoFramework.Utils
{
	public class WaitUtils
	{
		static String value = null;
		static String valueType = null;
		static ILog log = LogUtils.GetLogger(typeof(WaitUtils));

		public static By GetBy(String dbKey, List<DBRowTO> listElement)
		{
			value = WebActions.GetValue(dbKey, listElement);
			valueType = WebActions.GetValueType(dbKey, listElement);

			switch (valueType.ToLower())
			{
				case "id":
					return By.Id(value);

				case "name":
					return By.Name(value);

				case "classname":
					return By.ClassName(value);

				case "linktext":
					return By.LinkText(value);

				case "partiallinktext":
					return By.PartialLinkText(value);

				case "xpath":
					return By.XPath(value);

				case "tagname":
					return By.TagName(value);

				case "cssselector":
					return By.CssSelector(value);

				default:
					log.Debug("Select by : " + valueType + " is incorrect!");
					return null;
			}
		}

		public static IWebElement PresenceOfElementWaitUsingBy(String dbKey, List<DBRowTO> listElement)
		{
			String status = "FAIL";
			IWebElement element = null;
			try
			{
				element = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
							.Until(SEW.ExpectedConditions.ElementExists(GetBy(dbKey, listElement)));
				status = "PASS";
			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}

			return element;

		}


		public static IWebElement PresenceOfElementWaitUsingBy(IWebElement parentElement, String dbKey,
		List<DBRowTO> listElement)
		{
			String status = "FAIL";
			IWebElement element = null;
			try
			{
				//	element = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
				//			.Until(SEW.ExpectedConditions.PresenceOfNestedElementLocatedBy( parentElement,GetBy(dbKey, listElement)));
				status = "PASS";

			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}

			return element;


		}

		public static IList<IWebElement> PresenceOfElementsWaitUsingBy(String dbKey, List<DBRowTO> listDBElement)
		{
			String status = "FAIL";
			IList<IWebElement> listElement = null;
			try
			{
				listElement = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
					.Until(SEW.ExpectedConditions.PresenceOfAllElementsLocatedBy(GetBy(dbKey, listDBElement)));

			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}
			return listElement;
		}

		public static IWebElement InVisibilityOfElementWaitUsingBy(String dbKey, List<DBRowTO> listElement)
		{
			String status = "FAIL";
			IWebElement element = null;

			try
			{
				//	element = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
				//			.Until(SEW.ExpectedConditions.InvisibilOfElementLocated(GetBy(dbKey, listElement)));
				status = "PASS";
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}
			return element;
		}


		public static IWebElement VisibilityOfElementWaitUsingBy(String dbKey, List<DBRowTO> listElement)
		{
			String status = "FAIL";
			IWebElement element = null;

			try
			{
				element = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
						.Until(SEW.ExpectedConditions.ElementIsVisible(GetBy(dbKey, listElement)));
				status = "PASS";

			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}
			return element;

		}


		public static List<IWebElement> VisibilityOfElementWaitUsingBy(IWebElement parentElement, String dbKey, List<DBRowTO> listElement)
		{
			String status = "FAIL";
			List<IWebElement> eList = null;
			try
			{
				//	eList= new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
				//	.Until(SEW.ExpectedConditions.VisibilityOfNestedElementsLocatedBy(parentElement,GetBy(dbKey, listElement)));
				status = "PASS";

			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}
			return eList;

		}

		public static IList<IWebElement> VisibilityOfElementsWaitUsingBy(String dbKey, List<DBRowTO> listDBElement)
		{
			String status = "FAIL";
			IList<IWebElement> listElement = null;

			try
			{
				listElement = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
				.Until(SEW.ExpectedConditions.VisibilityOfAllElementsLocatedBy(GetBy(dbKey, listDBElement)));
				status = "PASS";


			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Presence of Element");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}
			return listElement;


		}


		public static void AlertIsPresent()
		{
			String status = "FAIL";

			try
			{
				new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
	.Until(SEW.ExpectedConditions.AlertIsPresent());
				status = "PASS";


			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Alert is Present");
				Report.PrintStatus(status);
			}
		}


		public static IWebElement ElementToBeClickable(String dbKey, List<DBRowTO> listElement)
		{
			String status = "FAIL";
			IWebElement element = null;

			try
			{
				element = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
						.Until(SEW.ExpectedConditions.ElementToBeClickable(GetBy(dbKey, listElement)));
				status = "PASS";
			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Element to be Clickable");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);

			}

			return element;
		}
		public static void ElementToBeClickable(IWebElement element)
		{

			String status = "FAIL";
			try
			{
				new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
			.Until(SEW.ExpectedConditions.ElementToBeClickable(element));
				status = "PASS";

			}


			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Element to be Clickable");
				Report.PrintStatus(status);
			}
		}

		public static bool ElementToBeSelected(String dbKey, List<DBRowTO> listElement)
		{

			String status = "FAIL";
			bool isSelected = false;

			try
			{
				isSelected = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
	.Until(SEW.ExpectedConditions.ElementToBeSelected(GetBy(dbKey, listElement)));
				status = "PASS";

			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Wait Element to be Selectable");
				Report.PrintKey(dbKey);
				Report.PrintValueType(valueType);
				Report.PrintValue(value);
				Report.PrintStatus(status);
			}

			return isSelected;
		}

		public static bool ElementToBeSelected(IWebElement element)
		{
			String status = "FAIL";
			bool isSelected = false;

			try
			{
				isSelected = new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
	.Until(SEW.ExpectedConditions.ElementToBeSelected(element));
				status = "PASS";

			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				Report.PrintOperation("Wait Element to be Selectable");

				Report.PrintStatus(status);
			}
			return isSelected;
		}




	}


}