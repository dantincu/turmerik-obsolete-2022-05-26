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
            T? retVal;
            string str;

            T val;

            if (cookies.TryGetValue(
                key, out str) && factory(
                    str, out val))
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
