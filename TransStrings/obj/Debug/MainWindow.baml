   M S B A M L   `   `   `  ����  TransStrings/    )RodSoft.TranslationJosmStrings.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MainWindow��5   	   $��364.925��6   $��1086.194��6-   .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5	      �� 5
      $��20 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��2��6.   .+�5      �� -   5        
buttonLoad��$��20��5   (   $��20��64    � ToolTip$
 Load��6T   �� 5      -   5        
buttonSave��$��20��5   (   $��20��64   $
 Save��6V   �� 5      -   5        buttonCreate��$��20��5   *   $��20��66   $ Compile��6X   �� 5      -   5         ��Name$ comboBoxLanguage��5      $	��132��61   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgMain��5   \   
dgMain�� �� AlternationCount$ 2��6"      AlternatingRowBackground
 �����$��0��6�   $��1��6�      AutoGenerateColumns .  $��2��6�   $��	0,0,0,0.2q�6�   ��6j   �� *TranslatedStrings+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 English string��6@   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6%   �� *EnText+6@   4  .System.Windows.Controls.DataGridTemplateColumn  5         Width5  /System.Windows.Controls.DataGridLengthConverter$ 10 6)     5      $	 Translated string��6@   
 6%   �� *RuText+6@     5      $ 10 6)      CellTemplate 5      �� 5      .R�5      �� $��	btnDelete��6%   -   5        $ Delete��5   N   5      5      5      5       5!      