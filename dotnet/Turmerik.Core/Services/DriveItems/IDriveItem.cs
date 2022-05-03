using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Services.DriveItems
{
    public interface IDriveItem : IDriveItemCore
    {
        string Extension { get; }
        string NameWithoutExtension { get; }
    }

    public class DriveItemImmtbl : DriveItemCoreImmtbl, IDriveItem
    {
        public DriveItemImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public string Extension { get; protected set; }
        public string NameWithoutExtension { get; protected set; }
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

        public string Extension { get; set; }
        public string NameWithoutExtension { get; set; }
    }
}
