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

        protected List<MockClnblTestObjMtbl> GetTestClnblList(
            int count = LIST_COUNT) => GetTestDataList(GetTestClnblMtbl, count);

        protected Dictionary<int, MockClnblTestObjMtbl> GetTestClnblDictnr(
            int count = LIST_COUNT) => GetTestDataDictrn(GetTestClnblMtbl, count);

        protected List<TestObj> GetTestObjList(
            int count = LIST_COUNT) => GetTestDataList(GetTestObj, count);

        protected Dictionary<int, TestObj> GetTestObjDictnr(
            int count = LIST_COUNT) => GetTestDataDictrn(GetTestObj, count);

        protected List<T> GetTestDataList<T>(
            Func<int, T> factory,
            int count = LIST_COUNT) => Enumerable.Range(
            1, count).Select(factory).ToList();

        protected Dictionary<int, T> GetTestDataDictrn<T>(
            Func<int, T> factory,
            int count = LIST_COUNT) => Enumerable.Range(
            1, count).ToDictionary(key => key, factory);

        protected TestObj GetTestObj(int key) => new TestObj(key, key * 1000);

        protected MockClnblTestObjMtbl GetTestClnblMtbl(int key) => new MockClnblTestObjMtbl
        {
            Key = key,
            Value = key * 1001
        };

        protected void PerformNestedObjTestAssertions<TNested, TImmtbl, TMtbl>(
            TImmtbl immtbl, TMtbl mtbl)
            where TNested : NestedObjBase<TImmtbl, TMtbl>
            where TImmtbl : class
            where TMtbl : class
        {
            var component = new NestedObjTestAssertionsComponent<TNested, TImmtbl, TMtbl>();

            component.PerformTestAssertions(immtbl, mtbl, immtbl, mtbl);
            component.PerformTestAssertions(immtbl, null, immtbl, null);
            component.PerformTestAssertions(immtbl, null, immtbl);
            component.PerformTestAssertions(null, null, null, null);
            component.PerformTestAssertions(null, null, null);
            component.PerformTestAssertions(null, null);
        }

        private static void RegisterAllServices()
        {
            var services = new ServiceCollection();
            TrmrkCoreServiceCollectionBuilder.RegisterAll(services);

            ServiceProviderContainer.Instance.Value.RegisterServices(services);
        }
    }
}
