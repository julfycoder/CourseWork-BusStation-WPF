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
            List<Passenger> passengers = station.GetEntities<Passenger>();
            if ((PassengerSurname != null && PassengerSurname != "" && PassengerSurname.Substring(0, 1) != " ") &&
                (PassengerName != null && PassengerName != "" && PassengerName.Substring(0, 1) != " ") &&
                (PassengerPatronymic != null && PassengerPatronymic != "" && PassengerPatronymic.Substring(0, 1) != " ") &&
                (PassengerNationality != null && PassengerNationality != "" && PassengerNationality.Substring(0, 1) != " "))
            {
                foreach (Passenger passenger in passengers)
                {
                    if (passenger.Surname == PassengerSurname && passenger.Name == PassengerName && passenger.Patronymic == PassengerPatronymic && passenger.Nationality == PassengerNationality)
                    {
                        MessageBox.Show("Допускается покупка лишь одного билета на рейс для одного пассажира!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ReserveTicket();
                BackToFlightPreview();
            }
            else MessageBox.Show("Все поля должны быть заполнены\n и данные в полях не должны начинаться на пробел!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                Purchase_date = DateTime.Now.ToString(),
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
        public string FlightDuration
        {
            get
            {
                TimeSpan time = station.GetTimeDuration(flight.idFlight);
                int hours = time.Days * 24 + time.Hours;
                string h = "";
                switch (hours.ToString().Substring(hours.ToString().Length - 1, 1))
                {
                    case "1": h = "час"; break;
                    case "3":
                    case "4": h = "часа"; break;
                    default: h = "часов"; break;
                }
                return "Продолжительность поездки " + hours.ToString() + " " + h;
            }
        }
        #endregion
    }
}
