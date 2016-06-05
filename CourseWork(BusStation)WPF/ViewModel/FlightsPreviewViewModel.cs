using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Reflection;
using CourseWork_BusStation_WPF.Model.BusStationEntity;
using CourseWork_BusStation_WPF.Model;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.View.Pages;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class FlightsPreviewViewModel : INotifyPropertyChanged
    {
        Page currentPage;
        IAccessible station;
        public FlightsPreviewViewModel(Page page)
        {
            this.currentPage = page;
            BackToMainMenuCommand = new Command(arg => BackToMainMenu());
            ApplyFiltresCommand = new Command(arg => ApplyFiltres());
            ResetFiltresCommand = new Command(arg => ResetFiltres());
            ReserveCommand = new Command(arg => Reserve());

            station = new BusStationAccess();
            _flights = new ObservableCollection<Flight>(station.GetEntities<Flight>());
        }


        void UpdateSelectionObservers()
        {
            BusInformation = "Bus:\n";
            List<Bus> buses = station.GetEntitiesByPrototype<Bus>(new Bus() { idBus = Flights[SelectedIndex].idBus });
            foreach (Bus bus in buses)
            {
                foreach (PropertyInfo property in bus.GetType().GetProperties())
                {
                    BusInformation += property.Name + ": " + property.GetValue(bus, null) + "\n";
                }
            }

            DriverInformation = "Driver:\n";
            List<Driver> drivers = station.GetEntitiesByPrototype<Driver>(new Driver()
            {
                idDriver = buses.ToList<Bus>().Find(bus => bus.idBus == Flights[SelectedIndex].idBus).idDriver
            });
            foreach (Driver driver in drivers)
            {
                foreach (PropertyInfo property in driver.GetType().GetProperties())
                {
                    DriverInformation += property.Name + ": " + property.GetValue(driver, null) + "\n";
                }
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
        public ICommand ReserveCommand { get; set; }
        void Reserve()
        {
            if (SelectedIndex != -1)
            {
                Flight flight = Flights[SelectedIndex];
                if (flight.Available_Tickets_Amount > 0) currentPage.NavigationService.Navigate(new TicketReservationPage(flight));
                else MessageBox.Show("К сожалению на данный рейс были проданы все билеты.", "Сожаление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Не выбран ни один рейс!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public ICommand ApplyFiltresCommand { get; set; }
        void ApplyFiltres()
        {
            Flight flight = new Flight();
            List<string> conditions = new List<string>();
            int count = 0;
            if (Departure_Place != null && Departure_Place != Departure_PlacesList[0])
            {
                flight.Departure_place = Departure_Place;
                count++;
            }
            if (Arrival_Place != null && Arrival_Place != Arrival_PlacesList[0])
            {
                flight.Arrival_Place = Arrival_Place;
                count++;
            }
            if (Departure_date != null && (Departure_date.Year != 1))
            {
                flight.Departure_date = Departure_date;
                count++;
            }
            if (count > 0) Flights = new ObservableCollection<Flight>(station.GetEntitiesByPrototype<Flight>(flight));
            else Flights = new ObservableCollection<Flight>(station.GetEntities<Flight>());
        }
        public ICommand ResetFiltresCommand { get; set; }
        void ResetFiltres()
        {
            Departure_Place = "Place";
            Arrival_Place = "Place";
            Departure_date = new DateTime();
            BusInformation = "";
            DriverInformation = "";
            ApplyFiltres();
        }

        #endregion

        #region Lists

        ObservableCollection<string> _departure_placesList;
        public ObservableCollection<string> Departure_PlacesList
        {
            get
            {
                _departure_placesList = new ObservableCollection<string>();
                _departure_placesList.Add("Place");
                foreach (Flight flight in Flights) _departure_placesList.Add(flight.Departure_place);
                return _departure_placesList;
            }
            set
            {
                _departure_placesList = value;
            }
        }
        ObservableCollection<string> _arrival_placeslList;
        public ObservableCollection<string> Arrival_PlacesList
        {
            get
            {
                _arrival_placeslList = new ObservableCollection<string>();
                _arrival_placeslList.Add("Place");
                foreach (Flight flight in Flights) _arrival_placeslList.Add(flight.Arrival_Place);
                return _arrival_placeslList;
            }
            set
            {
                _arrival_placeslList = value;
            }
        }

        #endregion

        #region Properties

        string _arrival_place;
        public string Arrival_Place
        {
            get
            {
                return _arrival_place;
            }
            set
            {
                _arrival_place = value;
                UpdatePropertyChanged("Arrival_Place");
            }
        }
        string _departure_place;
        public string Departure_Place
        {
            get
            {
                return _departure_place;
            }
            set
            {
                _departure_place = value;
                UpdatePropertyChanged("Departure_Place");
            }
        }

        public DateTime Departure_date
        {
            get;
            set;
        }

        int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                if (_selectedIndex > -1) UpdateSelectionObservers();
            }
        }

        string _busInformation;
        public string BusInformation
        {
            get
            {
                return _busInformation;
            }
            set
            {
                _busInformation = value;
                UpdatePropertyChanged("BusInformation");
            }
        }
        string _driverInfromation;
        public string DriverInformation
        {
            get
            {
                return _driverInfromation;
            }
            set
            {
                _driverInfromation = value;
                UpdatePropertyChanged("DriverInformation");
            }
        }

        #endregion

        #region Flights

        ObservableCollection<Flight> _flights = new ObservableCollection<Flight>();
        public ObservableCollection<Flight> Flights
        {
            get
            { 
                return _flights; 
            }
            set
            {
                _flights = value;
                UpdatePropertyChanged("Flights");
            }
        }

        #endregion
    }
}
