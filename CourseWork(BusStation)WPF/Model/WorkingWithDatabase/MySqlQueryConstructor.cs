using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CourseWork_BusStation_WPF.Model.WorkingWithDatabase
{
    static class MySqlQueryConstructor
    {
        public static string DeleteQuery(string table, string whereQuery)
        {
            return "DELETE FROM " + table + " " + whereQuery;
        }

        public static string InsertQuery(string table, DataRow row)
        {
            string queryString = "INSERT INTO " + table + "(";
            foreach (DataColumn column in row.Table.Columns)                                                //table(columns)
            {
                queryString += column.ColumnName;
                if (column != row.Table.Columns[row.Table.Columns.Count - 1]) queryString += ", ";
                else queryString += ") VALUES(";
            }
            foreach (DataColumn column in row.Table.Columns)                                                //value(values)
            {
                switch (row[column].GetType().ToString())
                {
                    case "System.String": queryString += "'" + row[column].ToString() + "'"; break;
                    case "System.Int32": queryString += row[column].ToString(); break;
                }
                if (column != row.Table.Columns[row.Table.Columns.Count - 1]) queryString += ", ";
                else queryString += ")";
            }
            return queryString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_object">Number of column or his name</param>
        /// <param name="ASC"></param>
        /// <returns></returns>
        public static string OrderByQuery(object order_object, bool ASC = true)
        {
            string query = " ORDER BY " + order_object;
            if (ASC) query += " ASC ";
            else query += " DESC ";
            return query;
        }

        public static string SelectQuery(string table, params string[] columns)
        {
            string query = "SELECT ";
            if (columns.Length > 0)
            {
                foreach (string column in columns)
                {
                    query += column;
                    if (column != columns[columns.Length - 1]) query += ", ";
                }
            }
            else query += "*";
            return query += " FROM " + table;
        }

        public static string SetQuery(params Dictionary<string, object>[] changes)
        {
            string query = "SET ";
            foreach (Dictionary<string, object> change in changes)
            {
                foreach (string key in change.Keys)
                {
                    query += key + " = ";
                    switch (change[key].GetType().ToString())
                    {
                        case "System.String": query += "'" + change[key] + "'"; break;
                        case "System.Int32": query += change[key].ToString(); break;
                    }
                    if (change != changes[changes.Length - 1]) query += ", ";
                }
            }
            return query;
        }

        public static string UpdateQuery(string table, string setQuery)
        {
            return "UPDATE " + table + " " + setQuery;
        }

        /// <summary>
        /// BETWEEN
        /// </summary>
        /// <param name="column"></param>
        /// <param name="operation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WhereIntervalQuery(string column, object value1, object value2, bool Outside = false)
        {
            string query = "";
            if (!Outside) query = " WHERE " + column + " BETWEEN ";
            else query = " WHERE " + column + " NOT BETWEEN ";
            switch (value1.GetType().ToString())
            {
                case "System.String": query += "'" + value1 + "'"; break;
                case "System.Int32": query += value1; break;
            }
            query += " AND ";
            switch (value2.GetType().ToString())
            {
                case "System.String": query += "'" + value2 + "'"; break;
                case "System.Int32": query += value2; break;
            }
            return query;
        }
        public static string WhereSelectionQuery(string column, bool without, params object[] values)
        {
            string query = "";
            if (!without) query = " WHERE " + column + " IN(";
            else query = " WHERE " + column + " NOT IN(";
            foreach (object value in values)
            {
                switch (value.GetType().ToString())
                {
                    case "System.String": query += "'" + value + "'"; break;
                    case "System.Int32": query += value; break;
                }
                if (value != values[values.Length - 1]) query += ", ";
                else query += ") ";
            }
            return query;
        }
        public static string WhereNULLQuery(string column, bool isNULL = true)
        {
            if (isNULL) return " WHERE " + column + " IS NULL";
            return " WHERE " + column + " IS NOT NULL";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="operation">such as '>', "=", "!=", ...</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WhereQuery(string column, string operation, object value)
        {
            string query = " WHERE " + column + " " + operation + " ";
            switch (value.GetType().ToString())
            {
                case "System.String": query += "'" + value + "'"; break;
                case "System.Int32": query += value; break;
            }
            return query;
        }
    }
}
