using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm
{
	public class VirtualMachine
	{
		public VirtualMachine()
		{
		}

		public void Initialize()
		{
			Write("VM Initialized.");
		}

		public event EventHandler OutputChanged;
		public string Output
		{
			get
			{
				return m_output;
			}
			set
			{
				m_output = value;
				if (OutputChanged != null)
					OutputChanged(this, new EventArgs());
			}
		}

		private void Write(string msg)
		{
			Output = msg;
		}

		public void ProcessInput(char input)
		{
			Write(input + "");
		}

		private string m_output;
	}
}
