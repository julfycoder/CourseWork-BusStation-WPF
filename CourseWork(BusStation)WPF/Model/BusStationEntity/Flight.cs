using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    public class Flight
    {
        public int idFlight { get; set; }
        public int idBus { get; set; }
        public int Available_Tickets_Amount { get; set; }
        public string Departure_place { get; set; }
        public string Arrival_Place { get; set; }
        public string Intermediate_stops { get; set; }


        bool _DepartureTimeChanged = false;
        public string Departure_time
        {
            get
            {
                if (_DepartureTimeChanged) return _Departure.ToString("HH:mm:ss", new CultureInfo("ru"));
                return null;
            }
            set
            {
                _DepartureTimeChanged = true;
                DateTime temp = DateTime.Parse(value, new CultureInfo("ru"));
                _Departure = new DateTime(_Departure.Year, _Departure.Month, _Departure.Day, temp.Hour, temp.Minute, temp.Second, DateTimeKind.Local);
            }
        }
        bool _ArrivalTimeChanged = false;
        public string Arrival_time
        {
            get
            {
                if (_ArrivalTimeChanged) return _Arrival.ToString("HH:mm:ss", new CultureInfo("ru"));
                return null;
            }
            set
            {
                _ArrivalTimeChanged = true;
                DateTime temp = DateTime.Parse(value, new CultureInfo("ru"));
                _Arrival = new DateTime(_Arrival.Year, _Arrival.Month, _Arrival.Day, temp.Hour, temp.Minute, temp.Second,DateTimeKind.Local);
            }
        }
        bool _DepartureDateChanged = false;
        public string Departure_date
        {
            get
            {
                if (_DepartureDateChanged) return _Departure.ToString("yyyy-MM-dd");
                return null;
            }
            set
            {
                _DepartureDateChanged = true;
                DateTime temp = DateTime.Parse(value);
                _Departure = new DateTime(temp.Year, temp.Month, temp.Day, _Departure.Hour, _Departure.Minute, _Departure.Second);
            }
        }
        bool _ArrivalDateChanged = false;
        public string Arrival_date
        {
            get
            {
                if (_ArrivalDateChanged) return _Arrival.ToString("yyyy-MM-dd");
                return null;
            }
            set
            {
                _ArrivalDateChanged = true;
                DateTime temp = DateTime.Parse(value);
                _Arrival = new DateTime(temp.Year, temp.Month, temp.Day, _Arrival.Hour, _Arrival.Minute, _Arrival.Second);
            }
        }

        private DateTime _Arrival;
        private DateTime _Departure;
    }
}
