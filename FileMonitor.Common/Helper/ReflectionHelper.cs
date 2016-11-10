using System;
using System.Collections.Generic;
using System.Reflection;


namespace FileMonitor.Common.Helper
{
    public static class ReflectionHelper
    {
        public static T GetObject<T>(T model, IDictionary<string,string> source)
        {
            Type type = model.GetType();
            foreach (var item in source)
            {
                PropertyInfo prop = type.GetProperty(item.Key);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(model, Convert.ChangeType(item.Value, prop.PropertyType), null);
                }   
            }

            return model;
        }
    }
}
