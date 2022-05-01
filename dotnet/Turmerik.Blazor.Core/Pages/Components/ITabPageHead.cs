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
        string Title { get; }
    }

    public class TabPageHeadImmtbl : CloneableObjectImmtblBase, ITabPageHead
    {
        public TabPageHeadImmtbl(ClnblArgs args) : base(args)
        {
        }

        public TabPageHeadImmtbl(ICloneableMapper mapper, ITabPageHead src) : base(mapper, src)
        {
        }

        public string Title { get; protected set; }
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

        public string Title { get; set; }
    }
}
