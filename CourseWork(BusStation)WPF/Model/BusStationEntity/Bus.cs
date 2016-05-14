using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    class Bus
    {
        public int idBus { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int Passengers { get; set; }
        public int idDriver { get; set; }
    }
}
