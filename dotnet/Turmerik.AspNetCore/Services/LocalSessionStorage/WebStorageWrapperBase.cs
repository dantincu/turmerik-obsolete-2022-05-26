using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public abstract class WebStorageWrapperBase : IWebStorageWrapper
    {
        protected WebStorageWrapperBase(IWebStorageSvc storageService)
        {
            Service = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public IWebStorageSvc Service { get; }

        public async Task<Tuple<bool, T>> TryGetValueAsync<T>(string key, Func<Exception, ValueTask> errHandler)
        {
            T value;
            bool hasValue = await Service.ContainKeyAsync(key);

            if (hasValue)
            {
                try
                {
                    value = await Service.GetItemAsync<T>(key);
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

        public async Task<Tuple<bool, T>> TryGetValueAsync<T>(string key, bool removeKeyOnError = true)
        {
            Func<Exception, ValueTask> errHandler;
            errHandler = ex => removeKeyOnError ? Service.RemoveItemAsync(key) : ValueTask.CompletedTask;

            var valTuple = await TryGetValueAsync<T>(key, errHandler);
            return valTuple;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<T> factory, bool removeKeyOnError = true)
        {
            T value = await AddOrUpdateAsync(key, factory, null, removeKeyOnError);
            return value;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, bool removeKeyOnError = true)
        {
            T value = await AddOrUpdateAsync<T>(key, factory, null, removeKeyOnError);
            return value;
        }

        public async Task<T> AddOrUpdateAsync<T>(
            string key,
            Func<T> factory,
            Func<T, T> updateFunc = null,
            bool removeKeyOnError = true)
        {
            var valTuple = await TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item2;

            if (!valTuple.Item1)
            {
                value = factory();
            }

            if (updateFunc != null)
            {
                value = updateFunc(value);
            }

            await Service.SetItemAsync(key, value);
            return value;
        }

        public async Task<T> AddOrUpdateAsync<T>(
            string key,
            Func<Task<T>> factory,
            Func<T, Task<T>> updateFunc = null,
            bool removeKeyOnError = true)
        {
            var valTuple = await TryGetValueAsync<T>(key, removeKeyOnError);
            T value = valTuple.Item2;

            if (!valTuple.Item1)
            {
                value = await factory();
            }

            if (updateFunc != null)
            {
                value = await updateFunc(value);
            }

            await Service.SetItemAsync(key, value);
            return value;
        }

        public async Task TryRemoveItemAsync(
            string key)
        {
            if (await Service.ContainKeyAsync(key))
            {
                await Service.RemoveItemAsync(key);
            }
        }
    }
}
