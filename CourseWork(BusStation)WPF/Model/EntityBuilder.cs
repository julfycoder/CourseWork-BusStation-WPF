using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CourseWork_BusStation_WPF.Model.BusStationEntity
{
    class EntityBuilder
    {
        object entity;
        public void SetEntity(object entity)
        {
            this.entity = entity;
        }
        public void SetEntityProperty(string propertyName, object value)
        {
            string propName = propertyName;
            PropertyInfo property = entity.GetType().GetProperty(propertyName);
            if (value == DBNull.Value) value = null;
            switch (value.GetType().ToString())
            {
                case "System.TimeSpan": value=value.ToString(); break;
                case "System.DateTime": value = value.ToString(); break;
            }
            property.SetValue(entity, value, null);
        }
        public object BuildEntity() { return entity; }
    }
}