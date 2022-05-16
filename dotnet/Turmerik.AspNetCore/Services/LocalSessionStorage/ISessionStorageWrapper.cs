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
