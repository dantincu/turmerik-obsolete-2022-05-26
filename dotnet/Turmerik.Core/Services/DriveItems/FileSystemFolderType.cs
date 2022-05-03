using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Core.Services.DriveItems
{
    public enum FileSystemFolderType
    {
        DriveRoot = 1,
        SpecialFolder
    }

    public class CurrentDriveFoldersTuple
    {
        public CurrentDriveFoldersTuple(
            IReadOnlyCollection<IDriveFolder> currentFolders,
            IDriveFolder currentlyOpenFolder,
            int currentlyOpenFolderIdx)
        {
            CurrentFolders = currentFolders ?? throw new ArgumentNullException(nameof(currentFolders));
            CurrentlyOpenFolder = currentlyOpenFolder ?? throw new ArgumentNullException(nameof(currentlyOpenFolder));
            this.CurrentlyOpenFolderIdx = currentlyOpenFolderIdx;
        }

        public IReadOnlyCollection<IDriveFolder> CurrentFolders { get; }
        public IDriveFolder CurrentlyOpenFolder { get; }
        public int CurrentlyOpenFolderIdx { get; }
    }

    public class NestedDriveFolder : NestedClnbl<IDriveFolder, DriveFolderImmtbl, DriveFolderMtbl>
    {
        public NestedDriveFolder()
        {
        }

        public NestedDriveFolder(DriveFolderImmtbl immtbl) : base(immtbl)
        {
        }

        public NestedDriveFolder(DriveFolderImmtbl immtbl, DriveFolderMtbl mtbl) : base(immtbl, mtbl)
        {
        }
    }

    public class DriveFoldersList : NestedClnblNmrbl<IDriveFolder, DriveFolderImmtbl, DriveFolderMtbl>
    {
        public DriveFoldersList()
        {
        }

        public DriveFoldersList(ReadOnlyCollection<DriveFolderImmtbl> immtbl) : base(immtbl)
        {
        }

        public DriveFoldersList(ReadOnlyCollection<DriveFolderImmtbl> immtbl, List<DriveFolderMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }

    public class DriveItemsList : NestedClnblNmrbl<IDriveItem, DriveItemImmtbl, DriveItemMtbl>
    {
        public DriveItemsList()
        {
        }

        public DriveItemsList(ReadOnlyCollection<DriveItemImmtbl> immtbl) : base(immtbl)
        {
        }

        public DriveItemsList(ReadOnlyCollection<DriveItemImmtbl> immtbl, List<DriveItemMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
