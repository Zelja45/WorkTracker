   M S B A M L   `   `   `  ����F  BWorkTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WorkTracker.ViewWorkTracker.View  5      ?$clr-namespace:WorkTracker.ComponentsWorkTracker.Components  5      5clr-namespace:WorkTracker.UtilsWorkTracker.Utils  5
      $    WorkTracker.View.AdminHomeView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      *localclr-namespace:WorkTracker.View  5      5
components$clr-namespace:WorkTracker.Components  5      +utilsclr-namespace:WorkTracker.Utils  5
      .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $	��40* �6   �� 5      $	��60* �6   .+�5   
   +   %WorkTracker.Components.HomeViewHeader  6&   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      #  MaterialDesignThemes.Wpf.Card  $��10q�6"   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp3=�5   "   $��8q�5   "       MaterialDesignCardBackground#���   5   "     MaterialDesignBody#���  5   "      UniformCornerRadius$ 10Q�5   "   .��5       �   5!      �� 5"      $
��auto �6(   �� 5#      $��* �6(   �� 5$      $
��auto �6(   .+�5'      ��   NumberOfWorkers#���  6      SecondaryHueMidBrush#���  5)       $��Center=�5*       $
��Wrap=�5+       �� 5,      $��1��5-       #���  5.       $��Center=�5/       $��40(�50       $
��Bold"�51       $��Center=�52       ��5,       �� *NumberOfWorkers+52       �� 53       �� Orientation$ 
Horizontal=�6!   $��2��54   !   $��Center=�55   !   .+�56      ��   NumberOfDeactivatedAccounts#���  6    #���  57       $��Center=�58       �� 5:      #���  5;       $��Center=�5<       ��5:       �� *NumberOfDeactivatedWorkers+5<       5>      5?      5@        5A      $��10q�6"   $��1��6.   $	 Dp3=�5B   "   $��8q�5C   "   #���   5D   "   #���  5E   "   $ 10Q�5F   "   .��5G      �   5H      �� 5I      $
��auto �6(   �� 5J      $��* �6(   �� 5K      $
��auto �6(   .+�5N      ��   NumberOfManagers#���  6    #���  5O       $��Center=�5P       $
��Wrap=�5Q       �� 5R      $��1��5S       #���  5T       $��Center=�5U       $��40(�5V       $
��Bold"�5W       $��Center=�5X       ��5R       �� *NumberOfManagers+5X       �� 5Y      $ 
Horizontal=�6!   $��2��5Z   !   $��Center=�5[   !   .+�5\      �� #���  6    #���  5]       $��Center=�5^       �� 5`      #���  5a       $��Center=�5b       ��5`       �� *NumberOfDeactivatedManagers+5b       5d      5e      5f        5g      $��10q�6"   $��3��5h   "   $	 Dp3=�5i   "   $��8q�5j   "   #���   5k   "   #���  5l   "   $ 10Q�5m   "   .��5n      �   5o      �� 5p      $
��auto �6(   �� 5q      $��* �6(   .+�5s      ��   NumberOfSectors#���  6    #���  5t       $��Center=�5u       $
��Wrap=�5v       �� 5w      $��1��5x       #���  5y       $��Center=�5z       $��40(�5{       $
��Bold"�5|       $��Center=�5}       ��5w       �� *NumberOfSectors+5}       5~      5      5�        5�   
   $��10q�6   $��2��5�      $	 Dp3=�5�      $��8q�5�      #���   5�      #���  5�      $ 10Q�5�      .��5�      �   5�      �� 5�      $
��auto �6$   �� 5�      $��* �6$   .+�5�      ��   NumberOfFreeWorkers#���  6   #���  5�      $��Center=�5�      $
��Wrap=�5�      �� 5�      $��1��6@   $��Center=�6M   $��Center=�6h   $��40(�5�      #���  5�      $
��Bold"�5�      ��5�      �� *NumberOfFreeWorkers+5�      5�      5�      5�      5�      