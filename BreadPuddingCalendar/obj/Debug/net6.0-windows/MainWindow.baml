   M S B A M L   `   `   `  ����O  KBreadPuddingCalendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BreadPuddingCalendarBreadPuddingCalendar  5   	   %    BreadPuddingCalendar.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:BreadPuddingCalendar     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � 	Resources 5
         ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value�� 5   
   %�  &	        �� 5      " ��6(   .�5      ��  � �� �	#FFC7EEFA6/   �� 5       � �� �	#FF7372B56/   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� #��z��6%   #��z��6E   #��z��5   %   $ 10��5   %   #��z��67   $��4q�5   %   ._�5   "   �� #��z��63   $��Center=�5   3   $��Center=�6P   $��20(�5   3   $
��Bold"�6K   5   #   5      5      5       5"      P� 5#       �@J�IsMouseOver$
	 True��65   . �5$      ��  � �� �	#FF4646AB67   5%      P� 5'       � ��$
	 True��63   . �5(      ��  � �� �	#FF8AC28A67   5)      5,      5-      .��50      � ���	#FFD0E9DF
 � RowDefinitions
 51   
   �� 52      $��50 �6   �� 53      .+�56   
   � $��0��6    � ColumnDefinitions 57      �� 58      �� 59      �� 5:      .+�5=      �� -   5        $��0��5=      $	��100��6%   $��40��61   $	��<==��5>      $��Right=�5?      #��[  61   �� 5A      -   5        $��2��5A      $	��100��6%   $��40��61   $	��==>��5B      $
��Left=�5C      #��[  60   �� 5E      -   5         ��Name$ yearMonthField��5F      $��1��5E      $	��200��6(   $��40��64   $��TEST STRING��6@   $��Center=�5F   0   $��Center=�5G       �� TextAlignment$ Center=�63   $��25(�5H      $
��Bold"�6&   �� 5J      -   5        $
��Left=�5J      $��25��60   $��25��6;   $��10q�5K      6!   5M      � 5O   
   -   5         �Name$ calendarGrid��5O   (   $��1��6   $��15q�6   
 5P      �� 5Q      �� 5R      �� 5S      �� 5T      �� 5U       5W      �� 5X      �� 5Y      �� 5Z      �� 5[      �� 5\      �� 5]      �� 5^      5j      5l      5m      