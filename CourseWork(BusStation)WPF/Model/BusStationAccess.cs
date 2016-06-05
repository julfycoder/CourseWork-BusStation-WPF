using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;
using CourseWork_BusStation_WPF.Model.BusStationEntity;

namespace CourseWork_BusStation_WPF.Model
{
    class BusStationAccess : IAccessible
    {
        EntityBuilder entityBuilder;
        Database database;
        public BusStationAccess()
        {
            entityBuilder = new EntityBuilder();
            DatabaseBuilder builder = new MySqlDatabaseBuilder();
            builder.SetDatabaseName("mydb");
            builder.SetServerAddress("127.0.0.1");
            builder.SetPort(3306);
            builder.SetUserName("root");
            builder.SetPassword("");
            database = builder.BuildDatabase();
        }

        #region GetEntities
        public List<T> GetEntities<T>(params string[] properties)
        {
            List<T> entities = new List<T>();
            DataTable table = database.GetData(MySqlQueryConstructor.SelectQuery(typeof(T).Name, properties));
            foreach (DataRow row in table.Rows)
            {
                entityBuilder.SetEntity(Activator.CreateInstance<T>());
                foreach (DataColumn column in row.Table.Columns)
                {
                    entityBuilder.SetEntityProperty(column.ColumnName, row[column.ColumnName]);
                }
                entities.Add((T)entityBuilder.BuildEntity());
            }
            return entities;
        }
        public List<T> GetEntitiesByPrototype<T>(T prototype, params string[] properties)
        {
            List<T> entities = new List<T>();
            List<string> conditions = new List<string>();
            foreach (PropertyInfo property in prototype.GetType().GetProperties())
            {
                switch (property.PropertyType.ToString())
                {
                    case "System.Int32": if ((int)property.GetValue(prototype, null) == new int()) continue; break;
                    case "System.String": if ((string)property.GetValue(prototype, null) == null) continue; break;
                    case "System.TimeSpan": if ((TimeSpan)property.GetValue(prototype, null) == new TimeSpan()) continue; break;
                    case "System.DateTime": if ((DateTime)property.GetValue(prototype, null) == new DateTime()) continue; break;
                }
                conditions.Add(MySqlQueryConstructor.SimpleCondition(property.Name, "=", property.GetValue(prototype, null)));
            }
            DataTable table = database.GetData(MySqlQueryConstructor.SelectQuery(typeof(T).Name, properties) + MySqlQueryConstructor.WhereQuery(conditions.ToArray()));

            foreach (DataRow row in table.Rows)
            {
                entityBuilder.SetEntity(Activator.CreateInstance<T>());
                foreach (DataColumn column in row.Table.Columns)
                {
                    entityBuilder.SetEntityProperty(column.ColumnName, row[column.ColumnName]);
                }
                entities.Add((T)entityBuilder.BuildEntity());
            }
            return entities;
        }
        #endregion

        #region Effects
        
        public void AddEntity<T>(T entity)
        {
            DataRow row = new DataTable().NewRow();
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                switch (property.PropertyType.ToString())
                {
                    case "System.String": if ((string)property.GetValue(entity, null) == null) property.SetValue(entity, "", null); break;
                    case "System.TimeSpan": if ((TimeSpan)property.GetValue(entity, null) == null) property.SetValue(entity, new TimeSpan(), null); break;
                    case "System.DateTime": if ((DateTime)property.GetValue(entity, null) == null) property.SetValue(entity, new DateTime(), null); break;
                }
                
                row.Table.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                row[property.Name] = property.GetValue(entity, null);
            }
            database.SendQuery(MySqlQueryConstructor.InsertQuery(entity.GetType().Name, row));
        }

        public void RemoveEntity<T>(T entity)
        {
            List<string> conditions = new List<string>();
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                switch (property.PropertyType.ToString())
                {
                    case "System.Int32": if ((int)property.GetValue(entity, null) == new int()) continue; break;
                    case "System.String": if ((string)property.GetValue(entity, null) == null) continue; break;
                    case "System.TimeSpan": if ((TimeSpan)property.GetValue(entity, null) == new TimeSpan()) continue; break;
                    case "System.DateTime": if ((DateTime)property.GetValue(entity, null) == new DateTime()) continue; break;
                }
                conditions.Add(MySqlQueryConstructor.SimpleCondition(property.Name, "=", property.GetValue(entity, null)));
            }
            database.SendQuery(MySqlQueryConstructor.DeleteQuery(entity.GetType().Name, MySqlQueryConstructor.WhereQuery(conditions.ToArray())));
        }

        public void ChangeEntity<T>(T oldEntity, T newEntity)
        {
            Dictionary<string, object> changes = new Dictionary<string, object>();
            foreach (PropertyInfo property in newEntity.GetType().GetProperties())
            {
                switch (property.PropertyType.ToString())
                {
                    case "System.Int32": if ((int)property.GetValue(newEntity, null) == new int()) continue; break;
                    case "System.String": if ((string)property.GetValue(newEntity, null) == null) continue; break;
                    case "System.TimeSpan": if ((TimeSpan)property.GetValue(newEntity, null) == new TimeSpan()) continue; break;
                    case "System.DateTime": if ((DateTime)property.GetValue(newEntity, null) == new DateTime()) continue; break;
                }
                changes.Add(property.Name, property.GetValue(newEntity, null));
            }
            if (changes.Count == 0) changes.Add("Available_Tickets_Amount", 0);
            database.SendQuery(MySqlQueryConstructor.UpdateQuery(oldEntity.GetType().Name,
                MySqlQueryConstructor.SetQuery(changes)) +
                MySqlQueryConstructor.WhereQuery(MySqlQueryConstructor.SimpleCondition(oldEntity.GetType().GetProperties()[0].Name,
                "=", 
                oldEntity.GetType().GetProperties()[0].GetValue(oldEntity, null))));
        }

        #endregion

    }
}
