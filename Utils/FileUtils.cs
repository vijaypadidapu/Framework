using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Reflection;
using LTICSharpAutoFramework.Reports;
using LTICSharpAutoFramework;

namespace LTICSharpAutoFramework.Utils
{
	public class FileUtils
	{
		private static FileUtils objFileUtils = null;

		public static FileUtils GetFileUtilsObject()
		{
			if (objFileUtils == null)
			{
				objFileUtils = new FileUtils();
			}
			return objFileUtils;
		}

		public void DeleteAllFilesInFolder(String folderPath)
		{
			String status = "FAIL";
			try
			{
				DirectoryInfo di = new DirectoryInfo(folderPath);

				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}

				status = "PASS";
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Delete all Files");
				Report.PrintStatus(status);
			}
		}


		public void DeleteFile(String filePath)
		{
			String status = "FAIL";
			try
			{
				File.Delete(filePath);
				status = "PASS";
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			finally
			{
				Report.PrintOperation("Delete File");
				Report.PrintStatus(status);
			}

		}

		public String DownloadFolderFilePath()
		{
			String folderPath = FXLConfig.GetConfiguration().DownloadFolder;
			DirectoryInfo diretoryInfo = new DirectoryInfo(folderPath);

			if (!diretoryInfo.Exists)
			{
				diretoryInfo.Create();
			}

			return diretoryInfo.FullName;
		}
		public String DownloadFolderFilePath(String folderPath)
		{
			DirectoryInfo diretoryInfo = new DirectoryInfo(folderPath);


			if (!diretoryInfo.Exists)
			{
				diretoryInfo.Create();
			}

			return diretoryInfo.FullName;
		}

		public bool IsFileExists(String filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return fileInfo.Exists;
		}

		public float GetFileSize(String filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			return fileInfo.Length;
		}

		public static string ReadTextFromFile(string messageFileName)
		{
			string message = string.Empty;
			string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string messageFilePath = Path.GetFullPath(Path.Combine(assemblyPath, @"..\..\TestData\")) + messageFileName;

			StreamReader r = new StreamReader(messageFilePath);
			message = r.ReadToEnd();

			return message;
		}



	}
}