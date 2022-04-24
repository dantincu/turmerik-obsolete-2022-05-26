using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface IClnblTestObj : ICloneableObject
    {
        int Key { get; }
        int Value { get; }
    }

    public class ClnblTestObjImmtbl : CloneableObjectImmtblBase, IClnblTestObj
    {
        public ClnblTestObjImmtbl(ClnblArgs args) : base(args)
        {
        }

        public ClnblTestObjImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public int Key { get; protected set; }
        public int Value { get; protected set; }
    }

    public class ClnblTestObjMtbl : CloneableObjectMtblBase, IClnblTestObj
    {
        public ClnblTestObjMtbl()
        {
        }

        public ClnblTestObjMtbl(ClnblArgs args) : base(args)
        {
        }

        public ClnblTestObjMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public int Key { get; set; }
        public int Value { get; set; }
    }
}
