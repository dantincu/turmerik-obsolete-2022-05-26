using System.Collections.Generic;
using System.Collections.ObjectModel;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.Data
{
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
}
