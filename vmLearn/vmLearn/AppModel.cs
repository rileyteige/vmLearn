using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vm;
using vm.Storage;
using vmLearn.ViewModels;

namespace vmLearn
{
	class AppModel
	{
		public AppModel()
		{
			m_virtualMachine = new VirtualMachine();

			m_registers = new List<RegisterViewModel>();
			foreach (Register register in m_virtualMachine.Registers)
				m_registers.Add(new RegisterViewModel(register));
		}

		public void Initialize()
		{
			VirtualMachine.Initialize();
		}

		public VirtualMachine VirtualMachine
		{
			get { return m_virtualMachine; }
		}

		public List<RegisterViewModel> Registers
		{
			get { return m_registers; }
		}

		public void StartVirtualMachine()
		{
			m_virtualMachine.Start();
		}

		public void ResetVirtualMachine()
		{
			m_virtualMachine.Reset();
		}

		VirtualMachine m_virtualMachine;
		List<RegisterViewModel> m_registers;
	}
}
