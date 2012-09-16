using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm.Code
{
	public interface IInstruction
	{
		string Name { get; }
		int ArgsExpected();

		bool IsCurrent { get; set; }
		event EventHandler IsCurrentChanged;
	}
}
