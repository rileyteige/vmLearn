using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility;
using vm;

namespace vmLearn
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			m_appModel = new AppModel();
			this.DataContext = m_appModel;
			ConnectEvents();
			m_appModel.Initialize();
		}

		private void ConnectEvents()
		{
			m_appModel.VirtualMachine.OutputChanged += new EventHandler(AppModel_OutputChanged);
		}

		private void AppModel_OutputChanged(object sender, EventArgs e)
		{
			VirtualMachine vm = sender as VirtualMachine;
			TextBox textBox = (TextBox)FindName("IOTextBox");

			if (textBox != null)
			{
				textBox.AppendText(vm.Output);
				textBox.CaretIndex += 1;
			}
		}

		private void CodeMemoryListBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			if (CodeMemoryListBox.SelectedItem != null)
			{
				CodeMemoryListBox.ScrollIntoView(CodeMemoryListBox.SelectedItem);
			}
			else
			{
				CodeMemoryListBox.ScrollIntoView(CodeMemoryListBox.Items.GetItemAt(0));
			}
		}

		private void IOTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			Key key = e.Key;
			char input = KeyboardUtility.GetCharFromKey(key);
			if (input == ' ' && !(e.Key == Key.Space || e.SystemKey == Key.Space))
				return;

			m_appModel.VirtualMachine.ProcessInput(input);
		}

		private void IOTextBox_TextChanged(object sender, RoutedEventArgs e)
		{
			ScrollTextBoxToEnd();
		}

		private void ScrollTextBoxToEnd()
		{
			TextBox textBox = (TextBox)this.FindName("IOTextBox");
			if (textBox == null)
				return;

			textBox.Focus();
			textBox.SelectionStart = textBox.Text.Length;
			textBox.ScrollToEnd();
			textBox.InvalidateVisual();
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			if (m_appModel.ExecuteNextInstruction())
			{
				ListBox code = (ListBox)this.FindName("CodeMemoryListBox");
			}
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			m_appModel.ResetVirtualMachine();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Window_Close(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Window_Minimize(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Window_Maximize(object sender, RoutedEventArgs e)
		{
			if (this.WindowState == WindowState.Maximized)
				this.WindowState = WindowState.Normal;
			else
				this.WindowState = WindowState.Maximized;
		}

		AppModel m_appModel;
	}
}
