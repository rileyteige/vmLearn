using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Storage
{
	public class StorageBase
	{
		public StorageBase()
		{
			m_data = new short[vmConstants.BUS_WIDTH];
		}

		public event EventHandler DataChanged;
		public short[] Data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
				if (DataChanged != null)
					DataChanged(this, new EventArgs());
			}
		}

		private short[] m_data;
	}
}
