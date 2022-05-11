using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Services.DriveItems
{
    public interface ITabPageHead : ICloneableObject
    {
        Guid Uuid { get; }
        string Name { get; }
        bool IsCurrent { get; }
        DriveFolder DriveFolder { get; }
    }

    public class TabPageHeadImmtbl : CloneableObjectImmtblBase, ITabPageHead
    {
        public TabPageHeadImmtbl(ClnblArgs args) : base(args)
        {
        }

        public TabPageHeadImmtbl(ICloneableMapper mapper, ITabPageHead src) : base(mapper, src)
        {
        }

        public Guid Uuid { get; protected set; }
        public string Name { get; protected set; }
        public bool IsCurrent { get; protected set; }
        public DriveFolder DriveFolder { get; protected set; }
    }

    public class TabPageHeadMtbl : CloneableObjectMtblBase, ITabPageHead
    {
        public TabPageHeadMtbl()
        {
        }

        public TabPageHeadMtbl(ClnblArgs args) : base(args)
        {
        }

        public TabPageHeadMtbl(ICloneableMapper mapper, ITabPageHead src) : base(mapper, src)
        {
        }

        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public DriveFolder DriveFolder { get; set; }
    }
}
