using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using vm.Storage;

namespace vmLearn.ViewModels
{
	public class RegisterViewModel : DependencyObject
	{
		public RegisterViewModel(Register register)
		{
			m_name = register.Name;
			register.DataChanged += new EventHandler(RegisterDataChanged);
		}

		private void RegisterDataChanged(object sender, EventArgs e)
		{
			Register register = sender as Register;
			string newBits = string.Empty;
			short[] data = register.Data;
			foreach (short digit in data)
				newBits += digit == 1 ? "1" : "0";
			Bits = newBits;
		}

		public string Name
		{
			get { return m_name; }
		}

		public static DependencyProperty BitsProperty = DependencyProperty.Register("Bits", typeof(string), typeof(RegisterViewModel), new PropertyMetadata(
			string.Empty));
		public string Bits
		{
			get { return (string) GetValue(BitsProperty); }
			set { SetValue(BitsProperty, value); }
		}

		private string m_name;
	}
}
