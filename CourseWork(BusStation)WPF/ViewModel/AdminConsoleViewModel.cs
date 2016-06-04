using System;
using System.ComponentModel;
using CourseWork_BusStation_WPF.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using CourseWork_BusStation_WPF.Model;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model.BusStationEntity;
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

        ObservableCollection<T> UpdateEntities<T>()
        {
            return new ObservableCollection<T>(station.GetEntities<T>());
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
                            station.RemoveEntity<Flight>(SelectedFlight);
                            Flights = UpdateEntities<Flight>();
                        }; break;
                    case "Passenger":
                        {
                            station.RemoveEntity<Passenger>(SelectedPassenger);
                            Passengers = UpdateEntities<Passenger>();
                        }; break;
                    case "Bus":
                        {
                            station.RemoveEntity<Bus>(SelectedBus);
                            Buses = UpdateEntities<Bus>();
                        }; break;
                    case "Driver":
                        {
                            station.RemoveEntity<Driver>(SelectedDriver);
                            Drivers = UpdateEntities<Driver>();
                        }; break;
                    case "Ticket":
                        {
                            station.RemoveEntity<Ticket>(SelectedTicket);
                            Tickets = UpdateEntities<Ticket>();
                        }; break;
                }
            }
        }
        public ICommand ApplyChangesCommand { get; set; }
        void ApplyChanges()
        {

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
                _flights = new ObservableCollection<Flight>(station.GetEntities<Flight>());
                _tickets = new ObservableCollection<Ticket>(station.GetEntities<Ticket>());
                _buses = new ObservableCollection<Bus>(station.GetEntities<Bus>());
                _drivers = new ObservableCollection<Driver>(station.GetEntities<Driver>());
                _passengers = new ObservableCollection<Passenger>(station.GetEntities<Passenger>());
            }
        }

        ObservableCollection<Flight> _flights = new ObservableCollection<Flight>();
        public ObservableCollection<Flight> Flights
        {
            get { return _flights; }
            set
            {
                _flights = value;
                UpdatePropertyChanged("Flights");
            }
        }
        ObservableCollection<Ticket> _tickets = new ObservableCollection<Ticket>();
        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                UpdatePropertyChanged("Tickets");
            }
        }
        ObservableCollection<Bus> _buses = new ObservableCollection<Bus>();
        public ObservableCollection<Bus> Buses
        {
            get { return _buses; }
            set
            {
                _buses = value;
                UpdatePropertyChanged("Buses");
            }
        }
        ObservableCollection<Driver> _drivers = new ObservableCollection<Driver>();
        public ObservableCollection<Driver> Drivers
        {
            get { return _drivers; }
            set
            {
                _drivers = value;
                UpdatePropertyChanged("Drivers");
            }
        }
        ObservableCollection<Passenger> _passengers = new ObservableCollection<Passenger>();
        public ObservableCollection<Passenger> Passengers
        {
            get { return _passengers; }
            set
            {
                _passengers = value;
                UpdatePropertyChanged("Passengers");
            }
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
