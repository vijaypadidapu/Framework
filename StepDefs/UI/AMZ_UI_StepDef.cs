using LTICSharpAutoFramework.Utils;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using System.Threading;
using System.Collections.Generic;
using LTICSharpAutoFramework.Pages.Web;


namespace LTICSharpAutoFramework.StepDefs.UI
{
    [Binding]
    public class AMZ_UI_StepDef
    {
        public static Dictionary<string, string> excelDataMap = new Dictionary<string, string>();

        AMZPage amzPage = new AMZPage();

        [Given(@"Read locator excel data from ""(.*)"" and sheet ""(.*)"" for scenario ""(.*)""")]
        public void GivenReadLocatorExcelDataFromAndSheetForScenario(string excelFile, string sheetName, string option)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string excelFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"ObjectRepo\")) + excelFile;

            ExcelUtils.SetExcelFile(excelFilePath, sheetName);
            int rowIndex = ExcelUtils.GetRowIndexByCellValue(0, option);
            ExcelUtils.CloseWorkBook();
            if (rowIndex > 0)
            {
                WebActions.excelLocatorMap = RestApiHelperMethods.CreateMapFromExcelHeaders(excelFilePath, sheetName, rowIndex);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Given(@"user launches the browser")]
        public void GivenUserLaunchesTheBrowser()
        {
            //Write now getting browser is opened in the Hooks
            //We can separate the code to close the browser through step if required
            //The below code is add any specific message in the Extent Report
            Hooks.stepNode.Info("Opening Amazon URL");
            WebActions.OpenURL("https://www.amazon.in/");
        }

        [Given(@"amazon application is available")]
        public void GivenAmazonApplicationIsAvailable()
        {
            //you can logic to verify if we have land up on the Amazon Home Page
            Thread.Sleep(100);
        }

        [When(@"user clicks on sign button on home page")]
        public void WhenUserClicksOnSignButtonOnHomePage()
        {
            amzPage.clickOnAccountLink();
        }

        [When(@"user enters invalid user id and clicks on '(.*)' button")]
        public void WhenUserEntersInvalidUserIdAndClicksOnButton(string p0)
        {
            //User name is hard coded in the Page...
            amzPage.enterUserName();
            amzPage.clickOnContinueBtn();
        }

        [Then(@"user verifies error message as ""(.*)"" on Login page")]
        public void ThenUserVerifiesErrorMessageAsOnLoginPage(string p0)
        {
            amzPage.readErrorMsg();
        }

        [Then(@"user closes the browser")]
        public void ThenUserClosesTheBrowser()
        {
            //Write now getting browser is closed in the Hooks
            //We can separate the code to close the browser through step if required
        }
    }
}
