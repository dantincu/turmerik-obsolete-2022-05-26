using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Components;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface IWebStorageSvc
    {
        ValueTask ClearAsync(CancellationToken? cancellationToken = null);
        ValueTask<T> GetItemAsync<T>(string key, bool canExceedBuffer = false, CancellationToken ? cancellationToken = null);
        ValueTask<string> GetItemAsStringAsync(string key, bool canExceedBuffer = false, CancellationToken ? cancellationToken = null);
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

    public abstract class WebStorageBase
    {
        protected WebStorageBase(
            ICloneableMapper mapper,
            IJSRuntime jSRuntime,
            ITrmrkAppSettings appSettings)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            JSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
            AppSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        protected ICloneableMapper Mapper { get; }
        protected IJSRuntime JSRuntime { get; }
        protected ITrmrkAppSettings AppSettings { get; }

        protected abstract bool IsPersistent { get; }

        public async ValueTask<T> GetItemAsync<T>(string key, bool canExceedBuffer = false, CancellationToken? cancellationToken = null)
        {
            string json = await GetItemAsStringAsync(key, canExceedBuffer, cancellationToken);
            var retVal = JsonConvert.DeserializeObject<T>(json);

            return retVal;
        }

        public async ValueTask<string> GetItemAsStringAsync(string key, bool canExceedBuffer = false, CancellationToken? cancellationToken = null)
        {
            string retVal;
            
            if (canExceedBuffer)
            {
                retVal = await GetItemAsBigStringAsync(key);
            }
            else
            {
                retVal = await JSRuntime.InvokeAsync<string>(
                    JsH.Get(JsH.WebStorage.GetItem),
                    key, IsPersistent);
            }

            return retVal;
        }

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            string json = JsonConvert.SerializeObject(data);

            await JSRuntime.InvokeVoidAsync(
                JsH.Get(JsH.WebStorage.SetItem),
                key, json, IsPersistent);
        }

        protected async ValueTask<string> GetItemAsBigStringAsync(string key)
        {
            Guid textGuid = Guid.NewGuid();
            string guidStr = textGuid.ToString("N");

            int maxChunkLength = AppSettings.JsInteropTextChunkMaxCharsCount;

            int chunksCount = await JSRuntime.InvokeAsync<int>(
                JsH.Get(JsH.WebStorage.GetBigItemChunksCount),
                key, guidStr, maxChunkLength, IsPersistent);

            string[] chunksArr = new string[chunksCount];

            for (int i = 0; i < chunksCount; i++)
            {
                string textChunk = await JSRuntime.InvokeAsync<string>(
                    JsH.Get(JsH.WebStorage.GetBigItemChunk), guidStr, i);

                chunksArr[i] = textChunk;
            }

            await JSRuntime.InvokeVoidAsync(
                JsH.Get(JsH.WebStorage.ClearBigItemChunks), guidStr);

            string bigText = string.Concat(chunksArr);
            return bigText;
        }
    }

    public class SessionStorageSvc : WebStorageBase, ISessionStorageSvc
    {
        public SessionStorageSvc(
            ICloneableMapper mapper,
            ISessionStorageService storage,
            IJSRuntime jSRuntime,
            ITrmrkAppSettings appSettings) : base(mapper, jSRuntime, appSettings)
        {
            this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public ISessionStorageService Storage { get; }

        protected override bool IsPersistent => false;

        public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
        {
            await Storage.ClearAsync(cancellationToken);
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.ContainKeyAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await Storage.RemoveItemAsync(key, cancellationToken);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsStringAsync(key, data, cancellationToken);
        }
    }

    public class LocalStorageSvc : WebStorageBase, ILocalStorageSvc
    {
        public LocalStorageSvc(
            ICloneableMapper mapper,
            ILocalStorageService storage,
            IJSRuntime jSRuntime,
            ITrmrkAppSettings appSettings) : base(mapper, jSRuntime, appSettings)
        {
            this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public ILocalStorageService Storage { get; }

        protected override bool IsPersistent => true;

        public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
        {
            await Storage.ClearAsync(cancellationToken);
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            var retVal = await Storage.ContainKeyAsync(key, cancellationToken);
            return retVal;
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await Storage.RemoveItemAsync(key, cancellationToken);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await Storage.SetItemAsStringAsync(key, data, cancellationToken);
        }
    }
}
