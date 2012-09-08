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
			InitializeVM();
		}

		private void ConnectIO()
		{
			m_appModel.Output.OutputChanged += new EventHandler(AppModel_OutputChanged);
		}

		private void InitializeVM()
		{
			m_appModel.VirtualMachine.Initialize();
		}

		private void AppModel_OutputChanged(object sender, EventArgs e)
		{
			using (Stream stream = sender as Stream)
			using (StreamReader reader = new StreamReader(stream))
			{
				stream.Position = 0;
				string newOutput = reader.ReadToEnd();
				TextBox textBox = (TextBox)FindName("OutputTextBox");
				if (textBox != null)
					textBox.Text += newOutput;
			}
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
