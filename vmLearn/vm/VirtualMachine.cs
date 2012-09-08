using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm
{
	public class VirtualMachine
	{
		public VirtualMachine(OutputStream output)
		{
			m_output = output;
		}

		public void Initialize()
		{
			Write("VM Initialized.");
		}

		public OutputStream Output
		{
			get { return m_output; }
		}

		private void Write(string msg)
		{
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			Output.Write(encoder.GetBytes(msg), 0, msg.Length);
		}

		private OutputStream m_output;
	}
}
