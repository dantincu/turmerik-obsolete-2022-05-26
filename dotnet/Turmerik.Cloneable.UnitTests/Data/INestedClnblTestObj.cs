using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedClnblTestObj : INestedClnbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>
    {
    }

    public class NestedClnblTestObj : NestedClnbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>, INestedClnblTestObj
    {
        public NestedClnblTestObj()
        {
        }

        public NestedClnblTestObj(ClnblTestObjImmtbl immtbl) : base(immtbl)
        {
        }

        public NestedClnblTestObj(ClnblTestObjImmtbl immtbl, ClnblTestObjMtbl mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
