using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class UnaryInstruction : Instruction
	{
		public UnaryInstruction(string name, Operand operand)
			: base(name)
		{
			m_operand = operand;
		}

		public Operand Operand
		{
			get { return m_operand; }
		}

		public override int ArgsExpected()
		{
			return 1;
		}

		Operand m_operand;
	}
}
