   M S B A M L   `   `   `  ����E  APhysicsSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PhysicsSim
PhysicsSim  5   	   !    PhysicsSim.KinematicsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   *localclr-namespace:PhysicsSim     5   	     � Title$  KinematicsWindow��5   	   $	��450��6"   $	��800��6/    � WindowState$ 	Maximized=�6;    � Icon$ images/logo.ico��6S    � 	Resources 5	         
lightGreen  	logoLight  
logoBright��%{   &	        &	      &	 >     ��5
   
   #D4F0F5��66   ��5   
   #4C7C85��65   ��5   
   #FFE59A��66   5      .��5      � #��[  6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $	��100��6"   $	��200��61   $
��Left=�6@   #��[ 6[   .+�5      �� $��Center=�6   $��20(�61   $��
Constantia)�6?   #��[ 6W   .��6�   Speed6�   �� 5      -   5        $��SpeedTextBox��5   -   $��Center=�5   p   $
��Auto��6�    �� TextAlignment$ Center=�5      #��[ 5      #��[ 6>   #��[ 6g   ��5      ��  �� ElementName$ SpeedSlider�� �� Path$ Value�	 �� UpdateSourceTrigger$	 PropertyChanged=�5   g   �� 5       -   5        $��SpeedSlider��5    n   
 � Maximum$	
 200Q�6    �� TickPlacement$ BottomRight=�6#    �� TickFrequency$	 0.1Q�6?    �� IsSnapToTickEnabled$
 True��6S   #��[ 6�   6�   5!      �� 5"   
   $��0��6   $��2��6"   $��Center=�62   .+�5#      �� -   5        $��airResistanceCheck��5$       d� 	IsChecked$ FalseY�5#      $��20, 10, 0, 0q�6)   $��
Constantia)�5$   1   $��15(�6I   .��6W   Air resistance6g   5%      �� 5&   
   $��0��6   $��3��6"   $��20, 7, 0, 0q�62   .+�5'      �� -   5        $��ObjectSelector��5'      $��
Constantia)�6b   $��15(�6z   $
��Auto��6�   5(      5)      �� 5*   
   $��0��6   $��4��6"   $��Right=�62   $	��100��6N   $	��405��6]    �� Orientation$ 
Horizontal=�6l   .+�5+      �� -   5        $	��100��5+      $	��200��6$   $��50��63   $��
0, 0, 5, 0q�6[   #��[ 5,      $��Clear��6=   $��
Constantia)�6M   $��20(�6e   #��[ 5-      #��[ 6>    5.       �� 
TargetType �� Property �� Value��%d   '��       �� 5/      " ��6   .�50      ��  �@��CornerRadius$ 15��69   6F   51      52      53      �� 54      -   5        $	��100��54      $	��200��6$   $��50��63   $��
0, 0, 5, 0q�6Z   #��[ 55      $
��Fire��6=   $��
Constantia)�6L   $��20(�6d   #��[ 56      #��[ 6>    57      ��%d   '��       �� 58      " ��6   .�59      ��  �@��CornerRadius$ 15��69   6F   5:      5;      5<      5=      �� 5>   
   $��0��6   $��1��6#   $	��100��63   $	��200��6B   $
��Left=�6Q   #��[ 6l   .+�5?      �� $��Center=�6   $��20(�61   $��
Constantia)�6?   #��[ 6W   .��6�   Angle6�   �� 5@      -   5        $��AngleTextBox��5A   -   $��Center=�5@   p   $
��Auto��6�   $ Center=�5A      #��[ 5B      #��[ 6>   #��[ 6g   ��5@      �� $ AngleSlider��$ Value�$	 PropertyChanged=�5B   g   �� 5C      -   5        $��AngleSlider��5C   k   $
 90Q�6   $ BottomRight=�6"   $ 1Q�6>   $
 True��6P   #��[ 6~   6�   5D      �� 5E   
   -	   5        $��field��5E   B   $��0��6   $��1��6!   $��5��6.   $��10, 0, 0, 0q�6O   .+�5F      �� -
   5        $��Himars��5F   d    
 Himars#��[ 6   $	��115��65    �� Left$ 0��6B    �� Bottom$ 0��6R   6t   �� 5G      -   5        $��himarsLauncher��5G   l     HimarsLauncher#��[ 6   $��45��6=   $ 5��6I   $ 60��6Y   6�   5H      5I      5J      