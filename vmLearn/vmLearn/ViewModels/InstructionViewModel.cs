using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using vm.Code;

namespace vmLearn.ViewModels
{
	public class InstructionViewModel : DependencyObject
	{
		public InstructionViewModel(IInstruction instruction)
		{
			m_instruction = instruction;
			instruction.IsCurrentChanged += new EventHandler(IsCurrentChanged);
			m_name = instruction.Name;

			UnaryInstruction unary = instruction as UnaryInstruction;
			BinaryInstruction binary = instruction as BinaryInstruction;

			if (binary != null)
			{
				m_leftOperand = binary.LeftOperand;
				m_rightOperand = binary.RightOperand;
			}
			else if (unary != null)
			{
				m_leftOperand = unary.Operand;
			}

		}

		public string Name
		{
			get { return m_name; }
		}

		public Operand LeftOperand
		{
			get { return m_leftOperand; }
		}

		public Operand RightOperand
		{
			get { return m_rightOperand; }
		}

		public IInstruction Instruction
		{
			get { return m_instruction; }
		}

		private void IsCurrentChanged(object sender, EventArgs e)
		{
			IInstruction instruction = sender as IInstruction;
			IsCurrent = instruction.IsCurrent;
		}

		public static DependencyProperty IsCurrentProperty = DependencyProperty.Register("IsCurrent", typeof(bool), typeof(InstructionViewModel), new PropertyMetadata(false));
		public bool IsCurrent
		{
			get { return (bool) GetValue(IsCurrentProperty); }
			set { SetValue(IsCurrentProperty, value); }
		}

		string m_name;
		Operand m_leftOperand;
		Operand m_rightOperand;
		IInstruction m_instruction;
	}
}
