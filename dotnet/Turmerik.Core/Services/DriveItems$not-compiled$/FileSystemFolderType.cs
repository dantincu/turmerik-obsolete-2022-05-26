using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Core.Services.DriveItems
{
    public enum DriveItemIdentifierType
    {
        Id = 1,
        Path = 2,
        Uri = 4
    }

    public enum FileSystemFolderType
    {
        DriveRoot = 1,
        SpecialFolder
    }

    public class DriveFolder : NestedClnbl<IDriveFolder, DriveFolderImmtbl, DriveFolderMtbl>
    {
        public DriveFolder()
        {
        }

        public DriveFolder(DriveFolderImmtbl immtbl) : base(immtbl)
        {
        }

        public DriveFolder(DriveFolderImmtbl immtbl, DriveFolderMtbl mtbl) : base(immtbl, mtbl)
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

    public class DriveItemIdentifier : NestedClnbl<IDriveItemIdentifier, DriveItemIdentifierImmtbl, DriveItemIdentifierMtbl>
    {
        public DriveItemIdentifier()
        {
        }

        public DriveItemIdentifier(DriveItemIdentifierImmtbl immtbl) : base(immtbl)
        {
        }

        public DriveItemIdentifier(DriveItemIdentifierImmtbl immtbl, DriveItemIdentifierMtbl mtbl) : base(immtbl, mtbl)
        {
        }
    }

    public class TabPageHeadsList : NestedClnblNmrbl<ITabPageHead, TabPageHeadImmtbl, TabPageHeadMtbl>
    {
        public TabPageHeadsList()
        {
        }

        public TabPageHeadsList(ReadOnlyCollection<TabPageHeadImmtbl> immtbl) : base(immtbl)
        {
        }

        public TabPageHeadsList(ReadOnlyCollection<TabPageHeadImmtbl> immtbl, List<TabPageHeadMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
