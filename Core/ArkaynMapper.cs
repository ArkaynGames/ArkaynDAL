using ArkaynDAL.Interfaces;
using System;
using System.Reflection;

namespace ArkaynDAL.Core
{
    public static class ArkaynMapper
    {
        public static T MapReaderToModel<T>(IArkaynReader reader) where T : new()
        {
            var model = new T();
            var type = typeof(T);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (prop == null || !prop.CanWrite)
                    continue;

                var value = reader.IsDBNull(i) ? null : reader[i];
                prop.SetValue(model, value);
            }

            return model;
        }
    }
}
