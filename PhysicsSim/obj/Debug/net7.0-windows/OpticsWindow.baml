   M S B A M L   `   `   `  ����E  APhysicsSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PhysicsSim
PhysicsSim  5   	       PhysicsSim.OpticsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   *localclr-namespace:PhysicsSim     5   	     � Title$  OpticsWindow��5   	   $	��450��6   $	��800��6+    � WindowState$ 	Maximized=�67    � Icon$ images/logo.ico��6O    � 	Resources 5	         
lightGreen  	logoLight  logo  
logoBright��%�   &	        &	      &	 >     &	 ]     ��5
   
   #D4F0F5��66   ��5   
   #4C7C85��65   ��5   
   #0C343C��60   ��5   
   #FFE59A��66   5      .��5      � #��[  6   .+�5   
   � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��1* �6    �� 5      $��3* �6    �� 5      $��1* �6    .+�5      �� -   5        $��RaysCheckBox��5   ]   $��0��5      $��0��6$    d� 	IsChecked$ FalseY�64   $��
0, 0, 5, 0q�6F   $��
Constantia)�5      $��15(�6/   .��6=   	Show rays6H   �� 5      $��0��6   $��1��6&   $��5, -3, 0, 0q�66   .+�5       �� -   5        $��LensSelector��5       $��
Constantia)�6b   $��15(�6z   $
��Auto��6�   .@�5!      ��  �� 
IsSelected$
 True��6#   $�� ��65   6B   �� 5"      $��Convex Lens��6#   $��
Constantia)�69   $��15(�6Q   6a   �� 5#      $��Concave Lens��6#   $��
Constantia)�6:   $��15(�6R   6b   5$      5%      �� 5&      $��0��6   $��2��6&   $��5, -3, 0, 0q�66   .+�5'      �� -   5        $��ObjectSelector��5'      $��
Constantia)�6f   $��15(�6~   $
��Auto��6�   .@�5(      �� $
 True��6#   $�� ��65   6B   5)      5*      �� 5+      $��2��6   $��0��6&   $	��100��66   $��
0, 0, 5, 0q�6B   $��Center=�6V   $��Center=�6s   #��[ 6�   .+�5,      �� $��Center=�6   $��
Constantia)�65   $��15(�6M   #��[ 6[   .��6�   Focus6�   �� 5-      $��Center=�6t   $
��Auto��6�    �� TextAlignment$ Center=�6�   #��[ 6�   #��[ 6�   #��[ 6  ��6   �� 	 �� ElementName$	 FocusSlider��
 �� Path$
 Value� �� UpdateSourceTrigger$ PropertyChanged=�6  �� 5.      -   5        $��FocusSlider��5.   p    � Maximum$	 100Q�6    �� TickPlacement$ BottomRight=�6'    �� TickFrequency$ 1Q�6C    �� IsSnapToTickEnabled$
 True��6U   #��[ 6�   6�   5/      �� 50      $��2��6   $��1��6&   $	��100��66   $��
5, 0, 0, 0q�6B   $��Center=�6V   $��Center=�6s   #��[ 6�   .+�51      �� $��Center=�6   $��
Constantia)�65   $��15(�6M   #��[ 6[   .��6�   
Distance6�   �� 52      $��Center=�6w   $
��Auto��6�   $ Center=�6�   #��[ 6�   #��[ 6�   #��[ 6	  ��6   �� $	 DistanceSlider��$
 Value�$ PropertyChanged=�6	  �� 53      -   5        $��DistanceSlider��53   r   $
 1000Q�6   $ BottomRight=�6(   $ 10Q�6D   $
 True��6W   #��[ 6�   6�   54      �� 55      -   5        $��field��55   F   $��1��6   $��4��6"   $��0��66   $
��Auto��6S   $
��Auto��6`   56      57      58      59      