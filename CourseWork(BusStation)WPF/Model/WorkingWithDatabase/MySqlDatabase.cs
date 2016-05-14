using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace CourseWork_BusStation_WPF.Model.WorkingWithDatabase
{
    public class MySqlDatabase : Database
    {
        MySqlConnection connection;
        public MySqlDatabase(string connectionQuery)
            : base(connectionQuery)
        {
            connection = new MySqlConnection(connectionQuery);
        }

        public override void SendQuery(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, connection);

            connection.Open();
            adapter.SelectCommand.ExecuteReader();
            connection.Close();
        }
        public override DataTable GetData(string query)
        {
            DataSet dataSet = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, connection);

            adapter.Fill(dataSet);
            return dataSet.Tables[0];
        }
    }
}
