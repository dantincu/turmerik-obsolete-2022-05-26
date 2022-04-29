using System.Collections.Generic;
using System.Collections.ObjectModel;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.AspNetCore.Services.DriveItems
{
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
