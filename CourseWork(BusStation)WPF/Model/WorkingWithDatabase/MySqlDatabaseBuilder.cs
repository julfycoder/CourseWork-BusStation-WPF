using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.WorkingWithDatabase
{
    class MySqlDatabaseBuilder:DatabaseBuilder
    {
        string connectionQuery = "";
        public override void SetServerAddress(string address)
        {
            connectionQuery += "DataSource=" + address + ";";
        }

        public override void SetDatabaseName(string databaseName)
        {
            connectionQuery += "database=" + databaseName + ";";
        }

        public override void SetPort(int port)
        {
            connectionQuery += "port=" + port.ToString() + ";";
        }

        public override void SetUserName(string userName)
        {
            connectionQuery += "Username=" + userName + ";";
        }

        public override void SetPassword(string password)
        {
            connectionQuery += "Password='" + password + "';";
        }

        public override Database BuildDatabase()
        {
            return new MySqlDatabase(connectionQuery);
        }
    }
}
