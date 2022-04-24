using System.Collections.Generic;
using Turmerik.Core.Cloneable.Nested;
using Xunit;
using Turmerik.Core.Helpers;
using System.Collections.ObjectModel;
using System;
using Turmerik.Cloneable.UnitTests.Data;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    public class NestedObjUnitTest : UnitTestBase
    {
        [Fact]
        public void NestedObjNmrblTest()
        {
            var list = GetTestObjList();
            var rdnlClctn = list.RdnlC();

            PerformNestedObjTestAssertions<NestedObjNmrbl<TestObj>, ReadOnlyCollection<TestObj>, List<TestObj>>(rdnlClctn, list);
        }

        [Fact]
        public void NestedObjDictnrTest()
        {
            var dictnr = GetTestObjDictnr();
            var rdnlDictnr = dictnr.RdnlD();

            PerformNestedObjTestAssertions<NestedObjDictnr<int, TestObj>, ReadOnlyDictionary<int, TestObj>, Dictionary<int, TestObj>>(rdnlDictnr, dictnr);
        }

        [Fact]
        public void NestedClnblTest()
        {
            var mtbl = GetTestMockClnblMtbl(1);
            var immtbl = new MockClnblTestObjImmtbl(mtbl);

            PerformNestedObjTestAssertions<NestedMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>(immtbl, mtbl);
        }

        [Fact]
        public void NestedClnblNmrblTest()
        {
            var list = GetTestMockClnblList(3);
            var rdnlClctn = list.RdnlC(mtbl => new MockClnblTestObjImmtbl(mtbl));

            PerformNestedObjTestAssertions<NestedMockClnblTestObjNmrbl, ReadOnlyCollection<MockClnblTestObjImmtbl>, List<MockClnblTestObjMtbl>>(rdnlClctn, list);
        }

        [Fact]
        public void NestedClnblDictnrTest()
        {
            var dictnr = GetTestMockClnblDictnr(3);
            var rdnlDictnr = dictnr.RdnlD(kvp => kvp.Key, kvp => new MockClnblTestObjImmtbl(kvp.Value));

            PerformNestedObjTestAssertions<NestedMockClnblTestObjDictnr<int>, ReadOnlyDictionary<int, MockClnblTestObjImmtbl>, Dictionary<int, MockClnblTestObjMtbl>>(rdnlDictnr, dictnr);
        }

        private void PerformNestedObjTestAssertions<TNested, TImmtbl, TMtbl>(
            TImmtbl immtbl, TMtbl mtbl)
            where TNested : NestedObjBase<TImmtbl, TMtbl>
            where TImmtbl : class
            where TMtbl : class
        {
            var component = new NestedObjTestComponent<TNested, TImmtbl, TMtbl>();

            component.PerformTest(immtbl, mtbl, immtbl, mtbl);
            component.PerformTest(immtbl, null, immtbl, null);
            component.PerformTest(immtbl, null, immtbl);
            component.PerformTest(null, null, null, null);
            component.PerformTest(null, null, null);
            component.PerformTest(null, null);
        }
    }
}