using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Input;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.View.Pages;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class FlightsPreviewViewModel : INotifyPropertyChanged
    {
        Page currentPage;
        Database database;
        public FlightsPreviewViewModel(Page page)
        {
            this.currentPage = page;
            BackToMainMenuCommand = new Command(arg => BackToMainMenu());
            ApplyFiltresCommand = new Command(arg => ApplyFiltres());
            ResetFiltresCommand = new Command(arg => ResetFiltres());
            ReserveCommand = new Command(arg => Reserve());

            CreateModel();
            _flightsTable = database.GetData(MySqlQueryConstructor.SelectQuery("Flight"));
        }

        void CreateModel()
        {
            DatabaseBuilder builder = new MySqlDatabaseBuilder();
            builder.SetDatabaseName("mydb");
            builder.SetServerAddress("127.0.0.1");
            builder.SetPort(3306);
            builder.SetUserName("root");
            builder.SetPassword("");
            database = builder.BuildDatabase();
        }
        void UpdateSelectionObservers()
        {
            BusInformation = "Bus:\n";
            DataTable busTable = database.GetData(MySqlQueryConstructor.SelectQuery("Bus") + MySqlQueryConstructor.WhereQuery(MySqlQueryConstructor.SimpleCondition("idBus", "=", FlightsTable.Rows[SelectedIndex]["idBus"])));
            foreach (DataColumn column in busTable.Columns) BusInformation += column.ColumnName + ": " + busTable.Rows[0][column] + "\n";

            DriverInformation = "Driver:\n";
            DataTable driverTable = database.GetData(MySqlQueryConstructor.SelectQuery("Driver") + MySqlQueryConstructor.WhereQuery(MySqlQueryConstructor.SimpleCondition("idDriver", "=", busTable.Rows[0]["idDriver"])));
            foreach (DataColumn column in driverTable.Columns) DriverInformation += column.ColumnName + ": " + driverTable.Rows[0][column] + "\n";
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
                DataRow flight = FlightsTable.Rows[SelectedIndex];
                if ((int)flight["Available_Tickets_Amount"] > 0) currentPage.NavigationService.Navigate(new TicketReservationPage(flight));
                else MessageBox.Show("К сожалению на данный рейс были проданы все билеты.", "Сожаление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Не выбран ни один рейс!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public ICommand ApplyFiltresCommand { get; set; }
        void ApplyFiltres()
        {
            string query = MySqlQueryConstructor.SelectQuery("Flight");
            List<string> conditions = new List<string>();
            if (Departure_Place != null && Departure_Place != Departure_PlacesList[0]) conditions.Add(MySqlQueryConstructor.SimpleCondition("Departure_Place", "=", Departure_Place));
            if (Arrival_Place != null && Arrival_Place != Arrival_PlacesList[0]) conditions.Add(MySqlQueryConstructor.SimpleCondition("Arrival_Place", "=", Arrival_Place));
            if (Departure_date != null && (Departure_date.Year != 1)) conditions.Add(MySqlQueryConstructor.SimpleCondition(
                              "Departure_date", "=", Departure_date.Year.ToString() + "-" + Departure_date.Month.ToString() + "-" + Departure_date.Day.ToString()));
            if (conditions.Count > 0) query += MySqlQueryConstructor.WhereQuery(conditions.ToArray());
            FlightsTable = database.GetData(query);
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
                foreach (DataRow row in FlightsTable.Rows) _departure_placesList.Add(row[FlightsTable.Columns["Departure_Place"]].ToString());
                return _departure_placesList;
            }
            set
            {
                _departure_placesList = value;
            }
        }
        ObservableCollection<string> _arriva_placeslList;
        public ObservableCollection<string> Arrival_PlacesList
        {
            get
            {
                _arriva_placeslList = new ObservableCollection<string>();
                _arriva_placeslList.Add("Place");
                foreach (DataRow row in FlightsTable.Rows) _arriva_placeslList.Add(row[FlightsTable.Columns["Arrival_Place"]].ToString());
                return _arriva_placeslList;
            }
            set
            {
                _arriva_placeslList = value;
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

        #region FlightTable

        DataTable _flightsTable;
        public DataTable FlightsTable
        {
            get { return _flightsTable; }
            set
            {
                _flightsTable = value;
                UpdatePropertyChanged("FlightsTable");
            }
        }

        #endregion
    }
}
