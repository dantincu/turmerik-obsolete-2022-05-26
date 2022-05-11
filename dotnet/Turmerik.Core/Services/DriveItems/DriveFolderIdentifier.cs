using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public enum DriveItemType
    {
        FsDriveRoot = 1,
        FsSpecialFolder
    }

    public enum DriveExplorerActionType
    {
        Initialize,
        ReloadCurrentTab,
        Navigate,
        NavigateBack,
        NavigateForward,
        ChangeTab,
        NewTab,
        CloseTab,
    }

    public class DriveFolderIdentifier
    {
        public DriveFolderIdentifier()
        {
        }

        public DriveFolderIdentifier(DriveFolderIdentifier src)
        {
            Id = src.Id;
            Path = src.Path;
            Uri = src.Uri;
            Address = src.Address;
            IsRootFolder = src.IsRootFolder;
        }

        public string Id { get; set ; }
        public string Path { get; set; }
        public string Uri { get; set; }
        public string Address { get; set; }
        public bool IsRootFolder { get; set; }
    }
}
