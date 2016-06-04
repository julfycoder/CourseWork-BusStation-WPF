using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;
using CourseWork_BusStation_WPF.View.Pages;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.Model.BusStationEntity;
using CourseWork_BusStation_WPF.Model;
using System.Reflection;
using System.Windows;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class TicketReservationViewModel : INotifyPropertyChanged
    {
        Flight flight;
        IAccessible station;
        Page currentPage;
        public TicketReservationViewModel(Page page, Flight flight)
        {
            this.currentPage = page;
            this.flight = flight;
            BackToFlightPreviewCommand = new Command(arg => BackToFlightPreview());
            ApplyReservationCommand = new Command(arg => ApplyReservation());

            station = new BusStationAccess();
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
            Passenger passenger = new Passenger()
            {
                Surname = PassengerSurname,
                Name = PassengerName,
                Patronymic = PassengerPatronymic,
                Nationality = PassengerNationality
            };
            station.AddEntity<Passenger>(passenger);

            Ticket ticket = new Ticket()
            {
                idFlight = flight.idFlight,
                idBus = flight.idBus,
                Cost = 55,
                idPassenger = station.GetEntitiesByPrototype<Passenger>(passenger)[0].idPassenger,
                Purchase_date = DateTime.Now,
            };

            station.AddEntity<Ticket>(ticket);
            station.ChangeEntity<Flight>(flight, new Flight() { Available_Tickets_Amount = flight.Available_Tickets_Amount - 1 });
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
