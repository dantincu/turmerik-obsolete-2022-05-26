using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface IClnblParentTestObj : IClnblTestObj
    {
        NestedClnblTestObj Nested { get; }
        NestedClnblTestObjNmrbl NestedNmrbl { get; }
        NestedClnblTestObjDictnr<int> NestedDictnr { get; }
    }

    public class ClnblParentTestObjImmtbl : ClnblTestObjImmtbl, IClnblParentTestObj
    {
        public ClnblParentTestObjImmtbl(ClnblArgs args) : base(args)
        {
        }

        public ClnblParentTestObjImmtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public NestedClnblTestObj Nested { get; protected set; }
        public NestedClnblTestObjNmrbl NestedNmrbl { get; protected set; }
        public NestedClnblTestObjDictnr<int> NestedDictnr { get; protected set; }
    }

    public class ClnblParentTestObjMtbl : ClnblTestObjMtbl, IClnblParentTestObj
    {
        public ClnblParentTestObjMtbl()
        {
        }

        public ClnblParentTestObjMtbl(ClnblArgs args) : base(args)
        {
        }

        public ClnblParentTestObjMtbl(ICloneableMapper mapper, ICloneableObject src) : base(mapper, src)
        {
        }

        public NestedClnblTestObj Nested { get; set; }
        public NestedClnblTestObjNmrbl NestedNmrbl { get; set; }
        public NestedClnblTestObjDictnr<int> NestedDictnr { get; set; }
    }
}
