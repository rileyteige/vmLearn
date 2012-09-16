using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
	public static class BinaryUtility
	{
		public static string Increment(string bits)
		{
			string newBits = string.Empty;

			bool carried = false;

			foreach (char bit in bits.Reverse())
			{
				char nextBit;

				if (bit == '1')
				{
					nextBit = carried ? '1' : '0';
					carried = true;
				}
				else
				{
					nextBit = '1';
					carried = false;
				}

				newBits = nextBit + newBits;
				if (nextBit == '1' && !carried)
					break;
			}

			int idx = bits.Length - newBits.Length - 1;
			while (newBits.Length < bits.Length)
			{
				newBits = bits[idx] + newBits;
				idx--;
			}

			return newBits.Substring(newBits.Length - bits.Length, bits.Length);
		}

		public static string GetTwosComplement(string bits)
		{
			string flippedBits = string.Empty;
			foreach (char curr in bits)
				flippedBits += curr == '0' ? '1' : '0';

			return Increment(flippedBits);
		}

		public static string ConvertIntToBinaryString(int number, int buswidth)
		{
			bool twosComplement = number < 0;
			if (twosComplement)
				number *= -1;

			int currentNumber = number;
			int idx = 0;
			string bits = string.Empty;

			while (currentNumber != 0)
			{
				bits = (currentNumber % 2 == 0 ? "0" : "1") + bits;
				idx++;

				currentNumber /= 2;
			}

			if (idx < buswidth - 1)
			{
				for (int i = 0; i < buswidth - idx - 1; i++)
					bits = "0" + bits;
			}

			if (twosComplement)
				bits = GetTwosComplement(bits);

			bits = (twosComplement ? '1' : '0') + bits;

			return bits.Substring(bits.Length - buswidth, buswidth);
		}
	}
}
