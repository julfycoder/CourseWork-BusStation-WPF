using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model.BusStationEntity;

namespace CourseWork_BusStation_WPF.Model
{
    interface IAccessible
    {
        List<T> GetEntities<T>(params string[] properties);
        List<T> GetEntitiesByPrototype<T>(T prototype, params string[] properties);

        void AddEntity<T>(T entity);
        void RemoveEntity<T>(T entity);
        void ChangeEntity<T>(T oldEntity, T newEntity);
    }
}
