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
        public Guid? TabPageUuid { get; set; }
        public int? TabPageIdx { get; set; }
        public DriveFolderIdentifier ParentFolderIdentifier { get; set; }
        public DriveFolderIdentifier FolderIdentifier { get; set; }
        public DriveFolderNavigation FolderNavigation { get; set; }
        public Guid CacheKeyGuid { get; set; }
        public bool RefreshCache { get; set; }
        public DriveExplorerData Data { get; set; }
    }
}
