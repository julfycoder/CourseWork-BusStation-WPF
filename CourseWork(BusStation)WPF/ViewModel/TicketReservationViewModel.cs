using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;
using CourseWork_BusStation_WPF.View.Pages;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.Model.BusStationEntity;
using System.Reflection;
using System.Windows;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class TicketReservationViewModel : INotifyPropertyChanged
    {
        DataRow flight;
        Database database;
        Page currentPage;
        public TicketReservationViewModel(Page page, DataRow flight)
        {
            this.currentPage = page;
            this.flight = flight;
            BackToFlightPreviewCommand = new Command(arg => BackToFlightPreview());
            ApplyReservationCommand = new Command(arg => ApplyReservation());

            CreateModel();
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands

        public ICommand BackToFlightPreviewCommand { get; set; }
        void BackToFlightPreview()
        {
            currentPage.NavigationService.Navigate(new FlightsPreviewPage());
        }
        public ICommand ApplyReservationCommand { get; set; }
        void ApplyReservation()
        {
            if ((PassengerSurname != null && PassengerSurname != "") && (PassengerName != null && PassengerName != "") && (PassengerPatronymic != null && PassengerPatronymic != "") && (PassengerNationality != null && PassengerNationality != ""))
            {
                ReserveTicket();
                BackToFlightPreview();
            }
            else MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        void ReserveTicket()
        {
            DataTable passengerTable = new DataTable();

            foreach (PropertyInfo property in typeof(Passenger).GetProperties()) if (property.Name != "idPassenger") passengerTable.Columns.Add(property.Name);

            DataRow passenger = passengerTable.NewRow();
            passenger["Surname"] = PassengerSurname;
            passenger["Name"] = PassengerName;
            passenger["Patronymic"] = PassengerPatronymic;
            passenger["Nationality"] = PassengerNationality;
            database.SendQuery(MySqlQueryConstructor.InsertQuery("Passenger", passenger));

            Dictionary<string, object> changes = new Dictionary<string, object>();
            changes.Add("Available_Tickets_Amount", (int)flight["Available_Tickets_Amount"] - 1);
            database.SendQuery(MySqlQueryConstructor.UpdateQuery("Flight", MySqlQueryConstructor.SetQuery(changes)) +
                MySqlQueryConstructor.WhereQuery(MySqlQueryConstructor.SimpleCondition("idFlight", "=", flight["idFlight"])));
        }

        #endregion

        #region Properties
        public string PassengerSurname
        {
            get;
            set;
        }
        public string PassengerName
        {
            get;
            set;
        }

        public string PassengerPatronymic
        {
            get;
            set;
        }
        public string PassengerNationality
        {
            get;
            set;
        }

        #endregion
    }
}
