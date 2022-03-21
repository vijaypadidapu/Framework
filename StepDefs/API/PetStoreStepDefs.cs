using LTICSharpAutoFramework.Utils;
using Newtonsoft.Json.Linq;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using RestSharp;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;



namespace LTICSharpAutoFramework
{
    [Binding]
    public class PetStoreStepDefs
    {
        public static String petId;
        RestApiHelper restApiHelper = new RestApiHelper();
        public RestRequest restRequest;
        public static String requestJson;
        [Given(@"user provide pet ID to retrive employee information")]
        public void GivenUserProvidePetIDToRetriveEmployeeInformation()
        {
            CommonStepDefs.restApiHelper.restClient.BaseUrl = new Uri(CommonStepDefs.restApiHelper.restClient.BaseUrl.AbsoluteUri + @"\" + petId);
        }
        
        [When(@"user update pet Id in request json")]
        public string WhenUserUpdatePetIdInRequestJson()
        {
            Random random = new Random();
            int i,length=2;
            for(i=0;i<length;i++)
            {
                petId += random.Next(1, 9).ToString("0");
            }
            Hooks.stepNode.Info("Pet Store Id :: " + petId);
            CommonStepDefs.requestJson = CommonStepDefs.requestJson.Replace("\"@unique-id\"", petId);

            return CommonStepDefs.requestJson;
        }
        
        [When(@"user update existing pet Id in request json")]
        public void WhenUserUpdateExistingPetIdInRequestJson()
        {
            CommonStepDefs.requestJson = CommonStepDefs.requestJson.Replace("\"$id\"", petId);
        }
        
        [When(@"user update invalid pet Id in request json")]
        public string WhenUserUpdateInvalidPetIdInRequestJson()
        {
            CommonStepDefs.requestJson = CommonStepDefs.requestJson.Replace("\"@unique-id\"", "XXX");

            return CommonStepDefs.requestJson;
        }
        
        [Then(@"user verify pet name")]
        public void ThenUserVerifyPetName()
        {
            JObject obj = JObject.Parse(CommonStepDefs.requestJson);
            String expectedPetName = Convert.ToString(obj.SelectToken("$.name"));
            String responseBody = restApiHelper.GetResponseContent(CommonStepDefs.restResponse);

            JObject obj1 = JObject.Parse(responseBody);

            String actualPetName = Convert.ToString(obj1.SelectToken("$.name"));

            Hooks.stepNode.Info("Pet ID: " + petId);
            Hooks.stepNode.Info("Expected Pet Name: " + expectedPetName);
            Hooks.stepNode.Info("Actual Pet Name: " + actualPetName);

            Assert.AreEqual(actualPetName, expectedPetName, "Pet Name are NOT as expected.");
        }

        [Then(@"verify response message as ""(.*)""")]
        public void ThenVerifyResponseMessageAs(String rspMessage)
        {
            String actualMsg = restApiHelper.GetResponseContent(CommonStepDefs.restResponse);
            JObject o = JObject.Parse(actualMsg);
            actualMsg = (string)o.SelectToken("message");

            Hooks.stepNode.Info("Pet ID: " + petId);
            Hooks.stepNode.Info("Expected Error Message: " + rspMessage);
            Hooks.stepNode.Info("Actual Error Message: " + actualMsg);

            Assert.AreEqual(actualMsg, rspMessage, "Error message is not as expected.");
        }
    }
}
