using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace vm
{
	internal static class vmConstants
	{
		public const int KB = 1024;
		public const int MEMORY_CAPACITY = 16 * KB;

		public const int BUS_WIDTH = 16;

		public const int INSTRUCTION_BITLENGTH = 44;
		public const int INSTRUCTION_OPCODELENGTH = 8;
		public const int INSTRUCTION_OPERANDLENGTH = 16;

		public static readonly ReadOnlyCollection<string> REGISTERNAMES = 
			new List<string> { "a", "b", "c", "d", "f", "s", "pc" }
			.AsReadOnly();
	}
}
