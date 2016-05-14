using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    class Ticket
    {
        public int idTicket { get; set; }
        public int idPassenger { get; set; }
        public int idFlight { get; set; }
        public int idBus { get; set; }
        public int Cost { get; set; }
        public string Purchase_date { get; set; }
    }
}
