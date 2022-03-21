using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using OpenQA.Selenium;
using System.IO;
using System.Net;
using LTICSharpAutoFramework.Utils;

namespace LTICSharpAutoFramework.Utils
{
    class CheckBrokenLinks
    {
        ILog log = LogUtils.GetLogger(typeof(CheckBrokenLinks));

        public bool Execute(Dictionary<String, Object>param)
        {
            if (param.ContainsKey("driver"))
            {
                IWebDriver driver = (IWebDriver)param["driver"];
                return CheckBrokenLinksMethod(driver, driver.Url);

            }
            return false;
        }
        public bool CheckBrokenLinksMethod(IWebDriver driver, String baseurl)
        {
            String url;
            HttpWebRequest httpWebReq = null;
            int httpStatusCode = 200;

            IList<IWebElement> links = driver.FindElements(By.TagName("a"));

            foreach(IWebElement webElement in links)
            {
                url = webElement.GetAttribute("href");
                log.Debug("URL : " + url);
                if(string.IsNullOrEmpty(url))
                {
                    log.Debug("Response : URL is either not configured for anchor tag or it is empty");
                    continue;
                }
                if(!url.StartsWith(baseurl))
                {
                    log.Debug("Response : URL belongs to another domain, skipping it");
                    continue;
                }

                try
                {
                    httpWebReq = (HttpWebRequest)WebRequest.Create(url);
                    httpWebReq.Method = "HEAD";
                    HttpWebResponse response = (HttpWebResponse)httpWebReq.GetResponse();
                    httpStatusCode = (int)response.StatusCode;

                    if(httpStatusCode >= 400)
                    {
                        log.Debug("Response : url is a broken link");

                    }
                    else
                    {
                        log.Debug("Response : url is a valid link");

                    }
                }
                catch(IOException e)
                {
                    ExceptionHandler.HandleException(e);
                }

            }
            return true;
        }

    }
}
