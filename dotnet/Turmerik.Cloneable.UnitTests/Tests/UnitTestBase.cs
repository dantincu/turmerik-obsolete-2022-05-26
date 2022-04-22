using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Cloneable.UnitTests.Data;
using Turmerik.Core.Infrastucture;
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

        protected List<TestObj> GetTestObjList(int count = LIST_COUNT)
        {
            var list = Enumerable.Range(
                1, count).Select(GetTestObj).ToList();

            return list;
        }

        protected Dictionary<int, TestObj> GetTestObjDictnr(int count = LIST_COUNT)
        {
            var dictnr = Enumerable.Range(
                1, count).ToDictionary(
                key => key, GetTestObj);

            return dictnr;
        }

        protected TestObj GetTestObj(int key) => new TestObj(key, key * 1000);

        private static void RegisterAllServices()
        {
            var services = new ServiceCollection();
            TrmrkCoreServiceCollectionBuilder.RegisterAll(services);

            ServiceProviderContainer.Instance.Value.RegisterServices(services);
        }
    }
}
