   M S B A M L   `   `   `  ����H  DWPF-LoginForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_LoginForm.ViewsWPF_LoginForm.Views  5   	   C&clr-namespace:WPF_LoginForm.ViewModelsWPF_LoginForm.ViewModels  5   	   '    !WPF_LoginForm.Views.ProducersView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WPF_LoginForm.Views     5   	   <	viewModel&clr-namespace:WPF_LoginForm.ViewModels     5   	     � Title$  ProducersView��5	   	   $	��450��6   $	��800��6,    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � DataContext 5      1   +WPF_LoginForm.ViewModels.ProducersViewModel  5   
   .��5      ��  �� CornerRadius$ 12��6   ��5   
   �� 5       �� ImageSource$ /Images/back-image.jpg��6    j� Stretch$
 None=�6>   ._�5   
   �� $ 10��6   $��2q�5      	 J� Opacity$
	 0.95Q�5      ��5      �� 5       
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5!      �  � Color$ #462AD8��6#    � Offset$ 0Q�63   � 5"      $ #DA34AE��6#   $
 0.75Q�63   � 5#      $ #8A16C1��6#   $ 1Q�63   5$      ��5'      �� 5(      $	
 0,1&�6&   $	 1,0&�67   .��5)      � $ #060531��6#   $ 0Q�63   � 5*      $ #1B1448��6#   $ 1Q�63   5+      ._�5.      � .+�50      �� $��30(�51      
�������$��Center=�53      $	��Top=�54      $��0,28,0,0q�64   $��Candara)�6F   $
��Bold"�6[   $��Producers View��6m   �� 55      -   5         ��Name$ btnUndo��55      $��0q�56      $��-��57      
�������$��16(�59      $��
Montserrat)�5:       � Cursor$
 Hand��5;      $��19,10,744,401q�5<      ��5>      �� 5?       �� 
TargetType" ��6    .�5@      ��  �� Property � �� �� Value �#28AEED6;    �� Triggers 5A      P� 5B   "    P� Property �@J�IsMouseOver P� Value . . �5C   &   ��  � �� �#278BEF6C   5D   #   5F      ��5I      �� 5J       �� 
TargetType" ��6*   .R�5K      �� $��18��6%   $��18��60   $ 9��5L      #��z��5M      ._�5N   "   �� $��Center=�63   $��Center=�5O      5P      5Q      5T      &   System.Windows.Controls.DataGrid  5V         AutoGenerateColumns .  $��110,84,122,112q�5W      ���	#FFC0C0E2���	#FF8A2BE2$��2q�5Z         RowBackground �	#FFE6E6FA   AlternatingRowBackground �	#FFD8BFD8 �� AlternationCount$ 2��5]         RowHeaderWidth$ 0Q�5^         HorizontalGridLinesBrush �	#FF8A2BE2   VerticalGridLinesBrush �	#FF8A2BE2   CanUserAddRows .  $��Candara)�5a      ��5V      �� *	Producers+ �� SelectedItem 6=   �� *SelectedProducer+    Columns  5c      0  *System.Windows.Controls.DataGridTextColumn  5d      ,  &System.Windows.Controls.DataGridColumn!   Header$! ID��6-   "   
IsReadOnly" . #   Width5  /System.Windows.Controls.DataGridLengthConverter$# * 6b   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 69   �� *ID+6b     5e      $
! Name��6-   $# * 6z   $ 6;   �� *Name+% �� UpdateSourceTrigger$% PropertyChanged=�6z     5f      $! Country Origin��6-   $# * 6�   $ 6E   �� *CountryOrigin+$% PropertyChanged=�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5g      $! Actions��61   $# * 6B   &   CellTemplate& 5h      �� 5i   "   .R�5j   &   �� ' �� Orientation$' 
Horizontal=�61   .+�5k   *   �� $
��Edit��61   ��6@   �� *DataContext.EditCommand+( �� RelativeSource( �� ) �� AncestorType$) DataGridL���6�   �� �� 5l   *   $��Delete��61   $��5,0,0,0q�6�   ��6B   �� *DataContext.DeleteCommand+( �� $) DataGridL���6�   �� 6�   5m   '   5n   #   5p      5r      �� 5u      -   5        $ btnAddProducer��5u      $��0q�5w   
   $��Add new producer��5x   
   
�������$��12(�5z   
   $��
Montserrat)�5{   
   $
 Hand��5|   
   $��10,358,573,0q�5}   
   $
��Bold"�6    ��5v   
   �� *OpenAddProducerDialogCommand+��5      �� 5�      " ��6    .�5�      ��  � �� �#462AD86;    5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   ��  � �� �#28AEED6C   5�   #   5�      ��5�      �� 5�      " ��6*   .R�5�      �� $	��150��6%   $��40��61   $ 20��5�      #��z��5�      ._�5�   "   �� $��Center=�63   $��Center=�5�   $   5�      5�      5�      �� 5�      $��16(�5�      
�������$
��Left=�5�      $	��Top=�5�      $��553,358,0,0q�6-   $��Candara)�6B   $
��Bold"�6W   $#��Go to search and add products��6i   * J� RenderTransformOrigin$* 0.839,3.618&�6�   $��44��6�   $	��329��6�   �� 5�      -   5        $ btnGoToProducts��5�      $��30��62   $��30��6=   $��644,387,122,29q�6I   $* 
-0.1,0.683&�6a   ��5�      �� 5�      " ��6*   .R�5�      � .+�5�   "   F� "����        �    ��� р�� �pr 	
�������+ �� Stretch$
+ Fill=�6P   5�      5�      5�      5�      5�      5�      5�      