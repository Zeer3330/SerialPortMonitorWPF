   M S B A M L   `   `   `  ����[  WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF  5   	    SerialPortMonitorWPF+   %SerialPortMonitorWPF.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5   	     � Title$  Serial Port Monitor��5   	   $��700         ��6%   $	��800��5      .��5      � -   5         �Name$ 	LiveChart��5      $��Center=�6   $��0,0,0,20q�6;   .+�5	   
   5   /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  -   5          Name$ Chart��5	      ,   &LiveChartsCore.SkiaSharpView.WPF.Chart   LegendPosition$ Right=�5      $	��610��6$   $
��Left=�60   $	��Top=�6K   $	��500��6c   $��0,19,0,0q�6p      Series 5
      �� *Series+   XAxes 5      �� *XAxis+   YAxes 5      �� *YAxis+5   p   �� 5   
   $��串口参数配置��6   $��
5,525,5,10q�6#   $	��130��67   .��5      ��  �� Orientation$ 
Horizontal=�6   $��10q�62   .+�5      �� .+�5      �� $��
串口号:��6   �� 5      -   5         ��Name$ PortNameComboBox��5      $	��100��69   �� 5      -   5        $��刷新��5      $��5q�6K   5      �� 5      .+�5      �� $��
波特率:��6   �� 5      -   5        $ BaudRateComboBox��5      $	��100��69   .@�5      �� $
��9600��6'   	 �� 
IsSelected$
	 True��66   �� 5      $��115200��6'   5      5      �� 5      $��10,0,0,0q�6   .+�5      �� $��
数据位:��6   �� 5      -   5        $ DataBitsComboBox��5      $	��100��69   .@�5       �� $��7��6'   �� 5!      $��8��6'   $
	 True��63   5"      5#      �� 5$      $��10,0,0,0q�6   .+�5%      �� $��
停止位:��6   �� 5&      -   5        $ StopBitsComboBox��5&      $	��100��69   .@�5'      �� $��1��6'   $
	 True��63   �� 5(      $��2��6'   5)      5*      �� 5+      $��10,0,0,0q�6   .+�5,      �� $��奇偶校验位:��6   �� 5-      -   5        $ ParityComboBox��5-      $	��100��67   .@�5.      �� $
��None��6'   $
	 True��66   �� 5/      $	��Odd��6'   �� 50      $
��Even��6'   51      52      �� 53      -	   5        $��应用配置��53      
 J� RenderTransformOrigin$
 1.913,0.406&�6H   $��Center=�6l   $	��143��6�   $��37��6�   $��50,0,0,0q�6�   54      55      �� 57   
   $��数据收发��6   $��0,30,0,0q�6!   $��Right=�63   $��Visible=�6V   $��Right=�6�   $	��Top=�6�   $	��150��6�   .��58      �� $ Vertical=�6   .+�59      �� -
   5         ��Name$ SendTextBox��59      $��40��6/   $��10q�6;   .��6G    � 	Resources 5:         	HelpBrush j� TileMode �� Opacity j� Stretch j� 
AlignmentX !� Visual��%�   &	        !� 5;      $
 None=�68   $	 0.3Q�6H   $
 None=�6V   $
 Left=�6e    5<      �� 5=   "   $��待发送数据��6,   5?      5@      .��6)   ��5A      �� 5B       �� 
TargetType" ��6     �� Triggers 5C      P� 5D   "    P� Property � �� P� Value$  ��6:   . �5E   &   ��  �� Property � �� �� Value# [  6C   5F   #   5H      .��5I   %   5J      �� 5L      -   5        $��发送数据��5L      $��10q�6I   $��30��6U   �� 5M      -   5        $ ReceiveTextBox��5M      $��10q�62    �� 
IsReadOnly$
 True��6>   $��40��6Q   .��6]    5N      ��%�   &	        !� 5O      $
 None=�68   $	 0.3Q�6H   $
 None=�6V   $
 Left=�6e    5P      �� 5Q   "   $��已接收数据��6,   5S      5T      .��6)   ��5U      �� 5V      " ��6     5W      P� 5X   "    � ��$  ��6:   . �5Y   &   ��  � ��# [  6C   5Z   #   5\      .��5]   %   5^      5_      5`      5a      5b      