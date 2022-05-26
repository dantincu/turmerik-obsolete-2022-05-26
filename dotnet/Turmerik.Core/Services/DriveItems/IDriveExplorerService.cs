using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.Core.Services.DriveItems
{
    public interface IDriveExplorerService
    {
        Task NavigateAsync(DriveExplorerServiceArgs args);

        Task<Tuple<Exception, DriveFolder>> GetDriveFolderTupleAsync(
            string driveItemId,
            Guid localSessionGuid,
            bool refreshCache);

        string GetDriveItemId(DriveItemIdentifier identifier);
        string GetDriveItemAddress(DriveItemIdentifier identifier);
        string GetDriveItemPath(DriveItemIdentifier identifier);
        string GetDriveItemUri(DriveItemIdentifier identifier);
    }

    public class DriveExplorerServiceArgs
    {
        public DriveExplorerServiceArgs()
        {
        }

        public DriveExplorerServiceArgs(DriveExplorerServiceArgs src)
        {
            ActionType = src.ActionType;
            TabPageUuid = src.TabPageUuid;
            FolderIdentifier = src.FolderIdentifier;
            FolderNavigation = src.FolderNavigation;
            CacheKeyGuid = src.CacheKeyGuid;
            RefreshCache = src.RefreshCache;
            Data = src.Data;
        }

        public DriveExplorerActionType ActionType { get; set; }
        public bool? ActionAppliesToCurrentFolder { get; set; }
        public Guid? TabPageUuid { get; set; }
        public Guid? TrgTabPageUuid { get; set; }
        public DriveItemIdentifier FolderIdentifier { get; set; }
        public DriveItemIdentifier NewFolderIdentifier { get; set; }
        public DriveItemIdentifier NewItemIdentifier { get; set; }
        public DriveFolderNavigation FolderNavigation { get; set; }
        public Guid CacheKeyGuid { get; set; }
        public bool RefreshCache { get; set; }
        public DriveExplorerData Data { get; set; }
    }
}
