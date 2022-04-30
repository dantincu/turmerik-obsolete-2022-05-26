using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.LocalSession
{
    public interface ILocalSessionsDictnr
    {
        Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid localSessionGuid);
        Task<ILocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid);
    }

    public class LocalSessionsDictnr : ILocalSessionsDictnr
    {
        private readonly ICloneableMapper mapper;

        private readonly ConcurrentDictionary<Guid, ILocalSessionData> dictnr;

        public LocalSessionsDictnr(ICloneableMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            dictnr = new ConcurrentDictionary<Guid, ILocalSessionData>();
        }

        public Task<ILocalSessionData> TryAddOrUpdateLocalSessionAsync(Guid localSessionGuid)
        {
            throw new NotImplementedException();
        }

        public Task<ILocalSessionData> TryRemoveLocalSessionAsync(Guid localSessionGuid)
        {
            throw new NotImplementedException();
        }
    }
}
