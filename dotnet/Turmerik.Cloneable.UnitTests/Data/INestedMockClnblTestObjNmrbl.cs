using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedMockClnblTestObjNmrbl : INestedClnblNmrbl<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>
    {
    }

    public class NestedMockClnblTestObjNmrbl : NestedClnblNmrbl<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>
    {
        public NestedMockClnblTestObjNmrbl()
        {
        }

        public NestedMockClnblTestObjNmrbl(ReadOnlyCollection<MockClnblTestObjImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedMockClnblTestObjNmrbl(ReadOnlyCollection<MockClnblTestObjImmtbl> immtbl, List<MockClnblTestObjMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
