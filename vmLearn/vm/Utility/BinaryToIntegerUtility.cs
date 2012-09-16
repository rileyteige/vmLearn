using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace vm.Utility
{
	public static class BinaryToIntegerUtility
	{
		public static int LoadUnsignedInt(string bits)
		{
			return Convert.ToInt32(bits, 2);
		}

		public static int LoadSignedInt(string bits)
		{
			if (bits.Length != vmConstants.INSTRUCTION_OPERANDLENGTH)
				throw new ArgumentOutOfRangeException("bits");

			bool isNegative = bits[0] == '1';
			string value = bits.Substring(1, vmConstants.INSTRUCTION_OPERANDLENGTH - 1);
			if (isNegative)
				value = BinaryUtility.GetTwosComplement(value);

			int bitValue = LoadUnsignedInt(value);
			return isNegative ? -bitValue : bitValue;
		}
	}
}
