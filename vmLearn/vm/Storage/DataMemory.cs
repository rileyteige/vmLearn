using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Storage
{
	public class DataMemory : StorageBase
	{
		public DataMemory(int address)
		{
			m_address = string.Format("{0:X2}", address);
		}

		public string Address
		{
			get { return m_address; }
		}

		public string Bits
		{
			get
			{
				string rtn = string.Empty;
				for (int idx = 0; idx < Data.Length; idx++)
					rtn += Data[idx] == 1 ? "1" : "0";
				return rtn;
			}
		}

		string m_address;
	}
}
