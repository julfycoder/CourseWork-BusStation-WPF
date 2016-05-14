using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CourseWork_BusStation_WPF.Model.WorkingWithDatabase
{
    public abstract class Database
    {
        protected string connectionQuery = "";
        public Database(string connectionQuery) { this.connectionQuery = connectionQuery; }

        public abstract void SendQuery(string query);
        public abstract DataTable GetData(string query);
    }
}
