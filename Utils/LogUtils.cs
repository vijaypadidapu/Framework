using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

namespace LTICSharpAutoFramework.Utils
{
	public class LogUtils
	{
		private static ILog log;

		public static ILog GetLogger(Type type)
		{
			if (log != null)
				return log;
			else
			{
				log = log4net.LogManager.GetLogger(type);
				return log;
			}
		}
	}
}