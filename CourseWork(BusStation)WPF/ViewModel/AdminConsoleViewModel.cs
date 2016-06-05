using System;
using System.ComponentModel;
using CourseWork_BusStation_WPF.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using CourseWork_BusStation_WPF.Model;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model.BusStationEntity;
using System.Reflection;
using System.Linq;
using System.Windows;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class AdminConsoleViewModel : INotifyPropertyChanged
    {
        IAccessible station;
        Page currentPage;
        public AdminConsoleViewModel(Page page)
        {
            this.currentPage = page;
            BackToMainMenuCommand = new Command(arg => BackToMainMenu());
            DeleteRowCommand = new Command(arg => DeleteRow());
            ApplyChangesCommand = new Command(arg => ApplyChanges());
            station = new BusStationAccess();
            _flights = new ObservableCollection<Flight>(station.GetEntities<Flight>());
            _tickets = new ObservableCollection<Ticket>(station.GetEntities<Ticket>());
            _buses = new ObservableCollection<Bus>(station.GetEntities<Bus>());
            _drivers = new ObservableCollection<Driver>(station.GetEntities<Driver>());
            _passengers = new ObservableCollection<Passenger>(station.GetEntities<Passenger>());
        }
        void UpdateCollection<T>(ObservableCollection<T> collection)
        {
            ObservableCollection<T> newCollection = new ObservableCollection<T>(station.GetEntities<T>());
            collection.Clear();
            foreach (T element in newCollection)
            {
                collection.Add(element);
            }
        }
        void ChangesAdding<T>(ObservableCollection<T> collection)
        {
            ObservableCollection<T> oldEntities = new ObservableCollection<T>(station.GetEntities<T>());
            if (collection.Count > oldEntities.Count)
            {
                for (int i = 0; i < collection.Count - oldEntities.Count; i++)
                {
                    station.AddEntity<T>(collection[oldEntities.Count + i]);
                }
                return;
            }
            foreach (T entity in collection)
            {
                T temp = Activator.CreateInstance<T>();
                temp.GetType().GetProperties()[0].SetValue(temp, -1, null);
                T oldEntity = oldEntities.ToList<T>().Find(e => e.GetType().GetProperties()[0].GetValue(e, null).Equals(entity.GetType().GetProperties()[0].GetValue(entity, null)));
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    if (property.Name != entity.GetType().GetProperties()[0].Name)
                    {
                        if (!oldEntity.GetType().GetProperty(property.Name).GetValue(oldEntity, null).Equals(property.GetValue(entity, null)))
                        {
                            if ((int)temp.GetType().GetProperties()[0].GetValue(temp, null) == -1) temp = Activator.CreateInstance<T>();
                            temp.GetType().GetProperty(property.Name).SetValue(temp, property.GetValue(entity, null), null);
                        }
                    }
                }
                if ((int)temp.GetType().GetProperties()[0].GetValue(temp, null) != -1) station.ChangeEntity<T>(oldEntity, temp);
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand BackToMainMenuCommand { get; set; }
        void BackToMainMenu()
        {
            currentPage.NavigationService.Navigate(new MainPage());
        }
        public ICommand DeleteRowCommand { get; set; }
        void DeleteRow()
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить эту запись?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                switch (SelectedTab.Name)
                {
                    case "Flight":
                        {
                            if (SelectedFlight != null)
                            {
                                station.RemoveEntity<Flight>(SelectedFlight);
                                UpdateCollection<Flight>(_flights);
                                UpdatePropertyChanged("Flights");
                            }
                            else
                            {
                                MessageBox.Show("Не выбрана ни одна запись!", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }; break;
                    case "Passenger":
                        {
                            if (SelectedPassenger != null)
                            {
                                station.RemoveEntity<Passenger>(SelectedPassenger);
                                UpdateCollection<Passenger>(_passengers);
                                UpdatePropertyChanged("Passengers");
                            }
                            else
                            {
                                MessageBox.Show("Не выбрана ни одна запись!", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }; break;
                    case "Bus":
                        {
                            if (SelectedBus != null)
                            {
                                station.RemoveEntity<Bus>(SelectedBus);
                                UpdateCollection<Bus>(_buses);
                                UpdatePropertyChanged("Buses");
                            }
                            else
                            {
                                MessageBox.Show("Не выбрана ни одна запись!", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }; break;
                    case "Driver":
                        {
                            if (SelectedDriver != null)
                            {
                                station.RemoveEntity<Driver>(SelectedDriver);
                                UpdateCollection<Driver>(_drivers);
                                UpdatePropertyChanged("Drivers");
                            }
                            else
                            {
                                MessageBox.Show("Не выбрана ни одна запись!", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }; break;
                    case "Ticket":
                        {
                            if (SelectedTicket != null)
                            {
                                station.RemoveEntity<Ticket>(SelectedTicket);
                                UpdateCollection<Driver>(_drivers);
                                UpdatePropertyChanged("Drivers");
                            }
                            else
                            {
                                MessageBox.Show("Не выбрана ни одна запись!", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }; break;
                }
            }
        }
        public ICommand ApplyChangesCommand { get; set; }
        void ApplyChanges()
        {
            switch (SelectedTab.Name)
            {
                case "Flight":
                    {
                        ChangesAdding<Flight>(_flights);
                    }; break;
                case "Passenger":
                    {
                        ChangesAdding<Passenger>(_passengers);
                    }; break;
                case "Bus":
                    {
                        ChangesAdding<Bus>(_buses);
                    }; break;
                case "Driver":
                    {
                        ChangesAdding<Driver>(_drivers);
                    }; break;
                case "Ticket":
                    {
                        ChangesAdding<Ticket>(_tickets);
                    }; break;
            }
        }
        #endregion

        #region Properties

        TabItem _selectedTab;
        public TabItem SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
            }
        }

        ObservableCollection<Flight> _flights;
        public ObservableCollection<Flight> Flights
        {
            get { return _flights; }
        }
        ObservableCollection<Ticket> _tickets = new ObservableCollection<Ticket>();
        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
        }
        ObservableCollection<Bus> _buses = new ObservableCollection<Bus>();
        public ObservableCollection<Bus> Buses
        {
            get { return _buses; }
        }
        ObservableCollection<Driver> _drivers = new ObservableCollection<Driver>();
        public ObservableCollection<Driver> Drivers
        {
            get { return _drivers; }
        }
        ObservableCollection<Passenger> _passengers = new ObservableCollection<Passenger>();
        public ObservableCollection<Passenger> Passengers
        {
            get { return _passengers; }
        }

        Flight _selectedFlight;
        public Flight SelectedFlight
        {
            get
            {
                return _selectedFlight;
            }
            set
            {
                _selectedFlight = value;
            }
        }
        Ticket _selectedTicket;
        public Ticket SelectedTicket
        {
            get
            {
                return _selectedTicket;
            }
            set
            {
                _selectedTicket = value;
            }
        }
        Driver _selectedDriver;
        public Driver SelectedDriver
        {
            get
            {
                return _selectedDriver;
            }
            set
            {
                _selectedDriver = value;
            }
        }
        Bus _selectedBus;
        public Bus SelectedBus
        {
            get
            {
                return _selectedBus;
            }
            set
            {
                _selectedBus = value;
            }
        }
        Passenger _selectedPassenger;
        public Passenger SelectedPassenger
        {
            get
            {
                return _selectedPassenger;
            }
            set
            {
                _selectedPassenger = value;
            }
        }

        #endregion
    }
}
