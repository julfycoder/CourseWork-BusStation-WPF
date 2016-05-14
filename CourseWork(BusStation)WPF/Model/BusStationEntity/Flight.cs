using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    class Flight
    {
        public int idFlight { get; set; }
        public string Arrival_time { get; set; }
        public string Departure_time { get; set; }
        public string Arrival_Place { get; set; }
        public string Intermediate_stops { get; set; }
        public int idBus { get; set; }
        public string Departure_date { get; set; }
        public string Arrival_date { get; set; }
    }
}
