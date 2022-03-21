using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using log4net;
using LTICSharpAutoFramework;

namespace LTICSharpAutoFramework.Utils
{
	public class DBUtils
	{
		static ILog log = LogUtils.GetLogger(typeof(DBUtils));
		private static MySqlConnection connection;
		private static string connectionString;
		private static SqlConnection sqlConnection;
		private static string sqlConnectionString;
		public static List<DBRowTO> eleData;
		public static List<DBRowTO> testData;

		public static void Initialize()
		{
			try
			{
				connectionString = "SERVER=" + FXLConfig.GetConfiguration().DbUrl + ";" + "DATABASE=" +
				FXLConfig.GetConfiguration().DbName + ";" + "UID=" + FXLConfig.GetConfiguration().DbUsername + ";" + "PASSWORD=" + FXLConfig.GetConfiguration().DbPassword + ";";
				connection = new MySqlConnection(connectionString);

			}
			catch (Exception e)
			{
				log.Debug(e.StackTrace);
			}
		}




		public static void InitializeSql()
		{
			sqlConnectionString = string.Format("Server = {0}; Database = {1}; User Id ={2}; Password = {3};",
			FXLConfig.GetConfiguration().SQLDbServer,
			FXLConfig.GetConfiguration().SQLDbName,
			FXLConfig.GetConfiguration().SQLDbUserName,
			FXLConfig.GetConfiguration().SQLDbPassword);

			sqlConnection = new SqlConnection(sqlConnectionString);

		}

		//open connection to database

		public static bool OpenConnection()
		{
			try
			{
				Initialize();
				//connection.Open();
				return true;
			}

			catch (MySqlException ex)
			{
				//When handling errors, you can your application's response based
				//on the error number.
				//The two most common error numbers when connecting are as follows:
				//0:Cannot connect to server.
				//1045: Invalid user name and/or password.

				switch (ex.Number)
				{
					case 0:
						log.Debug("Cannot connect to server. Contact adminstrator");
						break;

					case 1045:
						log.Debug("Invalid username/password, please try again");
						break;


				}

				return false;

			}
		}

		public static bool OpenSqlConnection()
		{
			try
			{
				InitializeSql();
				sqlConnection.Open();
				return true;
			}
			catch (SqlException ex)
			{
				log.Debug(ex.Message);
				return false;
			}
		}

		//Close connection


		public static bool CloseConnection()
		{
			try
			{
				connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				log.Debug(ex.Message);
				return false;
			}
		}


		public static bool CloseSqlConnection()
		{
			try
			{
				sqlConnection.Close();
				return true;
			}

			catch (SqlException ex)
			{
				log.Debug(ex.Message);
				return false;
			}
		}
		public static void Insert(String query)
		{
			//open connection

			if (OpenConnection() == true)
			{
				try
				{
					//create command and assign the query and connection from the constructor
					MySqlCommand cmd = new MySqlCommand(query, connection);

					//Execute command
					cmd.ExecuteNonQuery();
				}

				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}


		}

		//update statement

		public static void Update(String query)
		{
			//Open connection
			if (OpenConnection() == true)
			{
				try
				{
					//create mysql command
					MySqlCommand cmd = new MySqlCommand();
					//Assign the query using CommandText
					cmd.CommandText = query;
					//Assign the connection using Connection
					cmd.Connection = connection;


					//Execute query
					cmd.ExecuteNonQuery();
				}

				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
		}


		//Delete statement
		public static void Delete(String query)
		{
			if (OpenConnection() == true)
			{
				try
				{
					MySqlCommand cmd = new MySqlCommand(query, connection);
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
		}

		//Count statement
		public int Count(String query)
		{
			int Count = -1;

			try
			{
				//Open Connection
				if (OpenConnection() == true)
				{
					//Create Mysql Command
					MySqlCommand cmd = new MySqlCommand(query, connection);

					//ExecuteScalar will return one value
					Count = int.Parse(cmd.ExecuteScalar() + "");
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			return Count;
		}

		//Select statement

		public static MySqlDataReader Select(String query)
		{
			MySqlDataReader dataReader = null;
			//Open connection
			if (OpenConnection() == true)
			{
				try
				{
					//Create Command
					MySqlCommand cmd = new MySqlCommand(query, connection);
					//Create a data reader and Execute the command
					dataReader = cmd.ExecuteReader();
				}

				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}

				
			}
			return dataReader;
		}


		public static SqlDataReader SelectSql(String query)
		{
			SqlDataReader dataReader = null;
			//Open connection
			if (OpenConnection() == true)
			{
				try
				{
					//Create Command
					SqlCommand cmd = new SqlCommand(query, sqlConnection);
					//Create a data reader and Execute the command
					dataReader = cmd.ExecuteReader();
				}
				catch (SqlException ex)
				{
					log.Debug(ex.Message);
				}
			}
			return dataReader;
		}
		public static List<DBRowTO> GetEleObjData(String application)
		{
			MySqlDataReader dataReader = null;
			eleData = new List<DBRowTO>();

			String query = "SELECT ELEMENTS.Element_Key, ELEMENTS.Element_Value, VALUE_TYPE.Value_Type as Value_Type"
			+ "FROM ELEMENTS " + "INNER JOIN VALUE_TYPE" + "ON ELEMENTS.Value_Type_ID=VALUE_TYPE.Value_Type_ID"
			+ "WHERE ELEMENTS.Page_ID IN" + "(SELECT Page_ID FROM PAGES WHERE PAGES.App_ID IN"
			+ "(SELECT App_ID FROM APPLICATIONS WHERE Application = '" + application + "'))";


			OpenConnection();

			try
			{
				dataReader = Select(query);

				while (dataReader.Read())
				{
					DBRowTO temp = new DBRowTO();
					temp.Key = dataReader["Element_Key"].ToString();
					temp.Value = dataReader["Element_Value"].ToString();
					temp.ValueType = dataReader["Value_Type"].ToString();
					eleData.Add(temp);

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
					dataReader.Close();
					CloseConnection();
				}
				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
			return eleData;
		}
		public static List<DBRowTO> GetElementPageWise(String pageName)
		{
			String finalSQLArg = "";

			String[] pageNameArr = pageName.Split(',');

			if (pageNameArr.Length > 1)
			{
				foreach (String temp in pageNameArr)
				{
					finalSQLArg = finalSQLArg + "'" + temp + "'" + ",";

				}
				finalSQLArg = finalSQLArg.Substring(0, finalSQLArg.Length - 1);
			}
			else
			{
				finalSQLArg = "'" + pageName + "'";
			}

			String query = "Select ELEMENTS.Element_Key,ELEMENTS.Element_Value,VALUE_TYPE.Value_Type from ELEMENTS,VALUE_TYPE Where ELEMENTS.Value_Type_ID=VALUE_TYPE.Value_Type_ID && ELEMENTS.Page_ID IN (" + finalSQLArg + ");";

			MySqlDataReader dataReader = null;
			eleData = new List<DBRowTO>();

			OpenConnection();
			try
			{
				dataReader = Select(query);
				while (dataReader.Read())
				{
					DBRowTO temp = new DBRowTO();
					temp.Key = dataReader["Element_Key"].ToString();
					temp.Value = dataReader["Element_Value"].ToString();
					temp.ValueType = dataReader["Value_Type"].ToString();
					eleData.Add(temp);
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
					dataReader.Close();
					CloseConnection();
				}
				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
			return eleData;

		}

		public static List<DBRowTO> GetData(String featureIDs)
		{
			MySqlDataReader dataReader = null;
			testData = new List<DBRowTO>();

			String query = "SELECT TESTDATA.Testdata_Key,TESTDATA.UBS_QA_Testdata_Value FROM TESTDATA WHERE TESTDATA.Feature_ID in (" + featureIDs + ")";

			OpenConnection();
			dataReader = Select(query);
			try
			{
				dataReader = Select(query);


				while (dataReader.Read())
				{
					DBRowTO temp = new DBRowTO();
					temp.Key = dataReader["Testdata_Key"].ToString();
					temp.Value = dataReader["UBS_QA_Testdata_Value"].ToString();

					testData.Add(temp);
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
					dataReader.Close();
					CloseConnection();
				}
				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
			return testData;
		}

		public static List<DBRowTO> GetAPIData(String tableName, String featureName)
		{
			MySqlDataReader dataReader = null;
			testData = new List<DBRowTO>();

			String query = "SELECT " + tableName + ".Data_Key," + tableName
			+ ".Data_Value, ValueType.ValueType as value_type FROM " + tableName + " INNER JOIN ValueType ON"
			+ tableName + ".Data_ValueType=ValueType.id WHERE FeatureName = " + featureName + "'";

			OpenConnection();
			try
			{
				dataReader = Select(query);


				while (dataReader.Read())
				{
					DBRowTO temp = new DBRowTO();
					temp.Key = dataReader["Data_Key"].ToString();
					temp.Value = dataReader["Data_Value"].ToString();
					temp.ValueType = dataReader["VALUE_TYPE"].ToString();

					testData.Add(temp);
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
					dataReader.Close();
					CloseConnection();
				}
				catch (Exception e)
				{
					ExceptionHandler.HandleException(e);
				}
			}
			return testData;

		}











	}
}