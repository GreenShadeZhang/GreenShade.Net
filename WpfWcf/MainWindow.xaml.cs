using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWcf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
      public static MainWindow  Instance=null;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            
        }
      public  SerialPort myPort = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
 
            string baseAddress = "http://" + Environment.MachineName + ":8000/Service";
            ServiceHost host = new ServiceHost(typeof(WpfWcf.Control), new Uri(baseAddress));
            host.AddServiceEndpoint(typeof(WpfWcf.IControl), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            host.Open();
            if (myPort == null)
            {
                myPort = new SerialPort();
                myPort.BaudRate = 9600;
                myPort.DataBits = 8;
                myPort.PortName = Com.Text;
                myPort.NewLine = "\r\n";
                myPort.Open();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(myPort!=null)
            {
                myPort.Close();
            }          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ArryPort = SerialPort.GetPortNames();
            Com.ItemsSource = ArryPort;
        }
    }
}
