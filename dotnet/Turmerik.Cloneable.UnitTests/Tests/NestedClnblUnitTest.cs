using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Cloneable.UnitTests.Data;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    public class NestedClnblUnitTest : UnitTestBase
    {
        private void PerformNestedClnblTestAssertions(
            MockClnblTestObjImmtbl immtbl,
            MockClnblTestObjMtbl mtbl)
        {
            var nested = new NestedClnbl<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>(
                );
        }
    }
}
