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

namespace LTICSharpAutoFramework
{
    [Binding]
    public class CommonStepDefs
    {
        public static RestApiHelper restApiHelper = new RestApiHelper();
        String option;
        int rowIndex;
        public static RestRequest restRequest;
        public static IRestResponse restResponse = null;
        public static Dictionary<string, string> excelDataMap = new Dictionary<string, string>();

        public static String requestJson;

        [Given(@"user set base uri for ""(.*)"" api")]
        public void GivenUserSetBaseUriForApi(String baseUriKey)
        {
            String baseUrl = CommonStepDefs.excelDataMap["$" + baseUriKey];

            restApiHelper.SetUrl(baseUrl,"");
        }
        
        [Given(@"user set base path for ""(.*)"" endpoint")]
        public void GivenUserSetBasePathForEndpoint(String basePathKey)
        {
            String basePath = CommonStepDefs.excelDataMap["$" + basePathKey];

            restApiHelper.SetUrl("",basePath);
        }
        /// <summary>
        ///TO MAKE METHOD FOR SETBASE
        /// </summary>
        /// <param name="methodType"></param>
        /// <param name="jsonCondition"></param>


        [When(@"user execute ""(.*)"" request ""(.*)"" json")]
        public void WhenUserExecuteRequestJson(String methodType, String jsonCondition)
        {
            if (methodType == "POST" )
            {
                if (jsonCondition == "with")
                {
                    restApiHelper.restRequest =  restApiHelper.CreatePostRequest(requestJson);
                }
            }
            else if(methodType == "PUT")
            {
                if (jsonCondition == "with")
                {
                    restApiHelper.CreatePutRequest(requestJson);
                }
            }
            else if (methodType == "GET")
            {
                if (jsonCondition == "without")
                {
                    restApiHelper.CreateGetRequest();
                }
            }
            else if (methodType == "DELETE")
            {
                if (jsonCondition == "without")
                {
                    restApiHelper.restRequest  = restApiHelper.CreateDeleteRequest();
                }
            }

            CommonStepDefs.restResponse = restApiHelper.GetResponse(restApiHelper.restRequest, restApiHelper.restClient);
        }

        [When(@"user form request json ""(.*)"" with test data in excel ""(.*)"" and sheet ""(.*)"" for scneario '(.*)'")]
        public void WhenUserFormRequestJsonWithTestDataInExcelAndSheetForScnearioValid_UpdatePet_(String jsonFileName, String excelFileName, String sheetName, String optionFromFF)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string excelFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"TestData\API\Excel\")) + excelFileName;
            string jsonFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"TestData\API\requestJson\")) + jsonFileName;

            option = optionFromFF;

            //Selecting sheet
            if (option.Contains("Invalid"))
            {
                sheetName = "Invalid";

            }
            else if (option.Contains("Valid"))
            {
                sheetName = "Valid";
            }
            else if (option.Contains("Duplicate"))
            {
                sheetName = "Duplicate";
            }
            else
            {
                Assert.Fail();
            }

            ExcelUtils.SetExcelFile(excelFilePath, sheetName);
            rowIndex = ExcelUtils.GetRowIndexByCellValue(0, option);
            ExcelUtils.CloseWorkBook();
            if (rowIndex > 0)
            {
                requestJson = RestApiHelperMethods.PutExcelRowDataToJSONReturnsString(jsonFilePath, excelFilePath,
                    sheetName, rowIndex);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Then(@"user verifies response code as ""(.*)""")]
        public void ThenUserVerifiesResponseCodeAs(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)restResponse.StatusCode, "post request failed");
        }
       
        
        [Then(@"user reset rest assured parameters")]
        public void ThenUserResetRestAssuredParameters()
        {
            //RestApiUtils.reset();
        }

        [Given(@"Read config excel data from ""(.*)"" and sheet ""(.*)"" for scenario ""(.*)""")]
        public void GivenReadConfigExcelDataFromAndSheetForScenario(string excelFile, string sheetName, string option)
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string excelFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"TestData\API\Excel\")) + excelFile;

            ExcelUtils.SetExcelFile(excelFilePath, sheetName);
            rowIndex = ExcelUtils.GetRowIndexByCellValue(0, option);
            ExcelUtils.CloseWorkBook();
            if (rowIndex > 0)
            {
                excelDataMap = RestApiHelperMethods.CreateMapFromExcelHeaders(excelFilePath,sheetName, rowIndex);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
