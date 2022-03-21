using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NUnit.Framework;

namespace LTICSharpAutoFramework.Utils
{

	public class ExcelUtils
	{
		private static XSSFWorkbook excelWBook;
		private static ISheet excelSheet;
		private static IRow excelRow;

		private static String filePath;

		public static void SetExcelFile(String excelFilePath, String sheetName)
		{
			try
			{
				FileInfo excelFile = new FileInfo(excelFilePath);

				excelWBook = new XSSFWorkbook(excelFile);
				excelSheet = excelWBook.GetSheet(sheetName);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public static XSSFWorkbook SetExcelFile(String excelFilePath)
		{
			try
			{
				FileInfo excelFile = new FileInfo(excelFilePath);
				excelWBook = new XSSFWorkbook(excelFile);
			}

			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			return excelWBook;
		}

		public static ISheet SetSheet(String sheet)
		{
			excelSheet = excelWBook.GetSheet(sheet);
			return excelSheet;
		}

		public static String GetCellData(int rowNum, int cellNum)
		{
			String cellData = "";
			if (excelSheet.GetRow(rowNum).GetCell(cellNum) != null)
			{
				cellData = excelSheet.GetRow(rowNum).GetCell(cellNum).StringCellValue;

			}
			return cellData;
		}


		public static List<String> GetCellData(String columnName)
		{
			bool flag = false;
			int columnIndex = 0;
			int cellCount = GetCellCount(0);
			int rowCount = GetRowCount();

			List<String> columnValue = new List<string>();

			try
			{
				for (int cIndex = 0; cIndex < cellCount; cIndex++)
				{
					if (GetCellData(0, cIndex).Equals(columnName))
					{
						columnIndex = cIndex;
						flag = true;
					}
				}
				if (flag == true)
				{
					for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
					{
						columnValue.Add(GetCellData(rowIndex, columnIndex));
					}

				}
				else
				{
					Assert.Fail("Column name is not available in excel sheet: " + columnName);

				}
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}
			return columnValue;
		}

		public static void SetCellData(int rowNum, int cellNum, String value)
		{
			/* LastRowNum returns Index */
			int rowCount = excelSheet.LastRowNum;

			/*
             * Checking if row exist or not. If Yes, existing row with assigned to excelRow
             * else new Row will be created
             */
			if (rowNum >= 0 && rowNum <= rowCount)
			{
				excelRow = excelSheet.GetRow(rowNum);
			}
			else
			{
				excelRow = excelSheet.CreateRow(rowNum);
			}

			/* LastCellNum returns count */
			int cellCount = excelRow.LastCellNum;
			/*
             * Checking if cell exist or not. If Yes, value will be set in existing cell
             * else a new cell is created and value is set in the same
             */
			if (cellNum >= 0 && cellNum < cellCount)
			{
				excelRow.GetCell(cellNum).SetCellValue(value);
			}
			else
			{
				excelRow.CreateCell(cellNum).SetCellValue(value);
			}

			FileInfo excelFile = new FileInfo(filePath);
			FileStream excelFileStream = File.OpenRead(filePath);
			excelWBook.Write(excelFileStream);
			excelFileStream.Flush();
			excelFileStream.Close();
		}

		public static int GetRowCount()
		{
			int count = 0;

			if (excelSheet.PhysicalNumberOfRows > 0)
			{
				/* getLastRowNum returns Index not count thus count++ */
				count = excelSheet.LastRowNum;
			}
			else
			{
				Assert.Fail("!!! No Row found !!!");
			}

			return ++count;
		}

		public static int GetCellCount(int row)
		{
			int count = 0;

			excelRow = excelSheet.GetRow(row);

			if (excelRow.PhysicalNumberOfCells > 0)
			{

				/* getLastCellNum returns count */
				count = excelRow.LastCellNum;

			}
			else
			{
				Assert.Fail("!!! No Cell found !!!");
			}
			return count;
		}

		public static int GetRowIndexByCellValue(int columnNum, String searchValue)
		{
			try
			{
				int rowCount = GetRowCount();

				for (int index = 0; index < rowCount; index++)
				{

					if (GetCellData(index, columnNum).Equals(searchValue))
					{

						return index;
					}
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.HandleException(e);
			}

			return -1;
		}

		public static void CloseWorkBook()
		{
			excelWBook.Close();
		}




	}
}