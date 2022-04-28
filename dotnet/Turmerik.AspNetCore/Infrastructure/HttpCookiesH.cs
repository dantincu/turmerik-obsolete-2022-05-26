using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Data;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class HttpCookiesH
    {
        public static void AddValue<T>(
            this IResponseCookies cookies,
            string key,
            T value)
        {
            cookies.Append(key, value?.ToString());
        }

        public static string GetStr(
            this IRequestCookieCollection cookies,
            string key)
        {
            string str;

            if (!cookies.TryGetValue(key, out str))
            {
                str = null;
            }

            return str;
        }

        public static T? GetNullableValue<T>(
            this IRequestCookieCollection cookies,
            string key,
            TryParseVal<string, T> factory)
            where T : struct
        {
            T? retVal = cookies.GetNullableValue(
                key,
                str =>
                {
                    T outVal;

                    bool isValid = factory(str, out outVal);
                    return new Tuple<bool, T>(isValid, outVal);
                });

            return retVal;
        }

        public static T? GetNullableValue<T>(
            this IRequestCookieCollection cookies,
            string key,
            Func<string, Tuple<bool, T>> factory)
            where T : struct
        {
            T? retVal;
            string str;

            Tuple<bool, T> tuple = null;

            if (cookies.TryGetValue(
                key, out str))
            {
                tuple = factory(str);
            }

            if (tuple != null && tuple.Item1)
            {
                retVal = tuple.Item2;
            }
            else
            {
                retVal = null;
            }

            return retVal;
        }

        public static T GetValueOrNull<T>(
            this IRequestCookieCollection cookies,
            string key,
            TryParseVal<string, T> factory)
            where T : class
        {
            T retVal;
            string str;

            if (!cookies.TryGetValue(
                key, out str) || factory(
                    str, out retVal))
            {
                retVal = null;
            }

            return retVal;
        }
    }
}
