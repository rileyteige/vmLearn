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
			ConnectIO();
			m_appModel.Initialize();
		}

		private void ConnectIO()
		{
			m_appModel.VirtualMachine.OutputChanged += new EventHandler(AppModel_OutputChanged);
		}

		private void AppModel_OutputChanged(object sender, EventArgs e)
		{
			VirtualMachine vm = sender as VirtualMachine;
			TextBox textBox = (TextBox)FindName("OutputTextBox");
			if (textBox != null)
				textBox.Text += vm.Output;
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
