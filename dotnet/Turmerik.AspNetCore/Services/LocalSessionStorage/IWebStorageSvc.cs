using Microsoft.JSInterop;
using Newtonsoft.Json;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.AspNetCore.Settings;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface IWebStorageSvc
    {
        ValueTask ClearAsync(CancellationToken? cancellationToken = null);
        ValueTask<T> GetItemAsync<T>(string key, bool canExceedBuffer = false, CancellationToken ? cancellationToken = null);
        ValueTask<string> GetItemAsStringAsync(string key, bool canExceedBuffer = false, CancellationToken ? cancellationToken = null);
        ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null);
        ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null);
        ValueTask RemoveItemsAsync(string[] keysArr, CancellationToken? cancellationToken = null);
        ValueTask<string[]> KeysAsync(CancellationToken? cancellationToken = null);
        ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null);
        ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null);
    }

    public interface ISessionStorageSvc : IWebStorageSvc
    {
        // ISessionStorageService Storage { get; }
    }

    public interface ILocalStorageSvc : IWebStorageSvc
    {
        // ILocalStorageService Storage { get; }
    }

    public abstract class WebStorageBase : IWebStorageSvc
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
                    TrmrkJsH.Get(TrmrkJsH.WebStorage.GetItem),
                    key, IsPersistent);
            }

            return retVal;
        }

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.SetItem),
                key, json, IsPersistent);
        }

        public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
        {
            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.Clear),
                IsPersistent);
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            bool retVal = await JSRuntime.InvokeAsync<bool>(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.ContainsKey),
                key, IsPersistent).AsTask();

            return retVal;
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.RemoveItem),
                key, IsPersistent);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.SetItem),
                key, data, IsPersistent);
        }

        protected async ValueTask<string> GetItemAsBigStringAsync(string key)
        {
            Guid textGuid = Guid.NewGuid();
            string guidStr = textGuid.ToString("N");

            int maxChunkLength = AppSettings.JsInteropTextChunkMaxCharsCount;

            int chunksCount = await JSRuntime.InvokeAsync<int>(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.GetBigItemChunksCount),
                key, guidStr, maxChunkLength, IsPersistent);

            string[] chunksArr = new string[chunksCount];

            for (int i = 0; i < chunksCount; i++)
            {
                string textChunk = await JSRuntime.InvokeAsync<string>(
                    TrmrkJsH.Get(TrmrkJsH.WebStorage.GetBigItemChunk), guidStr, i);

                chunksArr[i] = textChunk;
            }

            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.ClearBigItemChunks), guidStr);

            string bigText = string.Concat(chunksArr);
            return bigText;
        }

        public async ValueTask RemoveItemsAsync(string[] keysArr, CancellationToken? cancellationToken = null)
        {
            await JSRuntime.InvokeVoidAsync(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.RemoveItems),
                keysArr, IsPersistent);
        }

        public async ValueTask<string[]> KeysAsync(CancellationToken? cancellationToken = null)
        {
            var keysArr = await JSRuntime.InvokeAsync<string[]>(
                TrmrkJsH.Get(TrmrkJsH.WebStorage.Keys),
                IsPersistent);

            return keysArr;
        }
    }

    public class SessionStorageSvc : WebStorageBase, ISessionStorageSvc
    {
        public SessionStorageSvc(
            ICloneableMapper mapper,
            // ISessionStorageService storage,
            IJSRuntime jSRuntime,
            ITrmrkAppSettings appSettings) : base(mapper, jSRuntime, appSettings)
        {
            // this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        // public ISessionStorageService Storage { get; }

        protected override bool IsPersistent => false;
    }

    public class LocalStorageSvc : WebStorageBase, ILocalStorageSvc
    {
        public LocalStorageSvc(
            ICloneableMapper mapper,
            // ILocalStorageService storage,
            IJSRuntime jSRuntime,
            ITrmrkAppSettings appSettings) : base(mapper, jSRuntime, appSettings)
        {
            // this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        // public ILocalStorageService Storage { get; }

        protected override bool IsPersistent => true;

        /* public async ValueTask ClearAsync(CancellationToken? cancellationToken = null)
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
        } */
    }
}
