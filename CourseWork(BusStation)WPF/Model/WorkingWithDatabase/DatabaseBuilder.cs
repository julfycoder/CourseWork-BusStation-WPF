using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork_BusStation_WPF.Model.WorkingWithDatabase
{
    abstract class DatabaseBuilder
    {
        public abstract void SetServerAddress(string address);
        public abstract void SetDatabaseName(string name);
        public abstract void SetPort(int port);
        public abstract void SetUserName(string userName);
        public abstract void SetPassword(string password);
        public abstract Database BuildDatabase();
    }
}
