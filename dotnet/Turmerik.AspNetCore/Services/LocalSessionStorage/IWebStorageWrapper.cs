using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface IWebStorageWrapper
    {
        IWebStorageSvc Service { get; }
        Task<Tuple<bool, T>> TryGetValueAsync<T>(string key, Func<Exception, ValueTask> errHandler);
        Task<Tuple<bool, T>> TryGetValueAsync<T>(string key, bool removeKeyOnError = true);
        Task<T> GetOrCreateAsync<T>(string key, Func<T> factory, bool removeKeyOnError = true);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, bool removeKeyOnError = true);
        Task<T> AddOrUpdateAsync<T>(string key, Func<T> factory, Func<T, T> updateFunc = null, bool removeKeyOnError = true);
        Task<T> AddOrUpdateAsync<T>(string key, Func<Task<T>> factory, Func<T, Task<T>> updateFunc = null, bool removeKeyOnError = true);
        Task TryRemoveItemAsync(string key);
    }
}
