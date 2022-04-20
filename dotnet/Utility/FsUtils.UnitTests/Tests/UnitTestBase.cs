﻿using FsUtils.Testing.Core.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Infrastucture;

namespace FsUtils.UnitTests.Tests
{
    internal class UnitTestBase : UnitTestCoreBase
    {
        static UnitTestBase()
        {
            RegisterAllServices();
        }

        private static void RegisterAllServices()
        {
            var services = new ServiceCollection();
            TrmrkCoreServiceCollectionBuilder.RegisterAll(services);

            ServiceProviderContainer.Instance.Value.RegisterServices(services);
        }
    }
}
