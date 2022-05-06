using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public interface IGridKeyboardKey : ICloneableObject
    {
        string KeyCode { get; }
        string KeyLabel { get; }
        string BtnCssClass { get; }
    }

    public class GridKeyboardKeyImmtbl : CloneableObjectImmtblBase, IGridKeyboardKey
    {
        public GridKeyboardKeyImmtbl(ClnblArgs args) : base(args)
        {
        }

        public GridKeyboardKeyImmtbl(ICloneableMapper mapper, IGridKeyboardKey src) : base(mapper, src)
        {
        }

        public string KeyCode { get; protected set; }
        public string KeyLabel { get; protected set; }
        public string BtnCssClass { get; protected set; }
    }

    public class GridKeyboardKeyMtbl : CloneableObjectMtblBase, IGridKeyboardKey
    {
        public GridKeyboardKeyMtbl()
        {
        }

        public GridKeyboardKeyMtbl(ClnblArgs args) : base(args)
        {
        }

        public GridKeyboardKeyMtbl(ICloneableMapper mapper, IGridKeyboardKey src) : base(mapper, src)
        {
        }

        public string KeyCode { get; set; }
        public string KeyLabel { get; set; }
        public string BtnCssClass { get; set; }
    }
}
