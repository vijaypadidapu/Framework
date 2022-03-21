using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Sockets;
using log4net;

namespace LTICSharpAutoFramework.Utils
{
	public class ExceptionHandler
	{
		private static Exception exception;
		static ILog log = LogUtils.GetLogger(typeof(ExceptionHandler));

		public static void HandleException(Object exceptionObj)
		{
			exception = (Exception)exceptionObj;
			log.Debug("### Exception Stack trace below ###", exception);

			/*Selenium Exception handled below*/

			if (exceptionObj is ElementNotVisibleException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!!  Although an element is present in the DOM ,it is not visible(cannot be interacted with) !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is ElementNotSelectableException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!!  Although an element is present in the DOM ,it is may be disabled(cannot be clicked/selected) !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is InvalidSelectorException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Selector used to find an element does not return a WebElement !!! For more detail check debug log." + exceptionObj.GetType().Name);
			}
			else if (exceptionObj is NoSuchElementException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Webdriver is unable to identify the elements !!! For more detail check debug log." + exceptionObj.GetType().Name);
			}
			else if (exceptionObj is NoSuchFrameException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Webdriver is switching to an invalid frame,which is not available !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is NoAlertPresentException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Webdriver is switching to an invalid alert,which is not available !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is NoSuchWindowException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Webdriver is switching to an invalid window,which is not available !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is StaleElementReferenceException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! The referenced element is no longer present or got changed on the DOM page !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is WebDriverTimeoutException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Wait time out exception !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is ElementClickInterceptedException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Element is not clickable !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is WebDriverException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Webdriver/Locator related exception !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}

			/*c# exception handled below*/

			else if (exceptionObj is NullReferenceException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception is raised when referring to the members of a null object !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is IndexOutOfRangeException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception thrown to indicate that an array has been accessed with an illegal index !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is FileNotFoundException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception is raised when a file is not accessible or does not open!!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is IOException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception thrown when an input-output operation failed or interrupted !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is ThreadInterruptedException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception thrown when a thread is waiting,sleeping, or doing some processing , and it is interrupted !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}

			/*JSON Exception */

			else if (exceptionObj is JsonReaderException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception occured while parsing the json !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is JsonException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Thrown to indicate a problem with the JSON API !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			else if (exceptionObj is SocketException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! Exception occured while attempting to connect a socket to a remote address and port !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}
			/*SQL Exception*/

			else if (exceptionObj is SqlException)
			{
				Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss") + "!!! SQL Exception has occured !!! For more detail check debug log." + exceptionObj.GetType().Name);

			}

			/*Other Exception*/
			else
			{
				//Assert.Fail(CalendarUtils.GetCalendarUtilsObject().GetTimeStamp("MM/dd/yyyy HH:mm:ss")+ "!!! Failure due to exception !!! For more detail check debug log."+ exceptionObj.GetType().Name);

			}


		}
	}
}