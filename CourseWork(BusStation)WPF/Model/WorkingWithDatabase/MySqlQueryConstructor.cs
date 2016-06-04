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
                    case "System.DateTime": queryString += "'" + ((DateTime)row[column]).Year.ToString() + "-" + ((DateTime)row[column]).Month.ToString() + "-" + ((DateTime)row[column]).Day.ToString() + "'"; break;
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
                        case "System.DateTime": query += "'" + ((DateTime)change[key]).Year.ToString() + "-" + ((DateTime)change[key]).Month.ToString() + "-" + ((DateTime)change[key]).Day.ToString() + "'"; break;
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

        public static string WhereQuery(params string[] conditions)
        {
            string query = " WHERE ";
            foreach (string condition in conditions)
            {
                if (condition != conditions[0]) query += " AND ";
                query += condition;
            }
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="operation">such as '>', "=", "!=", ...</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SimpleCondition(string column, string operation, object value)
        {
            string condition = column + " " + operation + " ";
            switch (value.GetType().ToString())
            {
                case "System.String": condition += "'" + value + "'"; break;
                case "System.Int32": condition += value; break;
                case "System.DateTime": condition += "'" + ((DateTime)value).Year.ToString() + "-" + ((DateTime)value).Month.ToString() + "-" + ((DateTime)value).Day.ToString() + "'"; break;
                default: condition += value.ToString(); break;
            }
            return condition;
        }
        public static string IntervalCondition(string column, object value1, object value2, bool Outside = false)
        {
            string condition = "";
            if (!Outside) condition += column + " BETWEEN ";
            else condition = column + " NOT BETWEEN ";
            switch (value1.GetType().ToString())
            {
                case "System.String": condition += "'" + value1 + "'"; break;
                case "System.Int32": condition += value1; break;
                case "System.DateTime": condition += "'" + ((DateTime)value1).Year.ToString() + "-" + ((DateTime)value1).Month.ToString() + "-" + ((DateTime)value1).Day.ToString() + "'"; break;
            }
            condition += " AND ";
            switch (value2.GetType().ToString())
            {
                case "System.String": condition += "'" + value2 + "'"; break;
                case "System.Int32": condition += value2; break;
                case "System.DateTime": condition += "'" + ((DateTime)value2).Year.ToString() + "-" + ((DateTime)value2).Month.ToString() + "-" + ((DateTime)value2).Day.ToString() + "'"; break;
            }
            return condition;
        }
        public static string SelectionCondition(string column, bool without, params object[] values)
        {
            string condition = "";
            if (!without) condition = column + " IN(";
            else condition = " WHERE " + column + " NOT IN(";
            foreach (object value in values)
            {
                switch (value.GetType().ToString())
                {
                    case "System.String": condition += "'" + value + "'"; break;
                    case "System.Int32": condition += value; break;
                    case "System.DateTime": condition += "'" + ((DateTime)value).Year.ToString() + "-" + ((DateTime)value).Month.ToString() + "-" + ((DateTime)value).Day.ToString() + "'"; break;
                }
                if (value != values[values.Length - 1]) condition += ", ";
                else condition += ") ";
            }
            return condition;
        }
        public static string NULLCondition(string column, bool isNULL = true)
        {
            if (isNULL) return column + " IS NULL";
            return column + " IS NOT NULL";
        }
    }
}
