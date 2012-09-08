using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vm
{
	public class OutputStream : MemoryStream
	{
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.Write(buffer, offset, count);
			if (OutputChanged != null)
				this.OutputChanged(this, new EventArgs());
		}

		public event EventHandler OutputChanged;
	}
}
