using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    public class Flight
    {
        public int idFlight { get; set; }
        public int idBus { get; set; }
        public int Available_Tickets_Amount { get; set; }
        public TimeSpan Departure_time { get; set; }
        public TimeSpan Arrival_time { get; set; }
        public string Departure_place { get; set; }
        public string Arrival_Place { get; set; }
        public string Intermediate_stops { get; set; }
        public DateTime Departure_date { get; set; }
        public DateTime Arrival_date { get; set; }
    }
}
