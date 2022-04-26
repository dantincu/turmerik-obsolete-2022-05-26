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

        public static async Task<bool> AssureIsSetAsync<T>(
            this ILocalStorageService localStorage,
            string key,
            Func<T> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            bool hasValue = valTuple.Item1;

            if (!hasValue)
            {
                T value = factory();

                if (value != null)
                {
                    await localStorage.SetItemAsync(key, value);
                }
            }

            return !hasValue;
        }

        public static async Task<bool> AssureIsSetAsync<T>(
            this ILocalStorageService localStorage,
            string key,
            Func<Task<T>> factory,
            bool removeKeyOnError = false)
        {
            var valTuple = await localStorage.TryGetValueAsync<T>(key, removeKeyOnError);
            bool hasValue = valTuple.Item1;

            if (!hasValue)
            {
                T value = await factory();

                if (value != null)
                {
                    await localStorage.SetItemAsync(key, value);
                }
            }

            return !hasValue;
        }

        public static async Task<Tuple<bool, T>> TryRemoveItem<T>(
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
