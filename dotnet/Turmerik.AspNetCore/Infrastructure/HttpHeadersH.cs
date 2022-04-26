using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class HttpHeadersH
    {
        public static void AddValue<T>(
            this IHeaderDictionary headers,
            string key,
            T value)
        {
            headers.Add(key, value?.ToString());
        }

        public static StringValues? GetStr(
            this IHeaderDictionary headers,
            string key)
        {
            StringValues? result;
            StringValues value;

            if (headers.TryGetValue(key, out value))
            {
                result = value;
            }
            else
            {
                result = null;
            }

            return result;
        }

        public static T? GetNullableValue<T>(
            this IHeaderDictionary headers,
            string key,
            TryParseVal<StringValues, T> factory)
            where T : struct
        {
            T? retVal;
            var value = headers.GetStr(key);

            T val;

            if (value.HasValue && factory(value.Value, out val))
            {
                retVal = val;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }

        public static T GetValueOrNull<T>(
            this IHeaderDictionary headers,
            string key,
            TryParseVal<StringValues, T> factory)
            where T : class
        {
            var value = headers.GetStr(key);
            T retVal;

            if (!value.HasValue || !factory(value.Value, out retVal))
            {
                retVal = null;
            }

            return retVal;
        }

        public static string GetStringOrNull(
            this IHeaderDictionary headers,
            string key)
        {
            string value = headers.GetValueOrNull(
                key,
                (StringValues strVal, out string val) =>
                {
                    bool retVal = strVal.Any() && strVal.Skip(1).None();

                    if (retVal)
                    {
                        val = strVal.Single();
                    }
                    else
                    {
                        val = null;
                    }

                    return retVal;
                });

            return value;
        }
    }
}
