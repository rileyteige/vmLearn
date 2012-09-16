using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using vm;
using vm.Code;
using vm.Storage;
using vmLearn.ViewModels;

namespace vmLearn
{
	class AppModel : DependencyObject
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
			VirtualMachine.CurrentInstructionChanged += new EventHandler(VirtualMachine_CurrentInstructionChanged);

			m_instructions = new List<InstructionViewModel>();
			foreach (IInstruction instruction in m_virtualMachine.Code)
				m_instructions.Add(new InstructionViewModel(instruction));
		}

		public VirtualMachine VirtualMachine
		{
			get { return m_virtualMachine; }
		}

		public List<RegisterViewModel> Registers
		{
			get { return m_registers; }
		}

		private void VirtualMachine_CurrentInstructionChanged(object sender, EventArgs e)
		{
			VirtualMachine vm = sender as VirtualMachine;
			CurrentInstruction = m_instructions.Where(x => x.Instruction == vm.CurrentInstruction).FirstOrDefault();
		}

		public static DependencyProperty CurrentInstructionProperty = DependencyProperty.Register("CurrentInstruction", typeof(InstructionViewModel), typeof(AppModel), new PropertyMetadata(null));
		public InstructionViewModel CurrentInstruction
		{
			get
			{
				return (InstructionViewModel) GetValue(CurrentInstructionProperty);
			}
			set
			{
				SetValue(CurrentInstructionProperty, value);
			}
		}

		public List<InstructionViewModel> Instructions
		{
			get { return m_instructions; }
		}

		public bool ExecuteNextInstruction()
		{
			return m_virtualMachine.ExecuteNextInstruction();
		}

		public void ResetVirtualMachine()
		{
			m_virtualMachine.Reset();
		}

		VirtualMachine m_virtualMachine;
		List<RegisterViewModel> m_registers;
		InstructionViewModel m_currentInstruction;
		List<InstructionViewModel> m_instructions;
	}
}
