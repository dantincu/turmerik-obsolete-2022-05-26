namespace Turmerik.OneDriveExplorer.Blazor.Server.App.Data
{
    public class TrmrkUserSessionsManager
    {
        private readonly object syncLock;

        public TrmrkUserSessionsManager()
        {
            syncLock = new object();
        }

        
    }
}
