   M S B A M L   `   `   `  ����E  APhysicsSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PhysicsSim
PhysicsSim  5   	       PhysicsSim.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PhysicsSim     5   	     � Title$  PHYSICS DEMO��5   	   $	��450��6   $	��800��6+    � Icon$ images/logo.ico��67    � WindowState$ 	Maximized=�6N    � 	Resources 5
         
lightGreen  	darkGreen  brightGreen  logo  	logoLight  
logoBright��%�   &	        &	      &	 >     &	 ]     &	 |     &	 �     ��5   
   #AFDCE5��66   ��5   
   #1B5B0E��65   ��5   
   #2CFC00��67   ��5   
   #0C343C��60   ��5   
   #4C7C85��65   ��5   
   #FFE59A��66   5      .��5      � #��[ 6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��2��6"   $��Center=�66   $��Center=�6S   .+�5      �� $	��100��6   $��images/logo.png��6!   6=   5      ]� 5   
   $��1��6   $��0��6#   .+�5       �� -   5        $	��Top=�5       $��0, 0, 5, 10q�6*   $	��200��6?   $��75��6K   $��Optics��5!      $��
Constantia)�6&   $��25(�6>   #��[ 6L   #��[ 5"      #��[ 6=    5#       �� 
TargetType �� Property �� Value��%d   '��       �� 5$      " ��6   .�5%      ��  �@��CornerRadius$ 20��69   6F   5&      5'      5(      � 5)      $��Center=�6   $��Center=�61   .+�5*      �� $	��400��6   $
��Auto��6%   $��	12,12,8,0q�63   $	��Top=�6F   $��.5q�6^   	 �� CornerRadius$	 18��6s   
 J� Effect
 5+      3  -System.Windows.Media.Effects.DropShadowEffect  5,         
BlurRadius$ 5Q�6+      Opacity$ .5Q�6:      ShadowDepth$ 3Q�6G   ._�5.      �� $	 15��6   ._�5/      � .+�50      �� -   5        $
��Mask��50   %   
�������#��[ 6D   $��.5q�6h   $	 13��6}   �� 51      .+�52   "   �� $��/images/optics.png��6(    J� OpacityMask 53   "   !� 54   &    !� Visual 62   ��  �� ElementName$
 Mask��56      57      58      59      5:      5;      ]� 5<   
   $��1��6   $��1��6#   .+�5=      �� -   5        $	��Top=�5=      $��5, 0, 0, 10q�6*   $	��200��6?   $��75��6K   $��
Kinematics��5>      $��
Constantia)�6+   $��25(�6C   #��[ 6Q   #��[ 5?      #��[ 6=    5@      ��%_   '��       �� 5A      " ��6   .�5B      ��  �@��CornerRadius$ 20��69   5C      5D      5E      � 5F      $��Center=�6   $��Center=�60   .+�5G      �� $	��400��6   $
��Auto��6%   $��	12,12,8,0q�63   $	��Top=�6F   $��.5q�6^   $	 18��6s   
 5H        5I      $ 5Q�6+   $ .5Q�6:   $ 3Q�6G   ._�5K      �� $	 15��6   ._�5L      � .+�5M      �� -   5        $��Mask2��5M   %   
�������#��[ 6E   $��.5q�6i   $	 13��6~   �� 5N      .+�5O   "   �� $��/images/motion.jpg��6(    5P   "   !� 5Q   &    62   �� $ Mask2��5S      5T      5U      5V      5W      5X      5Y      5Z      