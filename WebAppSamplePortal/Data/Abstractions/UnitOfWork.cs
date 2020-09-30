using System;
using System.Collections.Generic;

namespace WebAppSamplePortal.Data.Abstractions
{
    public abstract class UnitOfWorkProperty
    {
        Dictionary<Type, Func<object>> _properties = new Dictionary<Type, Func<object>>();

        public abstract void SaveWork();

        public T Property<T>()
            where T : class
        {
            Func<object> getProperty;

            if (!_properties.TryGetValue(typeof(T), out getProperty))
                return null;

            object property = getProperty();

            if (property == null)
                return null;

            var propertyType = property.GetType();
            var targetType = typeof(T);

            if (propertyType == targetType ||
                propertyType.BaseType == targetType ||
                targetType.IsAssignableFrom(propertyType))
            {
                return property as T;
            }

            throw new InvalidCastException();
        }

        public void Property<T>(Func<T> getter)
            where T : class
        {
            if (_properties.ContainsKey(typeof(T)))
                throw new InvalidOperationException();

            _properties.Add(typeof(T), getter);
        }
    }
}
