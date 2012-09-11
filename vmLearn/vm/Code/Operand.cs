using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vm.Storage;

namespace vm.Code
{
	public class Operand
	{
		public Operand()
		{
		}

		public void setLiteralValue(string value)
		{
			Initialize();
			m_value = value;
			IsLiteral = true;
		}

		public void setRegisterValue(string name)
		{
			Initialize();
			m_value = name;
			IsRegister = true;
		}

		public void setAddressValue(string address)
		{
			Initialize();
			m_value = address;
			IsAddress = true;
		}

		public string Value
		{
			get { return m_value; }
		}

		public bool IsInitialized
		{
			get { return m_isInitialized; }
		}

		public bool IsLiteral
		{
			get { return m_isLiteral; }
			private set
			{
				bool val = value;
				m_isLiteral = val;
				if (val)
				{
					m_isAddress = false;
					m_isRegister = false;
				}
			}
		}

		public bool IsAddress
		{
			get { return m_isAddress; }
			private set
			{
				bool val = value;
				m_isAddress = val;
				if (val)
				{
					m_isLiteral = false;
					m_isRegister = false;
				}
			}
		}

		public bool IsRegister
		{
			get { return m_isRegister; }
			private set
			{
				bool val = value;
				m_isRegister = val;
				if (val)
				{
					m_isLiteral = false;
					m_isAddress = false;
				}
			}
		}

		private void Initialize()
		{
			if (!m_isInitialized)
				m_isInitialized = true;
		}

		bool m_isLiteral;
		bool m_isAddress;
		bool m_isRegister;

		bool m_isInitialized;

		string m_value;
	}
}
