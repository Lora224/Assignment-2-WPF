   M S B A M L   `   `   `  ����   Assignment 2 WPF$    Assignment_2_WPF.Views.PetView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Pet View��$	��400��$	��600��.��� .+��� $��20q�.+��� $��Manage Your Pets��$��24(�$
��Bold"�$��Center=�$��0,0,0,20q��� $��0,0,0,20q����� *Pets+ �� SelectedItem �� *SelectedPet+��  �� Orientation$ 
Horizontal=�$��Center=�$��0,0,0,20q�.+��� -   $��Add New Pet��$	��100���� -   $��Edit Pet��$	��100��$
��10,0q��� -   $��
Remove Pet��$	��100���� -   $��Save Changes��$	��100���� $��Pet Details��$��18(�$��SemiBold"�$
��Left=�$��0,0,0,10q�� $��0,0,0,10q�.+��� $��0q����� *SelectedPet.PetName+�� $��Pet Name��
�������$��Center=�$��5,0,0,0q� J� IsHitTestVisible$ False��y��� *SelectedPet.PetName+ $   EmptyStringToVisibilityConverter �� 	Converter# [  � $��0,0,0,10q�.+��� $��0q����� *SelectedPet.Breed+�� $��Breed��
�������$��Center=�$��5,0,0,0q�$ False��y��� *SelectedPet.Breed+# [  � $��0,0,0,10q�.+��� $��0q����� *SelectedPet.Weight+�� $��Weight��
�������$��Center=�$��5,0,0,0q�$ False��y��� *SelectedPet.Weight+# [  (  "System.Windows.Controls.DatePicker  $��0,0,0,10q�   SelectedDate �� *SelectedPet.dob+�� $��Activities:��$��18(�$��SemiBold"�$
��Left=�$��0,20,0,5q��� $��0,0,0,20q����� *SelectedPet.activities+�� $��
Reminders:��$��18(�$��SemiBold"�$
��Left=�$��0,20,0,5q��� ���� *SelectedPet.schedules+