using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class Instruction
	{
		public Instruction(string name)
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
