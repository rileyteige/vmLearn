using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Storage
{
	public class Register : StorageBase
	{
		public Register(string name)
			: base()
		{
			m_name = name;
		}

		public string Name
		{
			get { return m_name; }
		}

		string m_name;
	}
}
