using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Utility
{
	public static class BinaryToRegisterNameUtility
	{
		private const int RegisterBitLength = 3;
		private static Dictionary<string, string> RegisterKeys = new Dictionary<string, string>
		{
			{ "000", "a" },
			{ "001", "b" },
			{ "010", "c" },
			{ "011", "d" },
			{ "100", "f" },
			{ "101", "s" },
			{ "110", "pc" }
		};

		public static string Convert(string bits)
		{
			if (bits.Length < 3)
				return null;

			string truncatedBits = bits.Length > 3 ? bits.Substring(bits.Length - RegisterBitLength, RegisterBitLength) : bits;

			if (!RegisterKeys.ContainsKey(truncatedBits))
				return null;

			return RegisterKeys[truncatedBits];
		}
	}
}
