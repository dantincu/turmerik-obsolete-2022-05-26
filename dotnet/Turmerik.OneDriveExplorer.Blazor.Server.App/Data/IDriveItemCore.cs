using Turmerik.Core.Cloneable;

namespace Turmerik.OneDriveExplorer.Blazor.Server.App.Data
{
    public interface IDriveItemCore : ICloneableObject
    {
        string Id { get; }
        string Name { get; }
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
    }
}
