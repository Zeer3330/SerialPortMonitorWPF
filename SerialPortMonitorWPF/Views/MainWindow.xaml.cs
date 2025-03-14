using SerialPortMonitorWPF.Models;
using SerialPortMonitorWPF.ViewModels;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace SerialPortMonitorWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            RefreshSerialPorts();
        }

        private void RefreshSerialPorts()
        {
            PortNameComboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                PortNameComboBox.Items.Add(port);
            }
            if (ports.Length > 0)
            {
                PortNameComboBox.SelectedIndex = 0;
            }
        }

        private void ApplyConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var config = new SerialPortConfig();

            if (PortNameComboBox.SelectedItem is string portName)
            {
                config.PortName = portName;
            }

            if (BaudRateComboBox.SelectedItem is ComboBoxItem baudRateItem)
            {
                if (int.TryParse(baudRateItem.Content.ToString(), out int baudRate))
                {
                    config.BaudRate = baudRate;
                }
            }

            if (DataBitsComboBox.SelectedItem is ComboBoxItem dataBitsItem)
            {
                if (int.TryParse(dataBitsItem.Content.ToString(), out int dataBits))
                {
                    config.DataBits = dataBits;
                }
            }

            if (StopBitsComboBox.SelectedItem is ComboBoxItem stopBitsItem)
            {
                if (int.TryParse(stopBitsItem.Content.ToString(), out int stopBitsValue))
                {
                    switch (stopBitsValue)
                    {
                        case 1:
                            config.StopBits = StopBits.One;
                            break;
                        case 2:
                            config.StopBits = StopBits.Two;
                            break;
                    }
                }
            }

            if (ParityComboBox.SelectedItem is ComboBoxItem parityItem)
            {
                switch (parityItem.Content.ToString())
                {
                    case "None":
                        config.Parity = Parity.None;
                        break;
                    case "Odd":
                        config.Parity = Parity.Odd;
                        break;
                    case "Even":
                        config.Parity = Parity.Even;
                        break;
                }
            }

            viewModel.ConfigureSerialPort(config);
        }

        private void SendDataButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var data = SendTextBox.Text;
            viewModel.SendData(data);
        }

        private void RefreshPortsButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshSerialPorts();
        }
    }
}