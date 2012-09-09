using System;
using System.Collections.Generic;
using System.Linq;
using vm.Code;
using vm.Storage;

namespace vm
{
	public class VirtualMachine
	{
		public VirtualMachine()
		{
			m_code = new List<IInstruction> { new Instruction("push"), new Instruction("pop"), new UnaryInstruction("call"), new BinaryInstruction("add") };
			CreateRegisters();
			ConstructData();
		}

		public void Initialize()
		{
			Write("VM Starting...");
			Write(string.Format("\nAvailable memory: {0} {1}-bit words", vmConstants.MEMORY_CAPACITY, vmConstants.BUS_WIDTH));
			Write("\nVM Initialized.");
		}

		public List<Register> Registers
		{
			get
			{
				return m_registers
					.Values
					.ToList();
			}
		}

		public List<IInstruction> Code
		{
			get { return m_code; }
		}

		public DataMemory[] Data
		{
			get { return m_data; }
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

		private void CreateRegisters()
		{
			m_registers = new Dictionary<string, Register>();
			List<string> registerNames = new List<string> { "a", "b", "c", "d", "f", "s", "pc" };
			foreach (string name in registerNames)
			{
				m_registers.Add(name, new Register(name));
			}
		}

		private void ConstructData()
		{
			m_data = new DataMemory[vmConstants.MEMORY_CAPACITY];
			for (int idx = 0; idx < m_data.Length; idx++)
			{
				m_data[idx] = new DataMemory(idx);
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

		private List<IInstruction> m_code;
		private DataMemory[] m_data;
		private Dictionary<string, Register> m_registers;

		private string m_output;
	}
}
