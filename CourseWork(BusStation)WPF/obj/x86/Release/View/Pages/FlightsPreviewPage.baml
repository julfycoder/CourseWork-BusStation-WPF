   M S B A M L   `   `   `  ����   CourseWork(BusStation)WPF=    7CourseWork_BusStation_WPF.View.Pages.FlightsPreviewPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  FlightsPreviewPage��.��� .+��� -   $��BackToMainMenuButton��$3��-Вернуться в главное меню��$��12,12,675,0q�$	��160��$��27��$	��Top=�����  �� Path$ BackToMainMenuCommand�&   System.Windows.Controls.DataGrid  -   $��
FlightGrid��   AutoGenerateColumns .  $��
12,149,0,0q�$   HorizontalScrollBarVisibility$
 Auto=�   
IsReadOnly . $	��194��$	��Top=�$
��Left=�$	��922������ $ Flights� �� SelectedIndex �� $ SelectedIndex� �� Mode$ OneWayToSource=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$% Доступно билетов��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *Available_Tickets_Amount+  $' !Время отправления��	 �� *Departure_time+  $! Время прибытия��	 �� *Arrival_time+  $' !Место отправления��	 �� *Departure_place+  $! Место прибытия��	 �� *Arrival_Place+  $3 -Промежуточные остановки��	 �� *Intermediate_stops+  $% Дата отправления��	 �� *Departure_date+  $ Дата прибытия��	 �� *Arrival_date+�� -   $��Departure_PlacesListBox��$��75��$
��Left=�$��	81,68,0,0q�$	��Top=�$	��163������ $ Departure_PlacesList�$ TwoWay=�
 �� SelectedItem
 �� $ Departure_Place�$ TwoWay=��� -   $��Departure_PlaceLabel��$��Рейсы из ��$��28��$
��Left=�$��
12,102,0,0q�$	��Top=��� -   $��Arrival_PlacesListBox��$��75��$��
327,68,0,0q�$	��Top=�$
��Left=�$	��153������ $ Arrival_PlacesList�$ TwoWay=�
 �� $ Arrival_Place�$ TwoWay=��� -   $��Arrival_PlaceLabel��$��Рейсы в��$��28��$
��Left=�$��265,102,0,0q�$	��Top=�$��56���� -   $��BeforeLabel��$��Дата��$��28��$
��Left=�$��531,102,0,0q�$	��Top=��� -   $��FiltresButton��$'��!Применить фильтры��$��32��$
��Left=�$��
784,68,0,0q�$	��Top=�$	��150������ $ ApplyFiltresCommand��� -	   $��FiltresResetButton��$!��Сброс фильтров��$��32��$��784,111,0,0q�$	��Top=�$
��Left=�$	��150������ $ ResetFiltresCommand�(  "System.Windows.Controls.DatePicker  -
   $��datePicker1��   Text$ Выберите дату��$��25��$
��Left=�$��571,105,0,0q�$	��Top=�$	��115��   SelectedDate �� $ Departure_date�$ OneWayToSource=��� -   $��ReservationButton��$+��%Забронировать билет��$
��Left=�$��784,357,0,137q�$	��150������ $ ReserveCommand��� -   $��BusInformationTextBox��$	��165��$
��Left=�$��
12,349,0,0q�$	��Top=�$	��213�� �� 
IsReadOnly . ���	#00000000���� $ BusInformation�$ OneWay=��� -   $��DriverInformationTextBox�����	#00000000$	��165��$
��Left=� . $��327,349,0,0q�$	��Top=�$	��213������ $ DriverInformation�$ OneWay=�