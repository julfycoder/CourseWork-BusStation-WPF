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
        private DateTime _purchase_date;

        bool _purchase_dateChanged = false;
        public string Purchase_date
        {
            get
            {
                if (_purchase_dateChanged) return _purchase_date.ToString("yyyy-MM-dd");
                return null;
            }
            set
            {
                _purchase_dateChanged = true;
                DateTime temp = DateTime.Parse(value);
                _purchase_date = new DateTime(temp.Year, temp.Month, temp.Day, _purchase_date.Hour, _purchase_date.Minute, _purchase_date.Second);
            }
        }
    }
}
