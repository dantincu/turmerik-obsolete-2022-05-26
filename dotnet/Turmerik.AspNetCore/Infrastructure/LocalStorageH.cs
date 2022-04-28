using Blazored.LocalStorage;
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
    public static class LocalStorageH
    {
        public static async Task<Tuple<bool, T>> TryGetValueAsync<T>(
            this ILocalStorageService localStorage,
            string key,
            Func<Exception, ValueTask> errHandler = null)
        {
            T value;
            bool hasValue = await localStorage.ContainKeyAsync(key);

            if (hasValue)
            {
                try
                {
                    value = await localStorage.GetItemAsync<T>(key);
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
            this ILocalStorageService localStorage,
            string key,
            bool removeKeyOnError)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(
                key, ex => removeKeyOnError ? localStorage.RemoveItemAsync(key) : ValueTask.CompletedTask);

            return valTuple;
        }

        public static async Task<T> AddOrUpdateAsync<T>(
            this ILocalStorageService localStorage,
            string key,
            Func<T, T> updateFunc,
            Func<T> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item1 ? updateFunc(valTuple.Item2) : factory();

            if (value != null)
            {
                await localStorage.SetItemAsync(key, value);
            }

            return value;
        }

        public static async Task<T> AddOrUpdateAsync<T>(
            this ILocalStorageService localStorage,
            string key,
            Func<T, Task<T>> updateFunc,
            Func<Task<T>> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item1 ? await updateFunc(valTuple.Item2) : await factory();

            if (value != null)
            {
                await localStorage.SetItemAsync(key, value);
            }

            return value;
        }

        public static async Task<Tuple<bool, T>> TryRemoveItemAsync<T>(
            this ILocalStorageService localStorage,
            string key)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(key, true);
            
            if (valTuple.Item1)
            {
                await localStorage.RemoveItemAsync(key);
            }

            return valTuple;
        }
    }
}
