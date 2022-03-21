using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RestSharp;
using System;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using log4net;

namespace LTICSharpAutoFramework.Utils
{
	public class RestApiHelperMethods
	{
		static ILog log = LogUtils.GetLogger(typeof(RestApiHelperMethods));

		public static String ConvertJsonToString(String jsonFilePath)
		{
			StreamReader r = new StreamReader(jsonFilePath);
			return r.ReadToEnd();
		}

		public static String PutExcelRowDataToJSONReturnsString(String jsonFilePath, String excelFilePath, String excelSheet, int excelRow)
		{
			return CreateJsonReturnsString(jsonFilePath, CreateMapFromExcelHeaders(excelFilePath, excelSheet, excelRow));

		}

		public static String CreateJsonReturnsString(String jsonFilePath, Dictionary<string, string> map)
		{
			String strJson = ConvertJsonToString(jsonFilePath);

			foreach (string key in map.Keys)
			{
				if (key.Contains("$int_"))
				{
					strJson = strJson.Replace("\"" + key + "\"", map[key]);
				}
				else
				{
					strJson = strJson.Replace(key, map[key]);
				}
			}

			strJson = strJson.Replace("\"null\"", "null");

			strJson = strJson.Replace("\"true\"", "true");

			strJson = strJson.Replace("\"false\"", "false");

			return strJson;
		}

		public static Dictionary<string, string> CreateMapFromExcelHeaders(String excelFilePath, String sheet, int valueRowIndex)
		{
			Dictionary<String, String> map = null;

			try
			{
				ExcelUtils.SetExcelFile(excelFilePath, sheet);
				int rowCount = ExcelUtils.GetCellCount(0);
				map = new Dictionary<String, String>();

				for (int index = 0; index < rowCount; index++)
				{
					String header = ExcelUtils.GetCellData(0, index);

					if (header.Length > 0)
					{
						header = "$" + header.Trim();
						String value = ExcelUtils.GetCellData(valueRowIndex, index);
						map.Add(header, value);
					}
				}

				ExcelUtils.CloseWorkBook();
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			return map;
		}
		public static void JsonFieldVerification(IRestResponse response, String excelFilePath, String sheetName, int rowIndex,
		int startCellIndex, String jsonKeyColumnName)
		{
			int keyListLength = -1;

			Dictionary<List<String>, List<String>> jsonKeyValueMap = RetriveJsonKeysAndExpectedValue(excelFilePath, sheetName, rowIndex, startCellIndex, jsonKeyColumnName);

			List<String> listKey = new List<String>();
			List<String> listExpectedValue = new List<String>();

			foreach (KeyValuePair<List<String>, List<String>> entrySet in jsonKeyValueMap)
			{
				listKey = entrySet.Key;
				listExpectedValue = entrySet.Value;
			}

			keyListLength = listKey.Count;

			for (int i = 0; i < keyListLength; i++)
			{
				if (listKey[i].Length > 0)
				{
					JObject jsonObj = JObject.Parse(response.Content);
					string listKeyToken = (string)jsonObj.SelectToken(listKey[i]);

					VerifyEquals(listExpectedValue[i], listKeyToken);
				}
			}
		}

		public static Dictionary<List<String>, List<String>> RetriveJsonKeysAndExpectedValue(String excelFilePath, String sheetName, int rowIndex,
		int startCellIndex, String jsonKeyColumnName)
		{
			List<String> listKey = new List<String>();
			List<String> listExpectedValue = new List<String>();

			Dictionary<List<String>, List<String>> jsonKeyValueMap = new Dictionary<List<String>, List<String>>();

			try
			{
				ExcelUtils.SetExcelFile(excelFilePath, "JsonKeys");
				listKey = ExcelUtils.GetCellData(jsonKeyColumnName);
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				try
				{
					ExcelUtils.CloseWorkBook();
				}
				catch (IOException e)
				{
					ExceptionHandler.HandleException(e);

				}
			}
			try
			{
				ExcelUtils.SetExcelFile(excelFilePath, sheetName);
				int cellCount = ExcelUtils.GetCellCount(rowIndex);

				for (int i = startCellIndex; i < cellCount; i++)
				{
					listExpectedValue.Add(ExcelUtils.GetCellData(rowIndex, i));
				}

			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			finally
			{
				try
				{
					ExcelUtils.CloseWorkBook();
				}
				catch (IOException e)
				{
					ExceptionHandler.HandleException(e);

				}
			}

			jsonKeyValueMap.Add(listKey, listExpectedValue);
			return jsonKeyValueMap;


		}

		public static void VerifyEquals(Object expectedValue, Object actualValue)
		{
			log.Debug("Expected Value: " + expectedValue + " and Actual Value:" + actualValue);
			Assert.AreEqual(expectedValue, actualValue, " Value mismatch. Expected Value: " + expectedValue + " and Actual Value:" + actualValue);

		}






	}
}