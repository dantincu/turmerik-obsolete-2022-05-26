using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Reflection;
using Turmerik.Testing.Core.Tests;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    internal class NestedObjTestComponent<TNested, TImmtbl, TMtbl> : UnitTestCoreBase
            where TNested : NestedObjBase<TImmtbl, TMtbl>
            where TImmtbl : class
            where TMtbl : class
    {
        public void PerformTest(
            TImmtbl immtbl,
            TMtbl mtbl,
            params object[] args)
        {
            var nested = ActvtrH.Create<TNested>(args);

            AssertEqualCore(nested.Immtbl, immtbl);
            AssertEqualCore(nested.Mtbl, mtbl);
        }
    }
}
