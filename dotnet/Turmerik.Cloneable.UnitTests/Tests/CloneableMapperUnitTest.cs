using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Cloneable.UnitTests.Data;
using Turmerik.Core.Cloneable;
using Turmerik.Testing.Core.Tests;
using Xunit;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    public class CloneableMapperUnitTest : UnitTestBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ICloneableMapper clnblMppr;

        public CloneableMapperUnitTest()
        {
            serviceProvider = ServiceProviderContainer.Instance.Value.Services;
            clnblMppr = serviceProvider.GetRequiredService<ICloneableMapper>();
        }

        [Fact]
        public void ClnblObjTest()
        {
            var mtbl = GetTestClnblMtbl(1);
            var immtbl = new ClnblTestObjImmtbl(clnblMppr, mtbl);

            AssertClnblTestObjPropsEqual(immtbl, mtbl);
        }

        [Fact]
        public void ParentClnblObjTest1()
        {
            var mtbl = GetTestParentClnblMtbl(1);
            var immtbl = new ClnblParentTestObjImmtbl(clnblMppr, mtbl);

            AssertClnblParentTestObjPropsEqual(immtbl, mtbl);
        }

        [Fact]
        public void ParentClnblObjTest2()
        {
            var mtbl = GetTestParentClnblMtbl(1, null);
            var immtbl = new ClnblParentTestObjImmtbl(clnblMppr, mtbl);

            AssertClnblParentTestObjPropsEqual(immtbl, mtbl);
        }

        [Fact]
        public void ParentClnblObjTest3()
        {
            var mtbl = GetTestParentClnblMtbl(1, 0);
            var immtbl = new ClnblParentTestObjImmtbl(clnblMppr, mtbl);

            AssertClnblParentTestObjPropsEqual(immtbl, mtbl);
        }

        [Fact]
        public void ParentClnblObjTest4()
        {
            var mtbl = GetTestParentClnblMtbl(1, 3);
            var immtbl = new ClnblParentTestObjImmtbl(clnblMppr, mtbl);

            AssertClnblParentTestObjPropsEqual(immtbl, mtbl);
        }

        private void AssertClnblTestObjPropsEqual(
            ClnblTestObjImmtbl immtbl,
            ClnblTestObjMtbl mtbl)
        {
            Assert.Equal(mtbl.Key, immtbl.Key);
            Assert.Equal(mtbl.Value, immtbl.Value);
        }

        private void AssertClnblParentTestObjPropsEqual(
            ClnblParentTestObjImmtbl immtbl,
            ClnblParentTestObjMtbl mtbl)
        {
            AssertClnblTestObjPropsEqual(immtbl, mtbl);

            if (mtbl.Nested != null)
            {
                Assert.NotNull(immtbl.Nested);

                AssertNestedClnblTestObjPropsEqual(
                    immtbl.Nested, mtbl.Nested);
            }
            else
            {
                Assert.Null(immtbl.Nested);
            }

            if (mtbl.NestedNmrbl != null)
            {
                Assert.NotNull(immtbl.NestedNmrbl);

                AssertNestedClnblTestObjNmrblPropsEqual(
                    immtbl.NestedNmrbl, mtbl.NestedNmrbl);
            }
            else
            {
                Assert.Null(immtbl.NestedNmrbl);
            }

            if (mtbl.NestedDictnr != null)
            {
                Assert.NotNull(immtbl.NestedDictnr);

                AssertNestedClnblTestObjDictnrPropsEqual(
                    immtbl.NestedDictnr, mtbl.NestedDictnr);
            }
            else
            {
                Assert.Null(immtbl.NestedDictnr);
            }
        }

        private void AssertNestedClnblTestObjPropsEqual(
            NestedClnblTestObj immtbl,
            NestedClnblTestObj mtbl)
        {
            AssertClnblTestObjPropsEqual(immtbl.Immtbl, mtbl.Mtbl);

            Assert.Null(immtbl.Mtbl);
            Assert.Null(mtbl.Immtbl);
        }

        private void AssertNestedClnblTestObjNmrblPropsEqual(
            NestedClnblTestObjNmrbl immtbl,
            NestedClnblTestObjNmrbl mtbl)
        {
            if (immtbl.Immtbl != null)
            {
                Assert.Equal(mtbl.Mtbl.Count, immtbl.Immtbl.Count);

                for (int i = 0; i < immtbl.Immtbl.Count; i++)
                {
                    AssertClnblTestObjPropsEqual(
                        immtbl.Immtbl[i], mtbl.Mtbl[i]);
                }
            }
            else
            {
                Assert.Null(mtbl.Mtbl);
            }

            Assert.Null(immtbl.Mtbl);
            Assert.Null(mtbl.Immtbl);
        }

        private void AssertNestedClnblTestObjDictnrPropsEqual(
            NestedClnblTestObjDictnr<int> immtbl,
            NestedClnblTestObjDictnr<int> mtbl)
        {
            if (immtbl.Immtbl != null)
            {
                Assert.Equal(mtbl.Mtbl.Count, immtbl.Immtbl.Count);
                int[] keysArr = immtbl.Immtbl.Keys.ToArray();

                for (int i = 0; i < keysArr.Length; i++)
                {
                    int key = keysArr[i];
                    var immtblObj = immtbl.Immtbl[key];

                    ClnblTestObjMtbl mtblObj;
                    bool isMatch = mtbl.Mtbl.TryGetValue(key, out mtblObj);

                    Assert.True(isMatch);

                    AssertClnblTestObjPropsEqual(
                        immtblObj, mtblObj);
                }
            }
            else
            {
                Assert.Null(mtbl.Mtbl);
            }

            Assert.Null(immtbl.Mtbl);
            Assert.Null(mtbl.Immtbl);
        }
    }
}
