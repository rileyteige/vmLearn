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
			m_virtualMachine = new VirtualMachine();
		}

		public void Initialize()
		{
			VirtualMachine.Initialize();
		}

		public VirtualMachine VirtualMachine
		{
			get { return m_virtualMachine; }
		}

		VirtualMachine m_virtualMachine;
	}
}
