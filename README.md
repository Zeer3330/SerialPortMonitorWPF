# SerialPortMonitorWPF

## 目录
1. [项目概述](#项目概述)
2. [技术要点](#技术要点)
    - [WPF 框架](#wpf-框架)
    - [串口通信](#串口通信)
    - [SkiaSharp 与 LiveChartsCore 绘图](#skiasharp-与-livechartscore-绘图)
    - [编码与文本处理](#编码与文本处理)
    - [异步编程支持](#异步编程支持)
3. [项目依赖](#项目依赖)
4. [安装步骤](#安装步骤)
5. [使用说明](#使用说明)
6. [注意事项](#注意事项)
7. [贡献与反馈](#贡献与反馈)

## 项目概述
SerialPortMonitorWPF 是一个基于 WPF（Windows Presentation Foundation）的应用程序，用于监控和管理串口通信。该项目允许用户配置串口参数，如波特率、数据位、停止位和奇偶校验等，并通过串口发送和接收数据。同时，使用 LiveChartsCore 进行数据可视化，为用户提供直观的数据分析体验。

## 技术要点

### WPF 设计
- **XAML 与代码分离**：项目使用 XAML（eXtensible Application Markup Language）来定义用户界面，而业务逻辑则在 C# 代码中实现。例如，`App.xaml.cs` 文件负责应用程序的全局初始化和清理逻辑，而 `Views/MainWindow.xaml.cs` 则处理主窗口的交互逻辑。这种分离方式使得代码结构清晰，易于维护和扩展。
```csharp
// App.xaml.cs 中的全局初始化逻辑
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
}
```
- **控件与布局**：使用 WPF 提供的各种控件，如 `ComboBox`、`Button` 和 `TextBox` 等，构建用户界面。通过布局管理器（如 `Grid`、`StackPanel` 等）来安排控件的位置和大小。这种布局方式使得界面设计灵活，能够适应不同的需求。
-- **带背景文字的输入框** :根据textbox.text属性的值自动为输入框添加背景，实现输入框中无数据时带背景文字，有数据时自动隐藏。
```csharp
 <TextBox x:Name="SendTextBox" Height="40" Margin="10">
                    <TextBox.Resources>
                        <VisualBrush x:Key="HelpBrush" TileMode="None" Opacity="0.3" Stretch="None" AlignmentX="Left">
                            <VisualBrush.Visual>
                                <TextBlock Text="待发送数据"/>
                            </VisualBrush.Visual>
                        </VisualBrush>
                    </TextBox.Resources>
                    <TextBox.Style>
                        <Style TargetType="TextBox">
                            <Style.Triggers>
                                <Trigger Property="Text" Value="">
                                    <Setter Property="Background" Value="{StaticResource HelpBrush}"/>
                                </Trigger>
                            </Style.Triggers>
                        </Style>
                    </TextBox.Style>
                    </TextBox>
```
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
- **MVVM模式**：MVVM 模式是一种软件架构模式。它把软件分为三个主要部分，即模型（Model）、视图（View）和视图模型（ViewModel）。模型负责存储数据，视图负责显示数据，视图模型则负责处理业务逻辑，起到连接模型和视图的作用。这种模式将视图和业务逻辑分离，能够提高代码的可维护性和可测试性，让代码结构更加清晰，有利于团队协作开发。该项目严格按照MVVM模式构建，使我对于MVVM模式的理解有很大的帮助，对于如何在具体项目中应用该模式有了进一步认知。
├── Models/                # 数据模型类，用于定义数据结构
├── ViewModels/            # MVVM 视图模型类，处理视图逻辑和数据绑定
├── Views/                 # WPF 视图文件，定义用户界面

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

### 编码与文本处理
在数据收发过程中，编码与文本处理是非常重要的环节。通过学习 `System.Text` 命名空间中的类，我了解到了不同的字符编码方式，并学会了如何进行编码和解码。这确保了数据在传输过程中的正确性。

### SkiaSharp 与 LiveChartsCore 绘图
- **SkiaSharp**：SkiaSharp 是一个跨平台的 2D 图形库，用于在项目中实现图形绘制。项目中使用了 `SkiaSharp.Views.Desktop.Common` 和 `SkiaSharp.Views.WPF` 等相关库。SkiaSharp 提供了丰富的图形绘制功能，如绘制线条、图形、文本等，能够满足项目的可视化需求。[SkiaSharp 官方文档](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/)
- **LiveChartsCore**：LiveChartsCore 是一个高性能的图表库，结合 SkiaSharp 实现数据可视化。在 `App.xaml.cs` 中，通过 `LiveCharts.Configure` 方法配置全局字体，以支持中文显示。LiveChartsCore 提供了多种图表类型，如折线图、柱状图、饼图等，能够直观地展示数据。[LiveChartsCore 官方文档](https://livecharts.dev/)
- SkiaSharp 和 LiveChartsCore 是两个非常强大的库，它们为项目的数据可视化提供了有力支持。通过学习 SkiaSharp 的图形绘制功能，我能够绘制出各种图形和文本。而 LiveChartsCore 则提供了多种图表类型，让我能够直观地展示数据。

### 编码与文本处理
- **System.Text 命名空间**：项目使用 `System.Text` 命名空间中的类来处理编码和文本转换。例如，`System.Text.Encoding` 和 `System.Text.Encoding.CodePages` 等类用于处理不同的字符编码。在数据收发过程中，需要对数据进行编码和解码，以确保数据的正确传输。[System.Text 官方文档](https://docs.microsoft.com/en-us/dotnet/api/system.text?view=net-8.0)

### 异步编程支持
- **System.Threading.Tasks 命名空间**：项目使用 `System.Threading.Tasks` 命名空间中的类来实现异步编程，提高应用程序的响应性能。例如，在串口通信中，可以使用异步方法来发送和接收数据，避免阻塞主线程。异步编程能够提高程序的并发处理能力，提升用户体验。[System.Threading.Tasks 官方文档](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks?view=net-8.0)

## 项目依赖
项目使用了多个 NuGet 包，以下是部分重要的依赖项：
- `System.IO.Ports`：用于串口通信。
- `SkiaSharp` 和 `SkiaSharp.Views.WPF`：用于图形绘制。
- `LiveChartsCore.SkiaSharpView`：用于数据可视化。
- `System.Text.Json`：用于 JSON 数据的序列化和反序列化。
- `Microsoft.Extensions.Logging.Abstractions`：用于日志记录。

## 安装步骤
1. **克隆项目**：打开命令行工具，运行以下命令克隆项目到本地。
```bash
git clone https://github.com/your-repo/SerialPortMonitorWPF.git
```
2. **打开项目**：使用 Visual Studio 打开项目文件（`.sln`）。
3. **恢复 NuGet 包**：Visual Studio 会自动恢复项目所需的 NuGet 包。如果没有自动恢复，可以右键单击解决方案，选择“管理解决方案的 NuGet 包”，然后点击“恢复”按钮。
4. **编译项目**：按下 `Ctrl + Shift + B` 编译项目。

## 使用说明
1. **启动应用程序**：编译成功后，按下 `F5` 启动应用程序。
2. **配置串口参数**：在主窗口中，选择可用的串口，并配置波特率、数据位、停止位和奇偶校验位等参数。点击“刷新”按钮可以刷新可用的串口列表。
3. **应用配置**：配置完成后，点击“应用配置”按钮，应用配置信息。
4. **发送数据**：在“待发送数据”文本框中输入要发送的数据，点击“发送数据”按钮，将数据通过串口发送出去。
5. **接收数据**：接收到的数据将显示在“已接收数据”文本框中。
6. **数据可视化**：使用 LiveChartsCore 绘制的图表会实时显示接收到的数据。



## 注意事项
- **串口连接**：在使用串口通信前，请确保外部设备已正确连接到计算机，并且串口参数配置正确。
- **数据格式**：在发送和接收数据时，请确保数据格式正确，避免出现乱码或数据丢失的情况。
- **异常处理**：在使用过程中，如果出现异常情况，如串口连接失败、数据发送失败等，请查看异常信息并进行相应的处理。

## 贡献与反馈
如果您对本项目有任何建议或发现了问题，请随时在 GitHub 仓库中提交 Issue 或 Pull Request。我们欢迎您的贡献！
