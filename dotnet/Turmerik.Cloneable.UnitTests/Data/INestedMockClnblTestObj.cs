using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedMockClnblTestObj : INestedClnbl<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>
    {
    }

    public class NestedMockClnblTestObj : NestedClnbl<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, INestedMockClnblTestObj
    {
        public NestedMockClnblTestObj()
        {
        }

        public NestedMockClnblTestObj(MockClnblTestObjImmtbl immtbl) : base(immtbl)
        {
        }

        public NestedMockClnblTestObj(MockClnblTestObjImmtbl immtbl, MockClnblTestObjMtbl mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
