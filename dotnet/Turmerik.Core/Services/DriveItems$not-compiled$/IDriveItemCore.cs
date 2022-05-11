using System;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Core.Services.DriveItems
{
    public interface IDriveItemCore : ICloneableObject
    {
        DriveItemIdentifier Identifier { get; }
        string Address { get; }
        string DisplayName { get; }
        bool? IsFolder { get; }
        bool? IsPinned { get; }
        bool? IsStarred { get; }
        DateTime CreationTime { get; }
        DateTime LastAccessTime { get; }
        DateTime LastWriteTime { get; }
        DriveFolder ParentFolder { get; }
    }

    public class DriveItemCoreImmtbl : CloneableObjectImmtblBase, IDriveItemCore
    {
        public DriveItemCoreImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemCoreImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public DriveItemIdentifier Identifier { get; protected set; }
        public string Address { get; protected set; }
        public string DisplayName { get; protected set; }
        public bool? IsFolder { get; protected set; }
        public bool? IsPinned { get; protected set; }
        public bool? IsStarred { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime LastAccessTime { get; protected set; }
        public DateTime LastWriteTime { get; protected set; }
        public DriveFolder ParentFolder { get; protected set; }
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

        public DriveItemIdentifier Identifier { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public bool? IsFolder { get; set; }
        public bool? IsPinned { get; set; }
        public bool? IsStarred { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DriveFolder ParentFolder { get; set; }
    }
}
