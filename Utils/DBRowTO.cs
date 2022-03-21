using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTICSharpAutoFramework.Utils
{
	public class DBRowTO
	{
		private String key;
		private String value;
		private String valueType;


		public String Key
		{
			get { return this.key; }
			set { this.key = value; }

		}

		public String Value
		{
			get { return this.value; }
			set { this.value = value; }

		}

		public String ValueType
		{
			get { return this.valueType; }
			set { this.valueType = value; }
		}

	}
}