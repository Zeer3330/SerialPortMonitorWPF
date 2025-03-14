using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MySqlConnector;
using SerialPortMonitorWPF.Models;
using SerialPortMonitorWPF.Views;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace SerialPortMonitorWPF.ViewModels
{
    public class MainWindowViewModel
    {
        private SerialPort serialPort;
        public IEnumerable<ISeries> Series { get; set; }
        public List<Axis> XAxis { get; set; } = new List<Axis>();
        public List<Axis> YAxis { get; set; } = new List<Axis>();
        public ObservableCollection<DateTimePoint> dataPoints { get; set; }
        private Timer timer;
        private readonly string connectionString = "Server=localhost;Database=MySQL80;Uid=root;Pwd=zjq20031008;";

        public MainWindowViewModel()
        {
            dataPoints = new ObservableCollection<DateTimePoint>();

            // 创建数据系列
            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Values = dataPoints,
                    Name = "串口数据",
                    Stroke = new SolidColorPaint(SKColors.Blue, 3),
                    Fill = null,
                    GeometryStroke = null,
                    GeometrySize = 0
                }
            };

            // 配置X轴
            XAxis.Add(new Axis
            {
                Name = "时间",
                Labeler = value => new DateTime((long)value).ToString("HH:mm:ss"),
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray, 1)
            });

            // 配置Y轴
            YAxis.Add(new Axis
            {
                Name = "数据值",
                Labeler = value => value.ToString("N0"),
                SeparatorsPaint = new SolidColorPaint(SKColors.LightGray, 1)
            });

            serialPort = new SerialPort();
            serialPort.DataReceived += SerialPort_DataReceived;

            // 初始化定时器
            timer = new Timer(1000); // 每 1 秒更新一次数据
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            // 创建数据库和表
            CreateDatabaseAndTable();
        }
        private void CreateDatabaseAndTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 创建数据库
                    string createDatabaseQuery = "CREATE DATABASE IF NOT EXISTS serial_data_db";
                    using (MySqlCommand createDatabaseCommand = new MySqlCommand(createDatabaseQuery, connection))
                    {
                        createDatabaseCommand.ExecuteNonQuery();
                    }

                    // 切换到新创建的数据库
                    connection.ChangeDatabase("serial_data_db");

                    // 创建表
                    string createTableQuery = "CREATE TABLE IF NOT EXISTS serial_data (" +
                                              "id INT AUTO_INCREMENT PRIMARY KEY," +
                                              "time DATETIME," +
                                              "value TINYINT" +
                                              ")";
                    using (MySqlCommand createTableCommand = new MySqlCommand(createTableQuery, connection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("数据库和表创建成功！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"创建数据库和表时出错: {ex.Message}");
                }
            }
        }
        public void ConfigureSerialPort(SerialPortConfig config)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            serialPort.PortName = config.PortName;
            serialPort.BaudRate = config.BaudRate;
            serialPort.DataBits = config.DataBits;
            serialPort.StopBits = config.StopBits;
            serialPort.Parity = config.Parity;

            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"打开串口失败: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
          
            int bytes = serialPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            serialPort.Read(buffer, 0, bytes);

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {

                var receivedData = Encoding.ASCII.GetString(buffer);
                var now = DateTime.Now;
                var chartvalue = double.Parse(buffer);
                dataPoints.Add(new DateTimePoint(now, chartvalue));
                SaveToDatabase(now, chartvalue);

                // 获取主窗口实例
                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    // 将接收到的数据追加到文本框中
                    mainWindow.ReceiveTextBox.Text += receivedData;
                }
            })); 
        }


        private void SaveToDatabase(DateTime time, double value)
        {
            string databaseConnectionString = connectionString + "Database=serial_data_db";
            using (MySqlConnection connection = new MySqlConnection(databaseConnectionString))
            {
                try
                {
                    connection.Open();

                    // 插入数据
                    string insertQuery = "INSERT INTO serial_data (time, value) VALUES (@time, @value)";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@time", time);
                        insertCommand.Parameters.AddWithValue("@value", value);
                        insertCommand.ExecuteNonQuery();
                    }

                    Console.WriteLine("数据插入成功！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"插入数据时出错: {ex.Message}");
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            //{
            //    Random random = new Random();
            //    dataPoints.Add(new DateTimePoint(DateTime.Now, random.Next(0, 100)));
            //}));
        }

        public void SendData(string data)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Write(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送数据失败: {ex.Message}");
            }
        }
    }
}
