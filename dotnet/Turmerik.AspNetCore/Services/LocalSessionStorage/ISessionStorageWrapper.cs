using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface ISessionStorageWrapper : IWebStorageWrapper
    {
    }

    public class SessionStorageWrapper : WebStorageWrapperBase, ISessionStorageWrapper
    {
        public SessionStorageWrapper(ISessionStorageSvc sessionStorage) : base(sessionStorage)
        {
        }
    }
}
