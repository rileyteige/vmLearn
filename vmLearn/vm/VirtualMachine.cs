using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using vm.Code;
using vm.Storage;
using vm.Utility;

namespace vm
{
	public class VirtualMachine
	{
		public VirtualMachine()
		{
			CreateRegisters();
			ConstructData();
		}

		public void Initialize()
		{
			Write("VM Starting...");
			Write(string.Format("\nAvailable memory: {0} {1}-bit words", vmConstants.MEMORY_CAPACITY, vmConstants.BUS_WIDTH));

			InitializeRegisterData();

			LoadCode("../../testCodeFile.txt");

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

		private void InitializeRegisterData()
		{
			foreach (Register register in Registers)
				register.Data = new short[vmConstants.BUS_WIDTH];
		}

		private void CreateRegisters()
		{
			m_registers = new Dictionary<string, Register>();
			foreach (string name in vmConstants.REGISTERNAMES)
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

		private void LoadCode(string fileName)
		{
			if (m_code == null)
				m_code = new List<IInstruction>();

			if (!File.Exists(fileName))
			{
				Write(string.Format("\nI love Angie"));
				return;
			}

			m_code.Clear();

			using (FileStream fStream = new FileStream(fileName, FileMode.Open))
			using (StreamReader reader = new StreamReader(fStream))
			{
				string instruction = reader.ReadLine();
				while (instruction != null)
				{
					m_code.Add(BinaryToInstructionUtility.ConvertFromBinary(instruction));
					instruction = reader.ReadLine();
				}
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

		public Register GetRegister(string name)
		{
			if (!m_registers.ContainsKey(name))
				return null;

			return m_registers[name];
		}

		private StorageBase GetStorageForOperand(Operand operand)
		{
			if (!operand.IsInitialized)
				return null;

			StorageBase storage = null;
			if (operand.IsAddress)
				storage = Data[Convert.ToInt32(operand.Value, 6)];
			else if (operand.IsRegister)
				storage = m_registers[operand.Value];

			return storage;
		}

		private string GetStorageValue(StorageBase storage)
		{
			if (storage == null)
				return null;

			string value = string.Empty;
			foreach (short digit in storage.Data)
			{
				if (digit != 1 && digit != 0)
					throw new Exception("Bad binary digit.");
				value += digit == 1 ? "1" : "0";
			}
			return value;
		}

		private short[] GetDataForBitString(string bits)
		{
			if (bits.Length != vmConstants.BUS_WIDTH)
				throw new Exception("bad bits length");

			short[] data = new short[vmConstants.BUS_WIDTH];
			int idx = 0;

			foreach (char currentChar in bits.ToCharArray())
			{
				data[idx] = (short) (currentChar == '1' ? 1 : 0);
				idx++;
			}

			return data;
		}

		private void ExecuteBinaryInstruction(BinaryInstruction instruction)
		{
			string name = instruction.Name;
			Operand left = instruction.LeftOperand;
			Operand right = instruction.RightOperand;

			StorageBase leftStorage = GetStorageForOperand(left);
			StorageBase rightStorage = GetStorageForOperand(right);

			string rightval = rightStorage != null ? GetStorageValue(rightStorage) : right.Value;

			switch (name)
			{
				case "mov":
					{
						if (leftStorage == null)
							throw new Exception("Bad args, mov");
						leftStorage.Data = GetDataForBitString(rightval);
					} break;
			}
		}

		private void ExecuteInstruction(IInstruction instruction)
		{
			string name = instruction.Name;
			Instruction regular = instruction as Instruction;
			UnaryInstruction unary = instruction as UnaryInstruction;
			BinaryInstruction binary = instruction as BinaryInstruction;

			if (binary != null)
			{
				ExecuteBinaryInstruction(binary);
			}
			else if (unary != null)
			{
			}
			else if (regular != null)
			{
			}
			else
			{
				throw new Exception("Bad instruction.");
			}
		}

		public void Start()
		{
			Write("\nProgram Starting...\n");
			IEnumerator<IInstruction> enumerator = m_code.GetEnumerator();
			enumerator.Reset();
			bool needsBreak = false;
			while (!needsBreak && enumerator.MoveNext())
			{
				Write(string.Format("\nExecuting {0}...", enumerator.Current.Name));
				ExecuteInstruction(enumerator.Current);
			}
			Write("\n\nProgram terminated.");
		}

		public void Reset()
		{
			InitializeRegisterData();
		}

		private List<IInstruction> m_code;
		private DataMemory[] m_data;
		private Dictionary<string, Register> m_registers;

		private string m_output;
	}
}
