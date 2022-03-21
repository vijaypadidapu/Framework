using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTICSharpAutoFramework.Utils
{
	public class CalendarUtils
	{
		private static CalendarUtils objCalendarUtils;

		public static CalendarUtils GetCalendarUtilsObject()
		{

			if (objCalendarUtils == null)
			{
				objCalendarUtils = new CalendarUtils();
			}
			return objCalendarUtils;
		}

		public String GetTimeStamp(String dateFormat)
		{
			DateTime currentDate = DateTime.Now;
			return currentDate.ToString(dateFormat);
		}
	}
}