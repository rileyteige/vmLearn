using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using vm.Code;

namespace vm.Utility
{
	public static class BinaryToInstructionUtility
	{
		private static Dictionary<string, string> InstructionKeys = new Dictionary<string, string>
		{
			{ "00000000", "mov" },
			{ "00000001", "push" },
			{ "00000010", "pop" },
			{ "00000011", "add" }
		};

		private static List<string> Instructions = new List<string>
		{
			"pop"
		};

		private static List<string> UnaryInstructions = new List<string>
		{
			"push"
		};

		private static List<string> BinaryInstructions = new List<string>
		{
			"mov",
			"add"
		};

		private static Operand CreateOperand(string memoryFlag, string registerFlag, string bits)
		{
			if (bits.Length != vmConstants.INSTRUCTION_OPERANDLENGTH)
				throw new ArgumentException("bits");

			Operand operand = new Operand();

			bool memory = memoryFlag == "1";
			bool register = registerFlag == "1";

			if (memory)
			{
				operand.setAddressValue(string.Format("{0}", Convert.ToInt32(bits.Substring(bits.Length - vmConstants.BUS_WIDTH, vmConstants.BUS_WIDTH), 2)));
			}
			else if (register)
			{
				string registerName = BinaryToRegisterNameUtility.Convert(bits);
				if (registerName != null)
					operand.setRegisterValue(registerName);
			}
			else
			{
				operand.setLiteralValue(bits);
			}

			return operand;
		}

		public static IInstruction ConvertFromBinary(string bits)
		{
			int idx = 0;
			int len = bits.Length;
			string instructionBits = bits.Substring(idx, vmConstants.INSTRUCTION_OPCODELENGTH);
			idx += vmConstants.INSTRUCTION_OPCODELENGTH;

			if (!InstructionKeys.ContainsKey(instructionBits))
				return null;

			string instructionName = InstructionKeys[instructionBits];

			if (Instructions.Exists(x => x == instructionName))
				return new Instruction(instructionName);

			string leftOperandMemoryFlag = bits.Substring(idx, 1);
			idx += 1;

			string leftOperandRegisterFlag = bits.Substring(idx, 1);
			idx += 1;

			string leftOperandValue = bits.Substring(idx, vmConstants.INSTRUCTION_OPERANDLENGTH);
			idx += vmConstants.INSTRUCTION_OPERANDLENGTH;

			Operand leftOperand = CreateOperand(leftOperandMemoryFlag, leftOperandRegisterFlag, leftOperandValue);

			if (UnaryInstructions.Exists(x => x == instructionName))
				return new UnaryInstruction(instructionName, leftOperand);

			string rightOperandMemoryFlag = bits.Substring(idx, 1);
			idx += 1;

			string rightOperandRegisterFlag = bits.Substring(idx, 1);
			idx += 1;

			string rightOperandValue = bits.Substring(idx, vmConstants.INSTRUCTION_OPERANDLENGTH);
			idx += vmConstants.INSTRUCTION_OPERANDLENGTH;

			Operand rightOperand = CreateOperand(rightOperandMemoryFlag, rightOperandRegisterFlag, rightOperandValue);

			if (BinaryInstructions.Exists(x => x == instructionName))
				return new BinaryInstruction(instructionName, leftOperand, rightOperand);

			return null;
		}
	}
}
