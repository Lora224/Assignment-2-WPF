   M S B A M L   `   `   `  ����K  GAssignment 2 WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Assignment_2_WPF.ViewsAssignment_2_WPF.Views  5   	   +    %Assignment_2_WPF.Views.AddNewSchedule   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Assignment_2_WPF.Views     5   	     � Title$  AddNewSchedule��5   	   $	��600��6    $	��800��6-   .��5	      � ��5
   
   �� 5       �� ImageSource$ /Views/background-2.jpg��6   .+�5   
   �� $��Add New Schedule��6   $��24(�5      $
��Bold"�5      $��289,64,289,476q�5      �� 5   
   $
��Left=�6   $��266,148,0,0q�6/   $
��Wrap=�6D   $��Choose pet: ��6X   $	��Top=�6l   $��17��6�   $��71��6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $��266,211,0,0q�6/   $
��Wrap=�6D   $��Schedule type:��6X   $	��Top=�6n   $��22��6�   $��88��6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $��266,277,0,0q�6/   $
��Wrap=�6D   $��
Date/ Time��6X   $	��Top=�6j   $��18��6�   $��71��6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $��266,336,0,0q�6/   $
��Wrap=�6D   $��Description:��6X   $	��Top=�6l   $��19��6�   $��71��6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ Description��5      $
��Left=�5   	   $��376,335,0,0q�6$    �� TextWrapping$
 Wrap=�69   $	��Top=�5   	   $	��149��6!   $��19��6-   ��5   	   �� *Description+ �� UpdateSourceTrigger$ PropertyChanged=�5   -   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateTime��5      $
��Left=�5      $��376,273,0,0q�6(   $	��Top=�5       $��25��6%   $	��149��61      SelectedDate 5      �� *ScheduleDate+5    1   �� 5#   
   -   5         ��Name$ ShowPetList��5#      $
��Left=�5$      $��376,146,0,0q�6.   $	��Top=�5%      $	��149��6+   $��22��67   ��5&      �� *Pets+ �� SelectedItem 5'      �� *SelectedPet+��5(      �� 5)      .R�5*      �� ��6    �� *	PetName+5+      5-      �� 50   
   -   5        $ ScheduleType��50      $
��Left=�51      $��376,209,0,0q�6.   $	��Top=�52      $	��149��6+   $��22��67   ��53      �� *ScheduleTypes+	 �� SelectedValue	 54      �� *SelectedScheduleType+��55      �� 56      .R�57      �� ��6    �� 58      5:      �� 5;   
   -   5        $
��Save��5;      $��Center=�6    $��	0,399,0,0q�6=   $	��Top=�6P   $��50��6h   ���	#FFF9B866$
��Bold"�6�   6�   �� 5<   
   $��Cancel��6   $
��Left=�6"   $��475,399,0,0q�6=   $	��Top=�6R   $��50��6j   
�������6u   5B      5C      