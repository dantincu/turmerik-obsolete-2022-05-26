using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class DriveItemCore
    {
        public DriveItemCore()
        {
        }

        public DriveItemCore(DriveItemCore src)
        {
            Id = src.Id;
            Name = src.Name;
        }

        /// <summary>
        /// Path for Local Disk Explorer app or drive item id for cloud storage apps
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class DriveItem : DriveItemCore
    {
        public DriveItem()
        {
        }

        public DriveItem(DriveItemCore src) : base(src)
        {
        }

        public DriveItem(DriveItem src) : base(src)
        {
            ParentFolderId = src.ParentFolderId;
            CreationTime = src.CreationTime;
            LastAccessTime = src.LastAccessTime;
            LastWriteTime = src.LastWriteTime;
            DriveItemType = src.DriveItemType;
        }

        /// <summary>
        /// Only used by cloud storage explorer apps
        /// </summary>
        public string ParentFolderId { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastAccessTime { get; set; }
        public DateTime? LastWriteTime { get; set; }
        public DriveItemType? DriveItemType { get; set; }
    }

    public class DriveFolder : DriveItem
    {
        public DriveFolder()
        {
        }

        public DriveFolder(DriveItemCore src) : base(src)
        {
        }

        public DriveFolder(DriveItem src) : base(src)
        {
        }

        public DriveFolder(DriveFolder src) : base(src)
        {
            IsRootFolder = src.IsRootFolder;
            FolderItems = src.FolderItems;
            FileItems = src.FileItems;
        }

        public bool? IsRootFolder { get; set; }
        public List<DriveItem> FolderItems { get; set; }
        public List<DriveItem> FileItems { get; set; }
    }
}
