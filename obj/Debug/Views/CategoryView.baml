   M S B A M L   `   `   `  ����H  DWPF-LoginForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_LoginForm.ViewsWPF_LoginForm.Views  5   	   C&clr-namespace:WPF_LoginForm.ViewModelsWPF_LoginForm.ViewModels  5   	   &     WPF_LoginForm.Views.CategoryView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WPF_LoginForm.Views     5   	   <	viewModel&clr-namespace:WPF_LoginForm.ViewModels     5   	     � Title$  CategoryView��5	   	   $	��450��6   $	��800��6+    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � DataContext 5      0   *WPF_LoginForm.ViewModels.CategoryViewModel  5   
   .��5      ��  �� CornerRadius$ 12��6   ��5   
   �� 5       �� ImageSource$ /Images/back-image.jpg��6    j� Stretch$
 None=�6>   ._�5   
   �� $ 10��6   $��2q�5      	 J� Opacity$
	 0.95Q�5      ��5      �� 5      
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5       �  � Color$ #462AD8��6#    � Offset$ 0Q�63   � 5!      $ #DA34AE��6#   $
 0.75Q�63   � 5"      $ #8A16C1��6#   $ 1Q�63   5#      ��5&      �� 5'      $	
 0,1&�6&   $	 1,0&�67   .��5(      � $ #060531��6#   $ 0Q�63   � 5)      $ #1B1448��6#   $ 1Q�63   5*      ._�5-      � .+�5/      �� $��30(�50      
�������$��Center=�52      $	��Top=�53      $��0,42,0,0q�64   $��Candara)�6F   $
��Bold"�6[   $��Categories View��6m   &   System.Windows.Controls.DataGrid  55         AutoGenerateColumns .  $��120,100,139,130q�6�   ���	#FFC0C0E2���	#FF8A2BE2$��2q�58         RowBackground �	#FFE6E6FA   AlternatingRowBackground �	#FFD8BFD8 �� AlternationCount$ 2��5;         RowHeaderWidth$ 0Q�5<         HorizontalGridLinesBrush �	#FF8A2BE2   VerticalGridLinesBrush �	#FF8A2BE2   CanUserAddRows .  $��Candara)�5?      ��55      �� *
Categories+ �� SelectedItem 6>   �� *SelectedCategory+   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *ID+6b     5B      $
 Name��6-   $ * 6y    6;   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�6y     5C      $ Price��6-   $ * 6g    6<   �� *Price+ �� StringFormat$ N2��6g   4  .System.Windows.Controls.DataGridTemplateColumn  5D      $ Actions��61   $ * 6B      CellTemplate 5E      �� 5F   "   .R�5G   &   ��  �� Orientation$ 
Horizontal=�61   .+�5H   *   �� $
��Edit��61   ��6@   �� *DataContext.EditCommand+  �� RelativeSource  �� ! �� AncestorType$! DataGridL���6�   �� 5I   '   5J   #   5L      5N      �� 5Q      -   5        " ��Name$" btnCategories��5Q      $��0q�5S   
   $��Add new category��5T   
   
�������$��12(�5V   
   $��
Montserrat)�5W   
   # � Cursor$
# Hand��5X   
   $��10,352,630,10q�5Y   
   $
��Bold"�6!   ��5R   
   �� *OpenAddCategoryDialogCommand+��5[      �� 5\      $ �� 
TargetType"$ ��6    .�5]      �� % �� Property% � ��& �� Value& �#462AD86;   ' �� Triggers' 5^      P� 5_   "   ( P� Property( �@J�IsMouseOver) P� Value) . . �5`   &   �� % � ��& �#28AEED6C   5a   #   5c      ��5f      �� 5g      * �� 
TargetType"* ��6*   .R�5h      �� $	��150��6%   $��40��61   $ 20��5i      #��z��5j      ._�5k   "   �� $��Center=�63   $��Center=�5l   $   5m      5n      5p      �� 5r      -   5        $" btnUndo��5r      $��0q�5s      $��-��5t      
�������$��16(�5v      $��
Montserrat)�5w      $
# Hand��5x      $��19,10,744,401q�5y      ��5{      �� 5|      "$ ��6    .�5}      �� % � ��& �#28AEED6;   ' 5~      P� 5   "   ( �@J�IsMouseOver) . . �5�   &   �� % � ��& �#278BEF6C   5�   #   5�      ��5�      �� 5�      "* ��6*   .R�5�      �� $��18��6%   $��18��60   $ 9��5�      #��z��5�      ._�5�   "   �� $��Center=�63   $��Center=�5�      5�      5�      5�      �� 5�      $��16(�5�      
�������$
��Left=�5�      $	��Top=�5�      $��210,331,0,0q�6-   $��Candara)�6B   $
��Bold"�6W   $%��Total Budget for all products: ��6i   $��35��6�   $	��232��6�   �� 5�      $
��Left=�6   $��421,331,0,0q�67   $
��Wrap=�6L   $	��Top=�6`   
�������$��16(�6�   $
��Bold"�6�   $��26��6�   $	��125��6�   ��6�   �� *TotalBudget+$ N2��5�      5�      5�      5�      