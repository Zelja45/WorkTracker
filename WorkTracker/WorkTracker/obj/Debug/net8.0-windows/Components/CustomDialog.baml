   M S B A M L   `   `   `  ����F  BWorkTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WorkTracker.ComponentsWorkTracker.Components  5   	   )    #WorkTracker.Components.CustomDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   0local$clr-namespace:WorkTracker.Components  5   	   -   5          � 
ResizeMode$  NoResize=�5	   	    � WindowStyle$
 None=�5
   	    � SizeToContent$ Height=�5   	    � Title$ CustomDialog��5   	   $
��auto��6   $	��300��6,    � AllowsTransparency$
 True��5   	      MaterialDesignPaper#���   5   	    � WindowStartupLocation$ CenterScreen=�6<   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ 0q�6   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp5=�5      	   CornerRadius$	 15��5      
   CaptionHeight$
 0Q�5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� -   5        $��Title��5        MaterialDesignBody#���  5      $��18(�5      $��Medium"�5      $
��Wrap=�5       � 5!   
   $��1��6    � ColumnDefinitions 5"      �� 5#      $
��auto �6#   �� 5$      $��* �6#   .+�5&      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        $
��Icon��5&   &   $��30��5'   &   $��30��5(   &   $��5q�5)   &      Kind$ Information=�5*   &   �� 5+      -   5        $��Description��5,      $��Center=�5+        SecondaryHueMidBrush#���  5-      $��1��5.      $��12(�5/      $
��Wrap=�50      51      �� 52   
   -   5        $��
YesNoPanel��52      $��2��53      $y�Hidden=�54       �� Orientation$ 
Horizontal=�55      $��Right=�56      .+�57      �� -   5        $��noBtn��57        GlobalCornerRadius+  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius# [ 58        No#���  59        MaterialDesignRaisedButton#��[ 5;      �� 5<      $��5��6   �� 5=      -   5        $��yesBtn��5=      # [ 5>        Yes#���  5?      #��[ 5A      5B      �� 5C   
   -   5        $��OKPanel��5C      $��2��5D      $y�Hidden=�5E      $ 
Horizontal=�5F      $��Right=�5G      .+�5H      �� -	   5        # [ 5H      $��OK��5I      #��[ 5K      5L      5N      5P      