using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class UnaryInstruction : Instruction
	{
		public UnaryInstruction(string name)
			: base(name)
		{
		}

		public override int ArgsExpected()
		{
			return 1;
		}
	}
}
