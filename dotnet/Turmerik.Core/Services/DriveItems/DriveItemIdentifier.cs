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
        CreateNewFolderInCurrent,
        CreateNewTextFileInCurrent,
        CreateNewMsOfficeFileInCurrent,
        CreateNewFolderInSelected,
        CreateNewTextFileInSelected,
        CreateNewMsOfficeFileInSelected,
        DeleteCurrentFolder,
        DeleteSelectedFolder,
        DeleteFile,
        RenameCurrentFolder,
        RenameSelectedFolder,
        RenameFile,
        MoveCurrentFolder,
        MoveSelectedFolder,
        MoveFile,
        CopyCurrentFolder,
        CopySelectedFolder,
        CopyFile
    }

    public enum MsOfficeFileType
    {
        Docx,
        Xlsx,
        Pptx
    }

    public class DriveItemIdentifier
    {
        public DriveItemIdentifier()
        {
        }

        public DriveItemIdentifier(DriveItemIdentifier src)
        {
            Id = src.Id;
            Name = src.Name;
            Path = src.Path;
            Uri = src.Uri;
            Address = src.Address;
            ParentId = src.ParentId;
            IsRootFolder = src.IsRootFolder;
            DriveItemType = src.DriveItemType;
            MsOfficeFileType = src.MsOfficeFileType;
        }

        public string Id { get; set ; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Uri { get; set; }
        public string Address { get; set; }
        public string ParentId { get; set; }
        public bool IsRootFolder { get; set; }
        public DriveItemType? DriveItemType { get; set; }
        public MsOfficeFileType? MsOfficeFileType { get; set; }
    }
}
