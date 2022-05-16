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
