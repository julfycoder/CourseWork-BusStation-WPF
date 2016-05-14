using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Input;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class FlightsPreviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Database database;
        public FlightsPreviewViewModel()
        {
            DatabaseBuilder builder = new MySqlDatabaseBuilder();
            builder.SetDatabaseName("mydb");
            builder.SetServerAddress("127.0.0.1");
            builder.SetPort(3306);
            builder.SetUserName("root");
            builder.SetPassword("");
            database = builder.BuildDatabase();
        }

        

        public List<string> Destination
        {
            get;
            set;
        }
        public List<string> DeparturePoint
        {
            get;
            set;
        }
        public DataTable FlightsTable
        {
            get 
            {
                return database.GetData(MySqlQueryConstructor.SelectQuery("Flight"));
            }
            set
            {
                UpdatePropertyChanged("FlightsTable");
            }
        }

        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
