using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Cloneable.UnitTests.Data;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Infrastucture;
using Turmerik.Core.Reflection;
using Turmerik.Testing.Core.Tests;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    public class UnitTestBase : UnitTestCoreBase
    {
        protected const int LIST_COUNT = 3;

        static UnitTestBase()
        {
            RegisterAllServices();
        }

        protected List<ClnblTestObjMtbl> GetTestClnblList(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataList(GetTestClnblMtbl, count, offset);

        protected Dictionary<int, ClnblTestObjMtbl> GetTestClnblDictnr(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataDictrn(GetTestClnblMtbl, count, offset);

        protected List<MockClnblTestObjMtbl> GetTestMockClnblList(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataList(GetTestMockClnblMtbl, count, offset);

        protected Dictionary<int, MockClnblTestObjMtbl> GetTestMockClnblDictnr(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataDictrn(GetTestMockClnblMtbl, count, offset);

        protected List<TestObj> GetTestObjList(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataList(GetTestObj, count);

        protected Dictionary<int, TestObj> GetTestObjDictnr(
            int count = LIST_COUNT,
            int offset = 0) => GetTestDataDictrn(GetTestObj, count);

        protected List<T> GetTestDataList<T>(
            Func<int, T> factory,
            int count = LIST_COUNT,
            int offset = 0) => Enumerable.Range(
            1 + offset, count).Select(factory).ToList();

        protected Dictionary<int, T> GetTestDataDictrn<T>(
            Func<int, T> factory,
            int count = LIST_COUNT,
            int offset = 0) => Enumerable.Range(
            1 + offset, count).ToDictionary(key => key, factory);

        protected TestObj GetTestObj(int key) => new TestObj(key, key * 1000);

        protected MockClnblTestObjMtbl GetTestMockClnblMtbl(int key) => new MockClnblTestObjMtbl
        {
            Key = key,
            Value = key * 1001
        };

        protected ClnblTestObjMtbl GetTestClnblMtbl(int key) => new ClnblTestObjMtbl
        {
            Key = key,
            Value = key * 1002
        };

        protected ClnblParentTestObjMtbl GetTestParentClnblMtbl(int key) => new ClnblParentTestObjMtbl
        {
            Key = key,
            Value = key * 1003
        };

        protected ClnblParentTestObjMtbl GetTestParentClnblMtbl(int key, int? nestedCount)
        {
            var mtbl = GetTestParentClnblMtbl(key);
            ClnblTestObjMtbl obj = GetTestClnblMtbl(1);

            List<ClnblTestObjMtbl> objList = null;
            Dictionary<int, ClnblTestObjMtbl> objDictnr = null;

            if (nestedCount.HasValue)
            {
                objList = GetTestClnblList(nestedCount.Value, +10);
                objDictnr = GetTestClnblDictnr(nestedCount.Value, nestedCount.Value + 10);
            }

            mtbl.Nested = new NestedClnblTestObj(null, obj);
            mtbl.NestedNmrbl = new NestedClnblTestObjNmrbl(null, objList);

            mtbl.NestedDictnr = new NestedClnblTestObjDictnr<int>(null, objDictnr);
            return mtbl;
        }

        private static void RegisterAllServices()
        {
            var services = new ServiceCollection();
            TrmrkCoreServiceCollectionBuilder.RegisterAll(services);

            ServiceProviderContainer.Instance.Value.RegisterServices(services);
        }
    }
}
