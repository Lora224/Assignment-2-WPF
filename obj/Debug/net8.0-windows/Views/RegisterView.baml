   M S B A M L   `   `   `  ����K  GAssignment 2 WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Assignment_2_WPF.ViewsAssignment_2_WPF.Views  5   	   )    #Assignment_2_WPF.Views.RegisterView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Assignment_2_WPF.Views     5   	     � Title$  Register screen��5   	   $	��600��6!   $	��800��6.    � 	Resources 5
         RoundedTextBox �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name  RoundedPasswordBox  RoundedButton J� Effect3  -System.Windows.Media.Effects.DropShadowEffect	   ShadowDepth
   Opacity   
BlurRadius �� Triggers P� Property P� Value��%�  &	        &	 N    &	 �    �� 5   
   " ��6'   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� 
�������$ 20��5   !   $��1q�5   !   ���#A0C4FF._�5      �� $ PART_ContentHost��6+   $
��10,5q�6E   5      5      5      �� 5       � ��$	 200��6&   �� 5       � ��$ 35��6'   5      �� 5   
   " G�6+   .�5      ��  � �� 5       �� 5!      " G�6&   .R�5"      �� 
�������$ 20��5#   !   $��1q�5$   !   ���#A0C4FF._�5&      �� $ PART_ContentHost��6+   $
��10,5q�6E   5'      5(      5*      �� 5+       � ��$	 200��6&   �� 5,       � ��$ 35��6'   5-      �� 50   
   " ��6&   .�51      ��  � ��$	 120��6&   �� 52       � ��$ 35��6'   �� 53       � ��
 �����6+   �� 54       � �� 55      �� 56      " ��6&   .R�57      �� #��z��6!   $ 20��58   !   $��0q�59   !    5:        5;   "   $	 2Q�63   $	
 0.3Q�5<   1   $ 5Q�5=   1   ._�5?      �� $��Center=�6/   $��Center=�5@   -   5A       5B      P� 5C       �@J�IsMouseOver$
 True��6=   . �5D   "   ��  � �� �#357ABD6?   5E      5G      5I      5J      5K      .��5M      � ��5N   
   �� 5O       �� ImageSource$ /Views/background.jpg��6   .+�5R   
   �� ���	#80FFFFFF$ 30��5S      $	��400��5T      $	��450��5U      $��Center=�5V      ._�5W      �� $��Center=�6   $��Center=�5X      .+�5Z      �� $��CHA CHA PET APPLICATION��6   $��24(�5[      $
��Bold"�5\      $��0,0,0,30q�5]      ���#2C3E50 �� TextAlignment$ Center=�5_      �� 5a      $��Register for ChaChaApp��6   $��18(�5b      $��SemiBold"�5c      $��0,0,0,30q�5d      ���#2C3E50$��Center=�5e   /   �� 5h       �� Orientation$ 
Horizontal=�6   $��0,0,0,15q�5i      .+�5j      �� $��
User Name:��6    $��SemiBold"�5k      $��80��5l      $��Center=�5m      $��0,0,10,0q�5n      ���#2C3E505o      �� 5p      -   5         ��Name$ 	NameInput��5p      #��[  5q      5r      �� 5u      $ 
Horizontal=�6   $��0,0,0,15q�5v      .+�5w      �� $��Email:��6    $��SemiBold"�5x      $��80��5y      $��Center=�5z      $��0,0,10,0q�5{      ���#2C3E505|      �� 5}      -   5        $ 
EmailInput��5}      #��[  5~      5      �� 5�      $ 
Horizontal=�6   $��0,0,0,30q�5�      .+�5�      �� $��	Password:��6    $��SemiBold"�5�      $��80��5�      $��Center=�5�      $��0,0,10,0q�5�      ���#2C3E505�      G� 5�      -   5         G�Name$ PasswordBox��5�   "   #��[ 5�       5�      �� 5�      $ 
Horizontal=�6   $��Center=�5�      .+�5�      �� -   5        $��Submit��5�      #��[ 5�      ���#4A90E2$��0,0,20,0q�5�      �� 5�      -   5        $��Cancel��5�      #��[ 5�      ���#7FB3D55�      5�      5�      5�      5�      5�      