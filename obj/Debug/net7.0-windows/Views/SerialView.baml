   M S B A M L   `   `   `  ����K  GAxisConfigurator, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AxisConfigurator.ViewsAxisConfigurator.Views  5      '    !AxisConfigurator.Views.SerialView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:AxisConfigurator.Views     5      .��5      ��   �� Orientation$  
Horizontal=�6   $��20��6*   .+�5	   
   �� $	��100��6   ��6   �� *ConnectCommand+��5
      �� 5       �� 
TargetType" ��6    �� Triggers 5      �� 5       �� Binding 6&   �� *PortConnected+ �� Value True6H   .a�5      ��  �� Property � �� �� Value
 � � �6;   �� 5       � ��$ 
Disconnect��68   5      �� 5       6&   �� *PortConnected+	 False6H   .a�5      ��  � ��
 �  ��6;   �� 5       � ��$ Connect��68   5      5      5      �� 5   
    �� SelectedIndex$ 0��6   $��70��6%   $��10 0 0 0q�60   ��6B   �� *
PortList+ �� SelectedItem 6c   �� *SelectedPort+	 �� Mode$	 TwoWay=�5      �� 5   
   $��
AutoSearch��6   $��10 0 0 0q�6&   ��68   �� *AutoSearchCommand+5      5      