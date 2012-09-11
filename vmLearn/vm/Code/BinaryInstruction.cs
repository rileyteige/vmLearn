using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public class BinaryInstruction : Instruction
	{
		public BinaryInstruction(string name, Operand leftOperand, Operand rightOperand)
			: base(name)
		{
			m_leftOperand = leftOperand;
			m_rightOperand = rightOperand;
		}

		public Operand LeftOperand
		{
			get { return m_leftOperand; }
		}

		public Operand RightOperand
		{
			get { return m_rightOperand; }
		}
		
		public override int ArgsExpected()
		{
			return 2;
		}

		Operand m_leftOperand;
		Operand m_rightOperand;
	}
}
