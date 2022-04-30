using Blazored.LocalStorage;
using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Helpers;

namespace Turmerik.AspNetCore.Infrastructure
{
    /// <summary>
    /// Thelse methods can only be called from razor pages, as it performs javascript calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="localStorage"></param>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="isSet"></param>
    public static class SessionStorageH
    {
        public static async Task<Tuple<bool, T>> TryGetValueAsync<T>(
            this ISessionStorageService sessionStorage,
            string key,
            Func<Exception, ValueTask> errHandler = null)
        {
            T value;
            bool hasValue = await sessionStorage.ContainKeyAsync(key);

            if (hasValue)
            {
                try
                {
                    value = await sessionStorage.GetItemAsync<T>(key);
                }
                catch (Exception ex)
                {
                    if (errHandler != null)
                    {
                        var valueTask = errHandler.Invoke(ex);
                        await valueTask.AsTask();
                    }

                    hasValue = false;
                    value = default(T);
                }
            }
            else
            {
                value = default(T);
            }

            return new Tuple<bool, T>(hasValue, value);
        }

        public static async Task<Tuple<bool, T>> TryGetValueAsync<T>(
            this ISessionStorageService sessionStorage,
            string key,
            bool removeKeyOnError)
        {
            var valTuple = await sessionStorage.TryGetValueAsync<T>(
                key, ex => removeKeyOnError ? sessionStorage.RemoveItemAsync(key) : ValueTask.CompletedTask);

            return valTuple;
        }

        public static async Task<T> AddOrUpdateAsync<T>(
            this ISessionStorageService sessionStorage,
            string key,
            Func<T, T> updateFunc,
            Func<T> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await sessionStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item1 ? updateFunc(valTuple.Item2) : factory();

            if (value != null)
            {
                await sessionStorage.SetItemAsync(key, value);
            }

            return value;
        }

        public static async Task<T> AddOrUpdateAsync<T>(
            this ISessionStorageService sessionStorage,
            string key,
            Func<T, Task<T>> updateFunc,
            Func<Task<T>> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await sessionStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item1 ? await updateFunc(valTuple.Item2) : await factory();

            if (value != null)
            {
                await sessionStorage.SetItemAsync(key, value);
            }

            return value;
        }

        public static async Task<Tuple<bool, T>> TryRemoveItemAsync<T>(
            this ISessionStorageService sessionStorage,
            string key)
        {
            var valTuple = await sessionStorage.TryGetValueAsync<T>(key, true);
            
            if (valTuple.Item1)
            {
                await sessionStorage.RemoveItemAsync(key);
            }

            return valTuple;
        }
    }
}
