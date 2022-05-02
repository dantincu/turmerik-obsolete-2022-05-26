using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public interface ITabPageHead : ICloneableObject
    {
        string Name { get; }
        bool IsCurrent { get; }
    }

    public class TabPageHeadImmtbl : CloneableObjectImmtblBase, ITabPageHead
    {
        public TabPageHeadImmtbl(ClnblArgs args) : base(args)
        {
        }

        public TabPageHeadImmtbl(ICloneableMapper mapper, ITabPageHead src) : base(mapper, src)
        {
        }

        public string Name { get; protected set; }
        public bool IsCurrent { get; protected set; }
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

        public string Name { get; set; }
        public bool IsCurrent { get; set; }
    }
}
