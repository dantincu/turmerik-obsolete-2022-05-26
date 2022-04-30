using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public interface IDriveItemCore : ICloneableObject
    {
        string Id { get; }
        string Name { get; }
        NestedDriveFolder ParentFolder { get; }
    }

    public class DriveItemCoreImmtbl : CloneableObjectImmtblBase, IDriveItemCore
    {
        public DriveItemCoreImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemCoreImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public NestedDriveFolder ParentFolder { get; protected set; }
    }

    public class DriveItemCoreMtbl : CloneableObjectMtblBase, IDriveItemCore
    {
        public DriveItemCoreMtbl()
        {
        }

        public DriveItemCoreMtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemCoreMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public NestedDriveFolder ParentFolder { get; set; }
    }

    public class NestedDriveFolder : NestedClnbl<IDriveFolder, DriveFolderImmtbl, DriveFolderMtbl>
    {
    }
}
