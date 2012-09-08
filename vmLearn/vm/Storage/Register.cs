using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Storage
{
	public class Register
	{
		public Register(string name)
		{
			m_name = name;
			m_data = new short[vmConstants.BUS_WIDTH];
		}

		public string Name
		{
			get { return m_name; }
		}

		public short[] Data
		{
			get { return m_data;}
		}

		public string Bits
		{
			get
			{
				string rtn = string.Empty;
				for (int i = 0; i < m_data.Length; i++)
				{
					rtn += m_data[i] == 1 ? "1" : "0";
				}
				return rtn;
			}
		}

		short[] m_data;
		string m_name;
	}
}
