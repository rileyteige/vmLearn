using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Storage
{
	public class DataMemory
	{
		public DataMemory(int address)
		{
			m_address = address;
			m_data = new short[vmConstants.BUS_WIDTH];
		}

		public int Address
		{
			get { return m_address; }
		}

		public short[] Data
		{
			get { return m_data; }
		}

		public string Bits
		{
			get
			{
				string rtn = string.Empty;
				for (int idx = 0; idx < m_data.Length; idx++)
					rtn += m_data[idx] == 1 ? "1" : "0";
				return rtn;
			}
		}

		short[] m_data;
		int m_address;
	}
}
