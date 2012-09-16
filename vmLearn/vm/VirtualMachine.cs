using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility;
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
			m_codeEnumerator = m_code.GetEnumerator();

			Write("\nVM Initialized.");
		}

		public bool NeedsReset
		{
			get { return m_needsReset; }
			private set { m_needsReset = value; }
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

		public event EventHandler CurrentInstructionChanged;
		public IInstruction CurrentInstruction
		{
			get
			{
				return m_currentInstruction;
			}
			set
			{
				m_currentInstruction = value;
				if (CurrentInstructionChanged != null)
					CurrentInstructionChanged(this, new EventArgs());
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

			if (leftStorage == null)
				throw new Exception(string.Format("Bad args, {0}", instruction.Name));

			string leftVal = GetStorageValue(leftStorage);
			string rightVal = rightStorage != null ? GetStorageValue(rightStorage) : right.Value;

			int leftInt = BinaryToIntegerUtility.LoadSignedInt(leftVal);
			int rightInt = BinaryToIntegerUtility.LoadSignedInt(rightVal);

			switch (name)
			{
				case "mov":
					{
						leftStorage.Data = GetDataForBitString(rightVal);
					} break;

				case "add":
					{
						int sum = leftInt + rightInt;
						leftStorage.Data = GetDataForBitString(BinaryUtility.ConvertIntToBinaryString(sum, vmConstants.BUS_WIDTH));
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

		public bool ExecuteNextInstruction()
		{
			bool needsBreak = false;
			if (needsBreak || m_needsReset)
				return false;

			if (m_codeEnumerator.Current != null)
				m_codeEnumerator.Current.IsCurrent = false;

			if (!m_codeEnumerator.MoveNext())
			{
				NeedsReset = true;
				return false;
			}
			else
			{
				CurrentInstruction = m_codeEnumerator.Current;
				m_codeEnumerator.Current.IsCurrent = true;
				ExecuteInstruction(m_codeEnumerator.Current);
				return true;
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
			if (m_codeEnumerator.Current != null)
				m_codeEnumerator.Current.IsCurrent = false;
			CurrentInstruction = null;
			m_codeEnumerator.Reset();
		}

		private bool m_needsReset;
		private IEnumerator<IInstruction> m_codeEnumerator;
		private IInstruction m_currentInstruction;
		private List<IInstruction> m_code;
		private DataMemory[] m_data;
		private Dictionary<string, Register> m_registers;

		private string m_output;
	}
}
