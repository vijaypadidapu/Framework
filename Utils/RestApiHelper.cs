using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LTICSharpAutoFramework.Utils
{
	public class RestApiHelper
	{
		public RestClient restClient;
		public RestRequest restRequest;
        public string base_Url_str;
        public string Url;

        public RestClient SetUrl(string base_Url, string resourceUrl)
		{
            if (!string.IsNullOrEmpty(base_Url))
                base_Url_str = base_Url;

            //var url = Path.Combine(base_Url_str, resourceUrl);
            Url = base_Url_str + resourceUrl;
            restClient = new RestClient(Url);
			return restClient;

        }
		public RestRequest CreatePostRequest(string jsonString)
		{
			restRequest = new RestRequest(Method.POST);
			restRequest.AddHeader("Accept", " application/json");
			restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
			return restRequest;
		}
		public RestRequest CreateGetRequest()
		{
			restRequest = new RestRequest(Method.GET);
			restRequest.AddHeader("Accept", " application/json");
			return restRequest;
		}

		public RestRequest CreatePutRequest(string jsonString)
		{
			restRequest = new RestRequest(Method.PUT);
			restRequest.AddHeader("Accept", " application/json");
			restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
			return restRequest;
		}

		public RestRequest CreateDeleteRequest()
		{
			restRequest = new RestRequest(Method.DELETE);
			restRequest.AddHeader("Accept", " application/json");
			return restRequest;
		}
		public IRestResponse GetResponse(RestRequest restRequest, RestClient restClient)
		{
			return restClient.Execute(restRequest);
		}

		public string GetResponseContent(IRestResponse response)
		{
			return response.Content;
		}
		public int GetResponseStatusCode(IRestResponse response)
		{
			return (int)response.StatusCode;
		}
		public bool IsRequestSuccessful(IRestResponse response)
		{
			return response.IsSuccessful;
		}

		public String ConvertJsonToString(string jsonFilePath)
		{
			StreamReader r = new StreamReader(jsonFilePath);
			return r.ReadToEnd();
		}



	}
}