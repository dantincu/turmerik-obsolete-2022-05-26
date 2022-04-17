using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using Turmerik.Core.Helpers;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.AppSettings
{
    public static class ConfigKeys
    {
        public const string TRMRK = "Trmrk";
    }

    public static class ConfigH
    {
        public static T GetObject<T>(this IConfiguration config, string objKey, T obj, IEnumerable<PropertyWrapper> propWrappers)
        {
            foreach (var propInfo in propWrappers)
            {
                string name = propInfo.Name;
                Type type = propInfo.PropType.Value.Data;

                object value = config.GetValue(type, $"{objKey}:{name}");
                propInfo.Data.SetValue(obj, value);
            }

            return obj;
        }

        public static T GetObject<T>(this IConfiguration config, string objKey, T obj)
        {
            obj = config.GetObject(objKey, obj, TypesH.Cache.Get<T>().InstPubGetPubSetProps.Value);
            return obj;
        }

        public static T GetObject<T>(this IConfiguration config, string objKey, Type type, Action<T> callback = null)
        {
            T obj = Activator.CreateInstance<T>();
            obj = config.GetObject(objKey, obj, TypesH.Cache.Get(type).InstPubGetPubSetProps.Value);

            callback?.Invoke(obj);
            return obj;
        }

        public static T GetObject<T>(this IConfiguration config, string objKey)
        {
            T obj = Activator.CreateInstance<T>();
            obj = config.GetObject(objKey, obj);

            return obj;
        }
    }
}
