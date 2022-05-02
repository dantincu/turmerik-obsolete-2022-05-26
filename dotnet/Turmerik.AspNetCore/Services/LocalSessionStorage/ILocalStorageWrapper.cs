using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Services.LocalSessionStorage
{
    public interface ILocalStorageWrapper : IWebStorageWrapper
    {
    }

    public class LocalStorageWrapper : WebStorageWrapperBase, ILocalStorageWrapper
    {
        public LocalStorageWrapper(ILocalStorageSvc localStorage) : base(localStorage)
        {
        }
    }
}
