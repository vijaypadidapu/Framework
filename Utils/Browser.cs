using LTICSharpAutoFramework.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Browsers
{
    public class Browser
    {
        public static IWebDriver driver;

        public Browser(String browserName)
        {

            switch (browserName)
            {
                case "chrome":
                    {
                        driver = new ChromeBrowser().getDriver();
                        break;
                    }
                case "ie":
                    {
                        driver = new IEBrowser().getDriver();
                        break;
                    }
                case "edge":
                    {
                        driver = new EdgeBrowser().getDriver();
                        break;
                    }
                default:
                    {
                        break;
                    }

            }


        }

        public IWebDriver GetDriver()
        {
            return driver;
        }


    }
}
