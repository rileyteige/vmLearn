using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class BinaryInstruction : Instruction
	{
		public BinaryInstruction(string name)
			: base(name)
		{
		}

		public override int ArgsExpected()
		{
			return 2;
		}
	}
}
