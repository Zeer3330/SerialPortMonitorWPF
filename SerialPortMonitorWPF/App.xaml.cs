using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace SerialPortMonitorWPF
{
    public partial class App : Application
    {
        
            // 设置全局字体-中文
         
            
                
        
        protected override void OnStartup(StartupEventArgs e)
        {
            // 全局初始化逻辑
            // 例如可以在这里进行日志系统的初始化、配置文件的加载等
            base.OnStartup(e);
                LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
    
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 全局清理逻辑
            // 例如可以在这里关闭所有打开的文件、释放资源等
            base.OnExit(e);
        }
    }
}    