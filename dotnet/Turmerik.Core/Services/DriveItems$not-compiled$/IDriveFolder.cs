﻿using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Core.Services.DriveItems
{
    public interface IDriveFolder : IDriveItemCore
    {
        bool? IsRootFolder { get; }
        FileSystemFolderType? FileSystemFolderType { get; }
        DriveFoldersList DriveFoldersList { get; }
        DriveItemsList DriveItemsList { get; }
    }

    public class DriveFolderImmtbl : DriveItemCoreImmtbl, IDriveFolder
    {
        public DriveFolderImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveFolderImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public bool? IsRootFolder { get; protected set; }
        public FileSystemFolderType? FileSystemFolderType { get; protected set; }
        public DriveFoldersList DriveFoldersList { get; protected set; }
        public DriveItemsList DriveItemsList { get; protected set; }
    }

    public class DriveFolderMtbl : DriveItemCoreMtbl, IDriveFolder
    {
        public DriveFolderMtbl()
        {
        }

        public DriveFolderMtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveFolderMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public bool? IsRootFolder { get; set; }
        public FileSystemFolderType? FileSystemFolderType { get; set; }
        public DriveFoldersList DriveFoldersList { get; set; }
        public DriveItemsList DriveItemsList { get; set; }
    }
}