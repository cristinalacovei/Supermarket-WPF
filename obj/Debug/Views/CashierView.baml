   M S B A M L   `   `   `  ����H  DWPF-LoginForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_LoginForm.ViewsWPF_LoginForm.Views  5   	   C&clr-namespace:WPF_LoginForm.ViewModelsWPF_LoginForm.ViewModels  5   	   %    WPF_LoginForm.Views.CashierView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WPF_LoginForm.Views     5   	   <	viewModel&clr-namespace:WPF_LoginForm.ViewModels     5   	     � Title$  CashierView��5	   	   $	��450��6   $	��800��6*    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � DataContext 5      /   )WPF_LoginForm.ViewModels.CashierViewModel  5   
   .��5      ��  �� CornerRadius$ 12��6   ��5   
   �� 5       �� ImageSource$ /Images/back-image.jpg��6    j� Stretch$
 None=�6>   ._�5   
   �� $ 10��6   $��2q�5      	 J� Opacity$
	 0.95Q�5      ��5      �� 5      
 �� 
StartPoint$	
 0,0&�6&    �� EndPoint$	 1,1&�67   .��5       �  � Color$ #462AD8��6#    � Offset$ 0Q�63   � 5!      $ #DA34AE��6#   $
 0.75Q�63   � 5"      $ #8A16C1��6#   $ 1Q�63   5#      ��5&      �� 5'      $	
 0,1&�6&   $	 1,0&�67   .��5(      � $ #060531��6#   $ 0Q�63   � 5)      $ #1B1448��6#   $ 1Q�63   5*      ._�5-      � .+�5.      �� $��30(�6   
�������$��Center=�6=   $	��Top=�6Z   $��0,42,0,0q�6r   $��Candara)�6�   $
��Bold"�6�   $��Cashier Dashboard��6�   �� 50      -   5         ��Name$ btnUndo��50      $��0q�6*   $��-��6>   
�������$��16(�6]   $��
Montserrat)�6k    � Cursor$
 Hand��6�   $��19,10,744,401q�6�   ��51      �� 52       �� 
TargetType" ��6    .�53      ��  �� Property � �� �� Value �#28AEED6;    �� Triggers 54      P� 55   "    P� Property �@J�IsMouseOver P� Value . . �56   &   ��  � �� �#278BEF6C   57   #   59      ��5;      �� 5<       �� 
TargetType" ��6*   .R�5=      �� $��18��6%   $��18��60   $ 9��6<   #��z��6M   ._�5>   "   �� $��Center=�63   $��Center=�6N   5?      5@      5B      �� 5D      $��22(�6   
�������$
��Left=�6=   $	��Top=�6X   $��
36,180,0,0q�6p   $��Candara)�6�   $
��Bold"�6�   $3��-Enter the barcode or the name of the product:��6�   �� 5E      $
��Left=�6   $��
36,223,0,0q�65    �� TextWrapping$
 Wrap=�6I   $	��Top=�6�   $	��300��6�   $��29��6�   $��Candara)�6�   $��18(�6�   ��6]   �� *ProductSearchText+6�   �� 5G      -   5        $ btnAddProduct��5G      $��0q�6V   $��ADD PRODUCT��6j   
�������$��12(�6�   $��
Montserrat)�6�   $
 Hand��6�   $��19,87,585,271q�6�   ��60   �� *AddProductCommand+��5H      �� 5I      " ��6    .�5J      ��  � �� �#462AD86;    5K      P� 5L   "    �@J�IsMouseOver . . �5M   &   ��  � �� �#28AEED6C   5N   #   5P      ��5R      �� 5S      " ��6*   .R�5T      �� $	��150��6%   $��40��61   $ 20��6=   #��z��6O   ._�5U   "   �� $��Center=�63   $��Center=�6N   5V      5W      5Y      �� 5[      -   5        $ btnFinalizeReceipt��5[      $��0q�6`   $��FINALIZE RECEIPT��6t   
�������$��12(�6�   $��
Montserrat)�6�   $
 Hand��6�   $��278,348,326,10q�6�   ��65   �� *FinalizeReceiptCommand+��5\      �� 5]      " ��6    .�5^      ��  � �� �#462AD86;    5_      P� 5`   "    �@J�IsMouseOver . . �5a   &   ��  � �� �#28AEED6C   5b   #   5d      ��5f      �� 5g      " ��6*   .R�5h      �� $	��150��6%   $��40��61   $ 20��6=   #��z��6O   ._�5i   "   �� $��Center=�63   $��Center=�6N   5j      5k      5m      �� 5o      $��22(�6   
�������$
��Left=�6=   $	��Top=�6X   $��
621,47,0,0q�6p   $��Candara)�6�   $
��Bold"�6�   $��Receipt:��6�    J� RenderTransformOrigin$ 7.977,-2.327&�6�   �� 5p       �� 
IsReadOnly . $
��Left=�6I   $��572,116,0,0q�6d   $
 Wrap=�6y   $	��Top=�6�   $	��175��6�   $	��302��6�   $��Candara)�6�   $��18(�6�   ��6   �� *ReceiptText+6�   5q      5r      5s      5t      