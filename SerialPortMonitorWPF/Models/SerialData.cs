namespace SerialPortMonitorWPF.Models
{
    // 该类表示从串口接收到的数据
    public class SerialData
    {
        // 存储接收到的字节数据
        public byte[] Data { get; set; }
        // 标记数据是否为十六进制格式
        public bool IsHexFormat { get; set; }
    }
}    