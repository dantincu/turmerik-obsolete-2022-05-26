using Blazored.LocalStorage;
using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface IWebStorageSvc
    {
        ValueTask ClearAsync(CancellationToken? cancellationToken = null);
        ValueTask<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null);
        ValueTask<string> GetItemAsStringAsync(string key, CancellationToken? cancellationToken = null);
        ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null);
        ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null);
        ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null);
        ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null);
    }

    public interface ISessionStorageSvc : IWebStorageSvc
    {
        ISessionStorageService Storage { get; }
    }

    public interface ILocalStorageSvc : IWebStorageSvc
    {
        ILocalStorageService Storage { get; }
    }

    public class SessionStorageSvc : ISessionStorageSvc
    {
        public SessionStorageSvc(ISessionStorageService storage)
        {
            this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public ISessionStorageService Storage { get; }

        public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
        {
            await Storage.ClearAsync(cancellationToken);
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.ContainKeyAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.GetItemAsync<T>(key, cancellationToken);
            return retVal;
        }

        public async ValueTask<string> GetItemAsStringAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.GetItemAsStringAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await Storage.RemoveItemAsync(key, cancellationToken);
        }

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsync(key, data, cancellationToken);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsStringAsync(key, data, cancellationToken);
        }
    }

    public class LocalStorageSvc : ILocalStorageSvc
    {
        public LocalStorageSvc(ILocalStorageService storage)
        {
            this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public ILocalStorageService Storage { get; }

        public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
        {
            await Storage.ClearAsync(cancellationToken);
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.ContainKeyAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.GetItemAsync<T>(key, cancellationToken);
            return retVal;
        }

        public async ValueTask<string> GetItemAsStringAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.GetItemAsStringAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await Storage.RemoveItemAsync(key, cancellationToken);
        }

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsync(key, data, cancellationToken);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsStringAsync(key, data, cancellationToken);
        }
    }
}
