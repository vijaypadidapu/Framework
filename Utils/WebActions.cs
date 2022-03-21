using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using LTICSharpAutoFramework.Reports;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using SEW = SeleniumExtras.WaitHelpers;
using System.Reflection;
using AventStack.ExtentReports;
using System.Windows.Forms;

namespace LTICSharpAutoFramework.Utils
{
    public class WebActions
    {
        static IWebDriver driver;
        static int timeout;
        static IJavaScriptExecutor js;

        readonly List<DBRowTO> listElement;
        public static Dictionary<string, string> excelLocatorMap = new Dictionary<string, string>();

        public WebActions()
        {
        }

        public WebActions(List<DBRowTO> argListElement)
        {
            this.listElement = argListElement;
        }
        public static void HighlightElement(IWebElement ele)
        {
            String status = "FAIL";
            try
            {
                js.ExecuteScript("arguments[0].style.border='3px solid red'", ele);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("HighlightElement");
                Report.PrintStatus(status);
            }
        }

        public static void ZoomInZoomOut(Double zoomPercentage)
        {
            String status = "FAIL";
            try
            {
                js.ExecuteScript("document.body.style.zoom = '" + zoomPercentage + "'");
                status = "PASS";
            }

            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Zoom IN/Out");
                Report.PrintStatus(status);
            }
        }

        public static void Initialize(IWebDriver driver, int timeout)
        {
            WebActions.driver = driver;
            WebActions.timeout = timeout;
            WebActions.js = (IJavaScriptExecutor)driver;
        }

        public static void SendKeyDown(int count)
        {
            int index = 0;
            while (index < count)
            {
                Actions action = new Actions(WebActions.driver);
                action.SendKeys(OpenQA.Selenium.Keys.Down).Perform();
                ++index;
            }
        }

        public static void SendKeyEnter(IWebElement ele)
        {
            Actions action = new Actions(WebActions.driver);
            action.SendKeys(OpenQA.Selenium.Keys.Enter).Perform();
        }

        public static void SendKeyTab()
        {
            Actions action = new Actions(WebActions.driver);
            String str = OpenQA.Selenium.Keys.Tab;
            action.SendKeys(OpenQA.Selenium.Keys.Tab).Perform();
        }

        public static void SendKeyDownArrow()
        {
            Actions action = new Actions(WebActions.driver);
            String str = OpenQA.Selenium.Keys.ArrowDown;
            action.SendKeys(OpenQA.Selenium.Keys.ArrowDown).Perform();
        }

        public static void ShiftTab(int count)
        {
            int i = 0;
            Actions action = new Actions(WebActions.driver);
            String tab = OpenQA.Selenium.Keys.Tab;
            String shift = OpenQA.Selenium.Keys.Shift;

            while (i < count)
            {
                action.SendKeys(OpenQA.Selenium.Keys.ArrowLeft).Perform();
                ++i;
            }

        }

        public static void Click(IWebElement element)
        {
            String status = "FAIL";
            try
            {
                element.Click();
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Click");
                Report.PrintStatus(status);
            }
        }
        public static void ContextClick(IWebElement element)
        {
            String status = "FAIL";
            try
            {
                new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
                .Until(SEW.ExpectedConditions.ElementToBeClickable(element));
                new Actions(driver).ContextClick(element).Perform();
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Context Click");
                Report.PrintStatus(status);
            }
        }

        public static void DoubleClick(IWebElement element)
        {
            String status = "FAIL";
            try
            {
                new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
                .Until(SEW.ExpectedConditions.ElementToBeClickable(element));
                new Actions(driver).DoubleClick(element).Perform();
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Double Click");
                Report.PrintStatus(status);
            }
        }
        public static void Clear(IWebElement element)
        {
            String status = "FAIL";
            try
            {
                new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
                .Until(SEW.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(element.Text)));
                element.Clear();
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Clear");
                Report.PrintStatus(status);
            }
        }
        public static void SendKeys(IWebElement element, String value)
        {
            element.Clear();
            String status = "FAIL";
            try
            {
                element.SendKeys(value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("sendKeys");
                Report.PrintValue(value);
                Report.PrintStatus(status);
            }
        }
        public static void SendKeysCharByChar(IWebElement element, String value)
        {
            element.Clear();
            String status = "FAIL";
            try
            {
                for (int i = 0; i < value.Length; i++)
                {
                    element.SendKeys(value[i].ToString());
                    Thread.Sleep(100);
                }
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("sendKeyscharbyChar");
                Report.PrintValue(value);
                Report.PrintStatus(status);
            }
        }
        public static void SendKeysCharByCharforAmount(IWebElement element, String value)
        {
            element.Clear();
            String status = "FAIL";
            String append = "";
            try
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i].ToString() == ".")
                    {
                        append = ".";
                    }
                    else
                    {
                        element.SendKeys(append + value[i].ToString());
                    }
                    Thread.Sleep(100);
                }
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("sendKeyscharbyCharforAmount");
                Report.PrintValue(value);
                Report.PrintStatus(status);
            }
        }
        public static void SendKeysCharByCharforLookup(IWebElement element, String value)
        {
            element.Clear();
            String status = "FAIL";

            try
            {
                for (int i = 0; i < value.Length; i++)
                {
                    element.SendKeys(value[i].ToString());
                    Thread.Sleep(2000);
                }
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("sendKeyscharbyChar");
                Report.PrintValue(value);
                Report.PrintStatus(status);
            }
        }
        public static void CloseBrowser()
        {

            String status = "FAIL";

            try
            {
                driver.Close();
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Close Browser");

                Report.PrintStatus(status);
            }
        }
        public static void QuitBrowser()
        {

            String status = "FAIL";

            try
            {
                driver.Quit();

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Quit Browser");

                Report.PrintStatus(status);
            }
        }
        public static bool IsAlertPresent()
        {
            bool isAlertPresent = false;
            String status = "FAIL";
            try
            {
                IAlert alert = SEW.ExpectedConditions.AlertIsPresent().Invoke(driver);

                if (alert != null)
                    isAlertPresent = true;

                else
                    isAlertPresent = false;

                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("isAlertPresent");

                Report.PrintStatus(status);
            }

            return isAlertPresent;
        }
        public static void AcceptAlert(IAlert alert)
        {

            String status = "FAIL";

            try
            {
                alert.Accept();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Accept Alert");

                Report.PrintStatus(status);
            }
        }
        public static void DismissAlert(IAlert alert)
        {

            String status = "FAIL";

            try
            {
                alert.Dismiss();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Dismiss Alert");

                Report.PrintStatus(status);
            }
        }
        public static String GetAlertAlert(IAlert alert)
        {
            String alertText = null;
            String status = "FAIL";

            try
            {
                alertText = alert.Text;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Getting Alert Text");

                Report.PrintStatus(status);
            }
            return alertText;
        }
        public static void MoveToElement(IWebElement element)
        {

            String status = "FAIL";

            try
            {
                new Actions(driver).MoveToElement(element).Perform();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation(" Javascript Click");

                Report.PrintStatus(status);
            }
        }
        public static void DragAndDrop(IWebElement sourceEle, IWebElement destinationEle)
        {

            String status = "FAIL";

            try
            {
                new Actions(driver).DragAndDrop(sourceEle, destinationEle).Perform();

                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Drag And Drop  ");

                Report.PrintStatus(status);
            }
        }
        public static String GetText(IWebElement element)
        {

            String text = "undefined";
            String status = "FAIL";

            try
            {
                text = element.Text;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Text ");
                Report.PrintValue(text);
                Report.PrintStatus(status);
            }
            return text;
        }
        public static String GetTextboxValue(IWebElement element)
        {

            String text = "undefined";
            String status = "FAIL";

            try
            {
                text = element.GetAttribute("value");
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Value ");
                Report.PrintValue(text);
                Report.PrintStatus(status);
            }
            return text;
        }

        public static String GetPagetitle()
        {

            String title = null;
            String status = "FAIL";

            try
            {
                title = driver.Title;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Getting Page Title ");

                Report.PrintStatus(status);
            }
            return title;
        }
        public static IWebElement ExpandRootElement(IWebElement element)
        {
            IWebElement eExpanded = null;
            String status = "FAIL";

            try
            {
                eExpanded = (IWebElement)js.ExecuteScript("return arguments[0].shadowRoot", element);

                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Expand Shawdowroot ");

                Report.PrintStatus(status);
            }
            return eExpanded;
        }

        public static void HighligthElement(IWebElement element)
        {

            String status = "FAIL";

            try
            {
                js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');",
                element);

                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Hightlight Element ");

                Report.PrintStatus(status);
            }
            
        }

        public static bool IsDisplayed(IWebElement element)
        {
            bool isDisplayed = false;
            String status = "FAIL";

            try
            {
                isDisplayed = element.Displayed;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Is Displayed ");

                Report.PrintStatus(status);
            }
            return isDisplayed;
        }
        public static bool IsEnabled(IWebElement element)
        {
            bool isEnabled = false;
            String status = "FAIL";

            try
            {
                isEnabled = element.Enabled;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Is Enabled ");

                Report.PrintStatus(status);
            }
            return isEnabled;
        }
        public static bool IsSelected(IWebElement element)
        {
            bool isSelected = false;
            String status = "FAIL";

            try
            {
                isSelected = element.Selected;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Is Selected ");

                Report.PrintStatus(status);
            }
            return isSelected;
        }
        public static void JsClick(IWebElement element)
        {
            String status = "FAIL";
            try
            {
                new WebDriverWait(GlobalVariables.driver, GlobalVariables.waitTimeSpan)
                .Until(SEW.ExpectedConditions.ElementToBeClickable(element));
                js.ExecuteScript("arguments[0].click();", element);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Javascript Click ");

                Report.PrintStatus(status);
            }
        }
        public static void OpenURL()
        {

            String status = "FAIL";

            try
            {
                driver.Navigate().GoToUrl(GlobalVariables.applicationURL);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Open URL   ");
                Report.PrintValue(GlobalVariables.applicationURL);
                Report.PrintStatus(status);
            }

        }
        public static void OpenURL(String url)
        {

            String status = "FAIL";

            try
            {
                driver.Navigate().GoToUrl(url);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Open URL   ");
                Report.PrintValue(url);
                Report.PrintStatus(status);
            }

        }
        public static void ScrollToBottom()
        {

            String status = "FAIL";

            try
            {
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Scroll To Bottom ");

                Report.PrintStatus(status);
            }

        }
        public static void ScrollByPixel(IWebElement element)
        {

            String status = "FAIL";

            try
            {
                js.ExecuteScript("window.scrollBy(50,0)", "");
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Scroll By Pixel ");

                Report.PrintStatus(status);
            }

        }

        public static void ScrollToElement(IWebElement element)
        {

            String status = "FAIL";

            try
            {
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Scroll To Element ");

                Report.PrintStatus(status);
            }

        }

        public static void SwitchFrameByName(String name)
        {

            String status = "FAIL";

            try
            {
                driver.SwitchTo().Frame(name);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Switch Frame By Name ");
                Report.PrintValue(name);
                Report.PrintStatus(status);
            }

        }
        public static void SwitchFrameByIndex(int index)
        {

            String status = "FAIL";

            try
            {
                driver.SwitchTo().Frame(index);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Switch Frame By Index ");
                Report.PrintValue(index + "");
                Report.PrintStatus(status);
            }

        }

        public static void SwitchFrameByWebElement(IWebElement frameElement)
        {

            String status = "FAIL";

            try
            {
                driver.SwitchTo().Frame(frameElement);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Switch Frame By Index ");

                Report.PrintStatus(status);
            }

        }


        public static void SwitchToDefaultConent()
        {

            String status = "FAIL";

            try
            {
                driver.SwitchTo().DefaultContent();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Switch to Default Content ");

                Report.PrintStatus(status);
            }

        }

        public static void SelectByValue(SelectElement select, String value)
        {

            String status = "FAIL";

            try
            {
                select.SelectByValue(value);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Select  By Value ");
                Report.PrintValue(value);
                Report.PrintStatus(status);
            }

        }

        public static void SelectByIndex(SelectElement select, int index)
        {
            
            String status = "FAIL";

            try
            {
                select.SelectByIndex(index);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Select  By Index ");
                Report.PrintValue(index + "");
                Report.PrintStatus(status);
            }

        }
        public static void SelectByVisibleText(SelectElement select, String visibleText)
        {

            String status = "FAIL";

            try
            {
                select.SelectByText(visibleText);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Select  By Visible Text ");
                Report.PrintValue(visibleText);
                Report.PrintStatus(status);
            }

        }
        public static IList<IWebElement> GetSelectOptions(SelectElement select)
        {

            String status = "FAIL";
            IList<IWebElement> options = null;
            try
            {
                options = select.Options;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get  Select options ");

                Report.PrintStatus(status);
            }
            return options;
        }

        public static String TakeScreenshot()
        {
            String path = null;
            String status = "FAIL";
            try
            {
                CaptureScreenshot screenshot = new CaptureScreenshot();
                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path = screenshot.CaptureScreen(driver, assemblyPath, "ErrorScreen", ImageFormat.Jpeg);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("TakeScreenshot");

                Report.PrintStatus(status);
            }
            return path;
        }
        public static String CaptureEntireScreen()
        {
            String path = null;
            String status = "FAIL";
            try
            {
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("TakeScreentshot");

                Report.PrintStatus(status);
            }
            return path;
        }

        public static void AttacheScreenshotToReport(string message)
        {
            Hooks.stepNode.Pass(message, MediaEntityBuilder.CreateScreenCaptureFromPath(WebActions.TakeScreenshot()).Build());
        }

        public static void AddLogToReport(string message)
        {
            Hooks.stepNode.Pass(message);
        }
        public static void WaitForPageLoad()
        {

            String status = "FAIL";
            bool pageLoad = false;
            try
            {
                while (pageLoad == false)
                {
                    pageLoad = js.ExecuteScript("return document.readyState").ToString().Equals("complete");

                    status = "PASS";
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Wait For Page Load");

                Report.PrintStatus(status);
            }

        }

        public Object GetElementByJavascript(String key)
        {
            Object obj = null;
            String valueType = WebActions.GetValueType(key, listElement);
            String value = WebActions.GetValue(key, listElement);

            String status = "FAIL";
            try
            {

                Thread.Sleep(1000);
                obj = js.ExecuteScript(value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("JS Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return obj;
        }
        public IWebElement GetElement(String key)
        {
            IWebElement webElement = null;
            String valueType = "undefined";
            String value = "undefined";

            String status = "FAIL";
            try
            {

                valueType = WebActions.GetValueType(key, listElement);
                value = WebActions.GetValue(key, listElement);

                Element element = new Element(driver);
                webElement = element.GetElement(valueType, value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return webElement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IWebElement GetElementByExcelXpath(String key)
        {
            IWebElement webElement = null;
            String valueType = string.Empty;
            string value = string.Empty;

            String status = "FAIL";
            try
            {

                valueType = "xpath";
                value = excelLocatorMap["$" + key];
                Element element = new Element(driver);
                webElement = element.GetElement(valueType, value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return webElement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Object GetElementByExcelJavaScript(String key)
        {
            Object obj = null;
            String valueType = string.Empty;
            string value = string.Empty;

            String status = "FAIL";
            try
            {

                value = excelLocatorMap["$" + key];
                Thread.Sleep(1000);
                obj = js.ExecuteScript(value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return obj;
        }
        public IWebElement GetElement(String locator, String locatorValue)
        {
            IWebElement element = null;


            String status = "FAIL";
            try
            {



                Element objElement = new Element(driver);
                element = objElement.GetElement(locator, locatorValue);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Elements");

                Report.PrintValue(locatorValue);
                Report.PrintValueType(locator);
                Report.PrintStatus(status);
            }
            return element;
        }

        public IList<IWebElement> GetElements(String key)
        {
            IList<IWebElement> listWebElement = null;
            String valueType = "undefined";
            String value = "undefined";

            String status = "FAIL";
            try
            {
                Element element = new Element(driver);
                value = WebActions.GetValue(key, listElement);
                valueType = WebActions.GetValueType(key, listElement);



                listWebElement = element.GetElements(valueType, value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return listWebElement;
        }

        public IList<IWebElement> GetElements(String locator, String locatorValue)
        {
            IList<IWebElement> lstElement = null;


            String status = "FAIL";
            try
            {
                Element objElement = new Element(driver);



                lstElement = objElement.GetElements(locator, locatorValue);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Elements");

                Report.PrintValue(locatorValue);
                Report.PrintValueType(locator);
                Report.PrintStatus(status);
            }
            return lstElement;
        }

        public IWebElement GetElementFromParentElement(IWebElement parentElement, String key)
        {
            IWebElement webElement = null;
            String valueType = "undefined";
            String value = "undefined";

            String status = "FAIL";
            try
            {
                Element element = new Element(driver);
                value = WebActions.GetValue(key, listElement);
                valueType = WebActions.GetValueType(key, listElement);


                webElement = element.GetElement(parentElement, valueType, value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Element");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return webElement;
        }
        public IList<IWebElement> GetElementsFromParentElement(IWebElement parentElement, String key)
        {
            IList<IWebElement> listWebElement = null;
            String valueType = "undefined";
            String value = "undefined";

            String status = "FAIL";
            try
            {
                Element element = new Element(driver);
                value = WebActions.GetValue(key, listElement);
                valueType = WebActions.GetValueType(key, listElement);


                listWebElement = element.GetElements(parentElement, valueType, value);
                status = "PASS";
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Elements");
                Report.PrintKey(key);
                Report.PrintValue(value);
                Report.PrintValueType(valueType);
                Report.PrintStatus(status);
            }
            return listWebElement;
        }

        public static bool CheckBrokenLinks()
        {
            bool check = false;
            String status = "FAIL";
            try
            {
                CheckBrokenLinks checkBrokenLinks = new CheckBrokenLinks();
                check = checkBrokenLinks.CheckBrokenLinksMethod(driver, driver.Url);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Checking broken links");

                Report.PrintStatus(status);
            }
            return check;

        }
        public static Object ExecuteJavaScript(String javaScript)
        {
            Object scriptReturn = null;
            String status = "FAIL";
            try
            {
                scriptReturn = js.ExecuteScript(javaScript);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Execute Javascript");
                Report.PrintValue(javaScript);
                Report.PrintStatus(status);
            }
            return scriptReturn;

        }
        public static Object ExecuteJavaScript(String javaScript, IWebElement element)
        {
            Object scriptReturn = null;
            String status = "FAIL";
            try
            {
                scriptReturn = js.ExecuteScript(javaScript, element);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Execute Javascript with Argument");
                Report.PrintValue(javaScript);
                Report.PrintStatus(status);
            }
            return scriptReturn;

        }
        public static String GetValue(String dbKey, List<DBRowTO> data)
        {
            foreach (DBRowTO temp in data)
            {
                if (temp.Key.Equals(dbKey))
                {
                    return temp.Value;
                }
                
            }
            return "undefined";
        }
        public static String GetValueType(String dataKey, List<DBRowTO> data)
        {
            foreach (DBRowTO temp in data)
            {
                if (temp.Key.Equals(dataKey))
                {
                    return temp.ValueType;
                }
                
            }
            return "user defined";
        }

        public static void NavigateForward()
        {

            String status = "FAIL";

            try
            {
                driver.Navigate().Forward();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Navigate Forward");

                Report.PrintStatus(status);
            }

        }
        public static void NavigateBackward()
        {

            String status = "FAIL";

            try
            {
                driver.Navigate().Back();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Navigate Backward");

                Report.PrintStatus(status);
            }

        }
        public static void Refresh()
        {

            String status = "FAIL";

            try
            {
                driver.Navigate().Refresh();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Refresh");

                Report.PrintStatus(status);
            }

        }
        public static void CopyStringToClipboard(String str)
        {

            String status = "FAIL";

            try
            {
                string fileContent = File.ReadAllText(str);
                Clipboard.SetText(fileContent);
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Copy To Clipboard");

                Report.PrintStatus(status);
            }

        }
        public static IList<String> GetWindowsHandles()
        {
            IList<String> handles = null;
            String status = "FAIL";

            try
            {
                handles = driver.WindowHandles;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Windows Handles");

                Report.PrintStatus(status);
            }
            return handles;
        }

        public static void SwitchToWindow(String windowHandle)
        {

            String status = "FAIL";

            try
            {
                WebActions.driver = driver.SwitchTo().Window(windowHandle);
                WebActions.js = (IJavaScriptExecutor)driver;
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Switch To Window");

                Report.PrintStatus(status);
            }

        }
        public static void AttachScreenshotToExtentReport()
        {

            String status = "FAIL";

            try
            {
                WebActions.TakeScreenshot();
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Attached Screenshot To Extent Report");

                Report.PrintStatus(status);
            }

        }


        public static Dictionary<String, String> GetBrowserLocalStorage()
        {
            long totalKeys = (long)js.ExecuteScript("return window.localStorage.lenght");

            String key;


            String status = "FAIL";
            Dictionary<String, String> mapKeyValue = new Dictionary<String, String>();

            try
            {
                for (long index = 0; index < totalKeys; index++)
                {
                    key = (String)js.ExecuteScript("return window.localStorage.key(" + index + ")");
                    mapKeyValue.Add(key, (String)js.ExecuteScript("return window.localStorage.getItem('" + key + "')"));
                }
                status = "PASS";

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                Report.PrintOperation("Get Browser Local Storage");

                Report.PrintStatus(status);
            }

            return mapKeyValue;

        }
        public static String GetTestDataFromDB(String dbKey)
        {
            return WebActions.GetValue(dbKey, GlobalVariables.testData);

        }

    }
}