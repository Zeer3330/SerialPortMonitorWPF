namespace SerialPortMonitorWPF.Models
{
    // 该类用于存储串口的配置信息
    public class SerialPortConfig
    {
        // 波特率，用于设置串口通信的数据传输速率
        public int BaudRate { get; set; }
        // 数据位，指定每个数据帧中包含的数据位数
        public int DataBits { get; set; }
        // 停止位，用于标识一个数据帧的结束
        public System.IO.Ports.StopBits StopBits { get; set; }
        public System.IO.Ports.Parity Parity { get; set; }
        public string PortName { get; set; }
    }
}    