   M S B A M L   `   `   `  ����F  BWorkTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WorkTracker.ComponentsWorkTracker.Components  5      !    WorkTracker.Components.Menu   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      0local$clr-namespace:WorkTracker.Components  5        � 	Resources  5
         PopupButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers	 P� Property
 P� Value��%�  &	        �� 5   
   " ��5      .�5      ��  � ��
 ���� 5      �� 5       � ��
 ���� 5      �� 5       � ��
 ���� 5      �� 5       � ��$ 0q�5      �� 5       � ��$ Center=�5      �� 5       � ��$ Center=�5      �� 5       � ��$ 0q�5      �� 5       � ��$ 50��5      �� 5       � �� 5      �� 5      " ��6&   .R�5       �� $ border��6!   #��z��5!   !   #��z��5"   !   #��z��5#   !   $
 true��5$   !   ._�5%      �� $ contentPresenter��6/   $|�False��5&   /   $
��True��5'   /   # z 5(   /   #��z��5)   /   #��z��5*   /   #��z��5+   /   5,       5-      P� 5.      	 �@J�IsMouseOver$

 True��5/   &   . �50   "   ��  � ��
 ���� 51   )   �� 52   "    � ��
 ���� 53   )   54      P� 55      	 � ��$

 True��56   &   . �57   "   ��  � ��
 ���� 58   )   �� 59   "    � ��
 ���� 5:   )   5;      5=      5?      5@      5A      .��5B      #  MaterialDesignThemes.Wpf.Card     MaterialDesignCardBackground#���  6   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp5=�6V   .��5C   
   � -   5         �Name$ GridNav��5C      $��60��6     � RowDefinitions 5D      �� 5E   
   $��60 �6   �� 5F   
   $��* �6   �� 5G      $
��auto �6    .+�5I      � $��0��6   .+�5J      d� $��8 0 0 0q�6   $��50��5K      $��50��5L      $
��Left=�5M       � Triggers 5N      :� 5O       :� RoutedEvent$ ToggleButton.Unchecked��6'   .[�5P      �� .��5Q   "   �� -   5         ��Name$ HideStackPanel��5Q   -   .��5R   &   W�  �� 
TargetName$ GridNav��66    �� TargetProperty$ Width�5S   6    g� 	BeginTime	 0:0:05T   6    W� From 2105U   6   	 W� To 605V   6    g� Duration$ 0:0:0.5D�5W   6   5X   '   5Y   #   5Z      5[      :� 5\      $ ToggleButton.Checked��6'   .[�5]      �� .��5^   "   �� -   5        $ ShowStackPanel��5^   -   .��5_   &   W� $ GridNav��66   $ Width�5`   6   	 0:0:05a   6    605b   6    2105c   6   $ 0:0:0.5D�5d   6   5e   '   5f   #   5g      5h      .��5j      ��   Logo#��[ 6   5k      �� 5m      $��WorkTracker��6     SecondaryHueMidBrush#���  5n      $
��Bold"�5o      $��16(�5p      $��Center=�5q      $
��Left=�5r      $��60 0 0 0q�5s      5t      �� 5u      $��1��6   $��
-1 40 0 20q�5v      ��5w      �� *MenuItemsData+��5x      �� 5y      .R�5z      �� #���   6!   $��Stretch=�5|   !   $��Stretch=�5}   !    � DataContext 5{   !   �� ��5~   !   �� *ItemCommand+ � ToolTip 5   !   �� *Text+.��5�      %   WorkTracker.Components.MenuItem  $��Center=�5�   .      Text 5�   .   �� *Text+   IconKind 5�   .   �� *
IconKind+   ItemCommand 5�   .   �� *ItemCommand+   
IsSelected 5�   .   �� *IsSelectedInitialy+5�   .   5�      5�      5�      �� 5�      #���   6   $��2��6@   $��Stretch=�5�      $��Stretch=�5�       5�      �� *
LogoutItem+��5�      �� *ItemCommand+ 5�      �� *Text+.��5�        $��Center=�5�   "    5�   "   �� *Text+ 5�   "   �� *
IconKind+ 5�   "   �� *ItemCommand+ 5�   "   �� *IsSelectedInitialy+5�   "   5�      5�      5�      5�      