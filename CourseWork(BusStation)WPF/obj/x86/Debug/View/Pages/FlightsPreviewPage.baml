   M S B A M L   `   `   `  ����  CourseWork(BusStation)WPF=    7CourseWork_BusStation_WPF.View.Pages.FlightsPreviewPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  FlightsPreviewPage��5      .��5
      � .+�5   
   �� -   5        $��BackToMainMenuButton��5   I   $3��-Вернуться в главное меню��6   $��12,12,675,0q�64   $	��160��6e   $��27��6�   $	��Top=�6�   ��6t   ��  �� Path$ BackToMainMenuCommand�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��
FlightGrid��5   C      AutoGenerateColumns .  $��
12,149,0,0q�6/   $   HorizontalScrollBarVisibility$
 Auto=�6z      
IsReadOnly . $	��194��6�   $	��Top=�6�   $
��Left=�6  $	��922��62  ��6U   �� $ Flights� �� SelectedIndex 6�   �� $ SelectedIndex� �� Mode$ OneWayToSource=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$% Доступно билетов��6%   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6?   �� *Available_Tickets_Amount+  5      $' !Время отправления��6%   	 6@   �� *Departure_time+  5      $! Время прибытия��6%   	 6=   �� *Arrival_time+  5      $' !Место отправления��6%   	 6@   �� *Departure_place+  5      $! Место прибытия��6%   	 6=   �� *Arrival_Place+  5      $3 -Промежуточные остановки��6%   	 6F   �� *Intermediate_stops+  5      $% Дата отправления��6%   	 6?   �� *Departure_date+  5      $ Дата прибытия��6%   	 6<   �� *Arrival_date+5      �� 5   
   -   5        $��Departure_PlacesListBox��5   L   $��75��6   $
��Left=�6   $��	81,68,0,0q�69   $	��Top=�6k   $	��163��6�   ��6�   �� $ Departure_PlacesList�$ TwoWay=�
 �� SelectedItem
 6�   �� $ Departure_Place�$ TwoWay=��� 5   
   -   5        $��Departure_PlaceLabel��5   _   $��Рейсы из ��6   $��28��6$   $
��Left=�60   $��
12,102,0,0q�6K   $	��Top=�6{   �� 5   
   -   5        $��Arrival_PlacesListBox��5   2   $��75��6   $��
327,68,0,0q�6   $	��Top=�6O   $
��Left=�6g   $	��153��6�   ��6�   �� $ Arrival_PlacesList�$ TwoWay=�
 6�   �� $ Arrival_Place�$ TwoWay=��� 5   
   -   5        $��Arrival_PlaceLabel��5   ^   $��Рейсы в��6   $��28��6"   $
��Left=�6.   $��265,102,0,0q�6I   $	��Top=�6x   $��56��6�   �� 5   
   -   5        $��BeforeLabel��5   [   $��Дата��6   $��28��6   $
��Left=�6+   $��531,102,0,0q�6F   $	��Top=�6n   �� 5   
   -   5        $��FiltresButton��5   h   $'��!Применить фильтры��6   $��32��6-   $
��Left=�69   $��
784,68,0,0q�6T   $	��Top=�6}   $	��150��6�   ��6�   �� $ ApplyFiltresCommand��� 5   
   -	   5        $��FiltresResetButton��5   K   $!��Сброс фильтров��6   $��32��6*   $��784,111,0,0q�66   $	��Top=�6e   $
��Left=�6�   $	��150��6�   ��6}   �� $ ResetFiltresCommand�6�   (  "System.Windows.Controls.DatePicker  5   
   -
   5        $��datePicker1��5   f      Text$ Выберите дату��6   $��25��6*   $
��Left=�66   $��571,105,0,0q�6Q   $	��Top=�6y   $	��115��6�      SelectedDate 6�   �� $ Departure_date�$ OneWayToSource=��� 5    
   -   5        $��ReservationButton��5    a   $+��%Забронировать билет��6   $
��Left=�6/   $��784,357,0,137q�6J   $	��150��6z   ��6�   �� $ ReserveCommand��� 5!   
   -   5        $��BusInformationTextBox��5!   N   $	��165��6   $
��Left=�6   $��
12,349,0,0q�6:   $	��Top=�6k   $	��213��6�    �� 
IsReadOnly . ���	#00000000��6�   �� $ BusInformation�$ OneWay=��� 5"   
   -   5        $��DriverInformationTextBox��5"   y   ���	#00000000$	��165��6*   $
��Left=�67    . $��327,349,0,0q�6d   $	��Top=�6�   $	��213��6�   ��6�   �� $ DriverInformation�$ OneWay=�5#      5$      