using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class Instruction : IInstruction
	{
		public Instruction(string name)
		{
			m_name = name;
		}

		public string Name
		{
			get { return m_name; }
		}

		public event EventHandler IsCurrentChanged;
		public bool IsCurrent
		{
			get
			{
				return m_isCurrent;
			}
			set
			{
				m_isCurrent = value;
				if (IsCurrentChanged != null)
					IsCurrentChanged(this, new EventArgs());
			}
		}

		public virtual int ArgsExpected()
		{
			return 0;
		}

		string m_name;
		bool m_isCurrent;
	}
}
