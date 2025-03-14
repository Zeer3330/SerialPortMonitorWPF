# SerialPortMonitorWPF

## 目录
1. [项目概述](#项目概述)
2. [技术要点](#技术要点)
    - [WPF 框架](#wpf-框架)
    - [串口通信](#串口通信)
    - [数据绑定与 MVVM 模式](#数据绑定与-mvvm-模式)
    - [SkiaSharp 与 LiveChartsCore 绘图](#skiasharp-与-livechartscore-绘图)
    - [编码与文本处理](#编码与文本处理)
    - [异步编程支持](#异步编程支持)
3. [项目依赖](#项目依赖)
4. [使用说明](#使用说明)
5. [贡献与反馈](#贡献与反馈)

## 项目概述
SerialPortMonitorWPF 是一个基于 WPF（Windows Presentation Foundation）的应用程序，用于监控和管理串口通信。该项目允许用户配置串口参数，如波特率、数据位、停止位和奇偶校验等，并通过串口发送和接收数据。

## 技术要点

### WPF 框架
- **XAML 与代码分离**：项目使用 XAML（eXtensible Application Markup Language）来定义用户界面，而业务逻辑则在 C# 代码中实现。例如，`App.xaml.cs` 文件负责应用程序的全局初始化和清理逻辑，而 `Views/MainWindow.xaml.cs` 则处理主窗口的交互逻辑。
```csharp
// App.xaml.cs 中的全局初始化逻辑
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
}
```
- **控件与布局**：使用 WPF 提供的各种控件，如 `ComboBox`、`Button` 和 `TextBox` 等，构建用户界面。通过布局管理器（如 `Grid`、`StackPanel` 等）来安排控件的位置和大小。

### 串口通信
- **System.IO.Ports 命名空间**：项目使用 `System.IO.Ports` 命名空间中的 `SerialPort` 类来实现串口通信。在 `Views/MainWindow.xaml.cs` 文件中，通过 `SerialPort.GetPortNames()` 方法获取可用的串口列表，并允许用户选择和配置串口参数。
```csharp
// 刷新可用串口列表
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
```

### 数据绑定与 MVVM 模式
- **数据绑定**：WPF 支持数据绑定，通过将视图（View）与视图模型（ViewModel）绑定，可以实现数据的自动更新和交互。在 `Views/MainWindow.xaml.cs` 中，通过 `DataContext` 属性将主窗口的视图模型 `MainWindowViewModel` 绑定到视图上。
```csharp
// 绑定视图模型
public MainWindow()
{
    InitializeComponent();
    DataContext = new MainWindowViewModel();
    RefreshSerialPorts();
}
```
- **MVVM 模式**：MVVM（Model-View-ViewModel）模式将视图和业务逻辑分离，提高了代码的可维护性和可测试性。视图负责显示数据，视图模型负责处理业务逻辑，模型负责存储数据。

### SkiaSharp 与 LiveChartsCore 绘图
- **SkiaSharp**：SkiaSharp 是一个跨平台的 2D 图形库，用于在项目中实现图形绘制。项目中使用了 `SkiaSharp.Views.Desktop.Common` 和 `SkiaSharp.Views.WPF` 等相关库。
- **LiveChartsCore**：LiveChartsCore 是一个高性能的图表库，结合 SkiaSharp 实现数据可视化。在 `App.xaml.cs` 中，通过 `LiveCharts.Configure` 方法配置全局字体，以支持中文显示。
```csharp
// 配置全局字体
LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
```

### 编码与文本处理
- **System.Text 命名空间**：项目使用 `System.Text` 命名空间中的类来处理编码和文本转换。例如，`System.Text.Encoding` 和 `System.Text.Encoding.CodePages` 等类用于处理不同的字符编码。

### 异步编程支持
- **System.Threading.Tasks 命名空间**：项目使用 `System.Threading.Tasks` 命名空间中的类来实现异步编程，提高应用程序的响应性能。例如，在串口通信中，可以使用异步方法来发送和接收数据，避免阻塞主线程。

## 项目依赖
项目使用了多个 NuGet 包，以下是部分重要的依赖项：
- `System.IO.Ports`：用于串口通信。
- `SkiaSharp` 和 `SkiaSharp.Views.WPF`：用于图形绘制。
- `LiveChartsCore.SkiaSharpView`：用于数据可视化。
- `System.Text.Json`：用于 JSON 数据的序列化和反序列化。

## 使用说明
1. 克隆项目到本地。
2. 打开项目文件（`.sln`），使用 Visual Studio 进行编译和运行。
3. 在主窗口中，选择可用的串口，并配置串口参数。
4. 点击“应用配置”按钮，应用配置信息。
5. 在输入框中输入要发送的数据，点击“发送数据”按钮，将数据通过串口发送出去。
6. 点击“刷新端口”按钮，刷新可用的串口列表。

## 贡献与反馈
如果您对本项目有任何建议或发现了问题，请随时在 GitHub 仓库中提交 Issue 或 Pull Request。我们欢迎您的贡献！
