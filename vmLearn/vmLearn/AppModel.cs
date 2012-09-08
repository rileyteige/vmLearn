using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vm;

namespace vmLearn
{
	class AppModel
	{
		public AppModel()
		{
			m_output = new OutputStream();
			m_virtualMachine = new VirtualMachine(Output);
		}

		public OutputStream Output
		{
			get { return m_output; }
		}

		public VirtualMachine VirtualMachine
		{
			get { return m_virtualMachine; }
		}

		VirtualMachine m_virtualMachine;

		OutputStream m_output;
	}
}
