   M S B A M L   `   `   `  ����K  GAssignment 2 WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Assignment_2_WPF.ViewsAssignment_2_WPF.Views  5   	   I)clr-namespace:Assignment_2_WPF.ViewModelsAssignment_2_WPF.ViewModels  5   	   )    #Assignment_2_WPF.Views.ScheduleView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Assignment_2_WPF.Views     5   	   8vm)clr-namespace:Assignment_2_WPF.ViewModels     5   	     � Title$  Schedule Panel��5	   	   $	��600��6    $	��900��6-   ��5
      �� 5   
    �� ImageSource$ /Views/cat-dog.png��6    j� Stretch$
 Fill=�66   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Schedule Panel��6   $��24(�5      $
��Bold"�5      $��0,0,0,20q�5      � 5   
   $��20,37,-10,0q�6   $��2��6$   ��5      �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       $��* �6#   .+�5$      &   System.Windows.Controls.Calendar  -   5          Name$ Calendar��5$         IsTodayHighlighted$
 True��5'      
�������   SelectedDate 5%      �� *SelectedDate+ �� Mode$ TwoWay=�5'   1   &   System.Windows.Controls.DataGrid  5)      -   5        $��1��5)      	   AutoGenerateColumns$	 False��5+      
   
IsReadOnly$

 True��5,      $��76,3,130,340q�6)   
�������$
��Bold"�6�   ���	#FF6E6E6F���	#FF8ABD78   HorizontalGridLinesBrush �	#FFAAAAAA   VerticalGridLinesBrush �	#FFAAAAAA��5*      �� *	Schedules+   Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Schedule��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 50   '   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5/   '   �� *Type+50   '     51      $
 Date��6)   $
 Auto 53   '    52   '   �� *Date+ �� StringFormat$ d��53   '   55      �� 56      -   5         ��Name$ RemoveSchedule��56      $��Remove Selected Schedule��6-   $	��Top=�6P    J� RenderTransformOrigin$ 0.833,1.091&�6h   $��31��6�   $��363,226,130,0q�6�   $
��Bold"�6�   ���	#FF8ABD78$��1��6�   �� 57      -   5        $ AddNewSchedule��57      $��Add New Schedule��6-   $
��Left=�6H   $��
-4,227,0,0q�6c   $	��Top=�6x   $��30��6�   $	��183��6�   ���	#FF8ABD78$
��Bold"�6�   �� 58      -   5        $ ScheduleDetails��58      $��Schedule Details��6.   $
��Left=�6I   $��
76,226,0,0q�6d   $	��Top=�6x   $��31��6�   $	��184��6�   $
��Bold"�6�   ���	#FFFAB768$��1��6�   �� 59      -   5        $��1��59      $��Return��6%   $
��Left=�66   $��476,292,0,0q�6Q   $	��Top=�6f   $��65��6�   ���	#FFFFF7F76�   5:      5;      5<      