using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Services.DriveItems
{
    public interface IDriveItemIdentifier : ICloneableObject
    {
        string Id { get; }
        string Name { get; }
        string Path { get; }
        string Uri { get; }
    }

    public class DriveItemIdentifierImmtbl : CloneableObjectImmtblBase, IDriveItemIdentifier
    {
        public DriveItemIdentifierImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemIdentifierImmtbl(ICloneableMapper mapper, IDriveItemIdentifier src) : base(mapper, src)
        {
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public string Uri { get; protected set; }
    }

    public class DriveItemIdentifierMtbl : CloneableObjectMtblBase, IDriveItemIdentifier
    {
        public DriveItemIdentifierMtbl()
        {
        }

        public DriveItemIdentifierMtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemIdentifierMtbl(ICloneableMapper mapper, IDriveItemIdentifier src) : base(mapper, src)
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Uri { get; set; }
    }
}
