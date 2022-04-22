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

            PerformNestedObjNmrblTestAssertions(rdnlClctn, list);
            PerformNestedObjNmrblTestAssertions(rdnlClctn, null);
            PerformNestedObjNmrblTestAssertions(null, list);
        }

        [Fact]
        public void NestedObjDictnrTest()
        {
            var dictnr = GetTestObjDictnr();
            var rdnlDictnr = dictnr.RdnlD();

            PerformNestedObjDictnrTestAssertions(rdnlDictnr, dictnr);
            PerformNestedObjDictnrTestAssertions(rdnlDictnr, null);
            PerformNestedObjDictnrTestAssertions(null, dictnr);
        }

        private void PerformNestedObjNmrblTestAssertions(
            ReadOnlyCollection<TestObj>? rdnlClctn,
            List<TestObj>? list)
        {
            var nested = new NestedObjNmrbl<TestObj>(
                rdnlClctn, list);

            AssertEqualCore(rdnlClctn, nested.Immtbl);
            AssertEqualCore(list, nested.Mtbl);
        }

        private void PerformNestedObjDictnrTestAssertions(
            ReadOnlyDictionary<int, TestObj>? rdnlDictnr,
            Dictionary<int, TestObj>? dictnr)
        {
            var nested = new NestedObjDictnr<int, TestObj>(
                rdnlDictnr, dictnr);

            AssertEqualCore(rdnlDictnr, nested.Immtbl);
            AssertEqualCore(dictnr, nested.Mtbl);
        }
    }
}