using Turmerik.Core.Cloneable;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public interface IDriveItem : IDriveItemCore
    {
    }

    public class DriveItemImmtbl : DriveItemCoreImmtbl, IDriveItem
    {
        public DriveItemImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }
    }

    public class DriveItemMtbl : DriveItemCoreMtbl, IDriveItem
    {
        public DriveItemMtbl()
        {
        }

        public DriveItemMtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }
    }
}
