using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Turmerik.Core.Helpers
{
    public static class ReflH
    {
        public static string GetTypeFullDisplayName(this Type type)
        {
            string typeFullName = TypesH.Cache.Get(type).FullDisplayName;
            return typeFullName;
        }

        public static bool PropAccessorIs(
            this PropertyInfo propInfo,
            Func<PropertyInfo, bool> hasAccessorFunc,
            Func<PropertyInfo, MethodInfo> accessorFunc,
            Func<MethodInfo, bool> predicate,
            bool retTrueIfNotExtists = false)
        {
            bool retVal = hasAccessorFunc(propInfo);

            if (retVal)
            {
                MethodInfo accessor = accessorFunc(propInfo);
                retVal = predicate(accessor);
            }
            else if (retTrueIfNotExtists)
            {
                retVal = true;
            }

            return retVal;
        }

        public static bool PropGetterIs(
            this PropertyInfo propInfo,
            Func<MethodInfo, bool> predicate,
            bool retTrueIfNotExtists = false)
        {
            bool retVal = propInfo.PropAccessorIs(
                pi => pi.CanRead,
                pi => pi.GetMethod,
                predicate,
                retTrueIfNotExtists);

            return retVal;
        }

        public static bool PropSetterIs(
            this PropertyInfo propInfo,
            Func<MethodInfo, bool> predicate,
            bool retTrueIfNotExtists = false)
        {
            bool retVal = propInfo.PropAccessorIs(
                pi => pi.CanWrite,
                pi => pi.SetMethod,
                predicate,
                retTrueIfNotExtists);

            return retVal;
        }
    }
}
