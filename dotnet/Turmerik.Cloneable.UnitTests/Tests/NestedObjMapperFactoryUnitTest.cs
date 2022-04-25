using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Cloneable.UnitTests.Data;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;
using Turmerik.Core.Cloneable.Nested.Clnbl;
using Turmerik.Core.Cloneable.Nested.Clnbl.Mappers;
using Turmerik.Core.Cloneable.Nested.Clnbl.Mappers.Factories;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Cloneable.Nested.Mappers.Factories;
using Turmerik.Core.Components;
using Turmerik.Core.Reflection;
using Turmerik.Testing.Core.Tests;
using Xunit;

namespace Turmerik.Cloneable.UnitTests.Tests
{
    public class NestedObjMapperFactoryUnitTest : UnitTestBase
    {
        private readonly ITypesStaticDataCache typesCache;
        private readonly IServiceProvider serviceProvider;
        private readonly IClonnerFactory clonnerFactory;
        private readonly ICloneableMapper clnblMapper;
        private readonly INestedObjMapperMainFactory mainFactory;

        public NestedObjMapperFactoryUnitTest()
        {
            serviceProvider = ServiceProviderContainer.Instance.Value.Services;
            typesCache = serviceProvider.GetRequiredService<ITypesStaticDataCache>();
            clonnerFactory = serviceProvider.GetRequiredService<IClonnerFactory>();
            clnblMapper = serviceProvider.GetRequiredService<ICloneableMapper>();
            mainFactory = serviceProvider.GetRequiredService<INestedObjMapperMainFactory>();
        }

        [Fact]
        public void FactoriesTest()
        {
            NestedObjMapperFactoryCore<NestedImmtblObjNmrblMapperFactory<TestObj>, NestedImmtblObjNmrblMapper<TestObj>>();
            NestedObjMapperFactoryCore<NestedMtblObjNmrblMapperFactory<TestObj>, NestedMtblObjNmrblMapper<TestObj>>();

            NestedObjMapperFactoryCore<NestedImmtblObjDictnrMapperFactory<int, TestObj>, NestedImmtblObjDictnrMapper<int, TestObj>>();
            NestedObjMapperFactoryCore<NestedMtblObjDictnrMapperFactory<int, TestObj>, NestedMtblObjDictnrMapper<int, TestObj>>();

            NestedObjMapperFactoryCore<NestedImmtblClnblMapperFactory<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedImmtblClnblMapper<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();
            NestedObjMapperFactoryCore<NestedMtblClnblMapperFactory<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedMtblClnblMapper<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();

            NestedObjMapperFactoryCore<NestedImmtblClnblNmrblMapperFactory<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedImmtblClnblNmrblMapper<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();
            NestedObjMapperFactoryCore<NestedMtblClnblNmrblMapperFactory<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedMtblClnblNmrblMapper<IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();

            NestedObjMapperFactoryCore<NestedImmtblClnblDictnrMapperFactory<int, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedImmtblClnblDictnrMapper<int, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();
            NestedObjMapperFactoryCore<NestedMtblClnblDictnrMapperFactory<int, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>, NestedMtblClnblDictnrMapper<int, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>>();
        }

        [Fact]
        public void MainFactoryTest()
        {
            NestedObjMapperMainFactoryTestCore<
                NestedObjNmrbl<TestObj>,
                NestedImmtblObjNmrblMapper<TestObj>,
                NestedMtblObjNmrblMapper<TestObj>>();

            NestedObjMapperMainFactoryTestCore<
                NestedObjDictnr<int, TestObj>,
                NestedImmtblObjDictnrMapper<int, TestObj>,
                NestedMtblObjDictnrMapper<int, TestObj>>();

            NestedObjMapperMainFactoryTestCore<
                NestedClnbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedImmtblClnblMapper<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedMtblClnblMapper<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>>();

            NestedObjMapperMainFactoryTestCore<
                NestedClnblNmrbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedImmtblClnblNmrblMapper<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedMtblClnblNmrblMapper<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>>();

            NestedObjMapperMainFactoryTestCore<
                NestedClnblDictnr<int, IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedImmtblClnblDictnrMapper<int, IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>,
                NestedMtblClnblDictnrMapper<int, IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>>();
        }

        private void NestedObjMapperFactoryCore<TFactory, TMapper>()
            where TFactory : INestedObjMapperFactory
            where TMapper : INestedObjMapper
        {
            var factory = ActvtrH.Create<TFactory>(
                clonnerFactory,
                clnblMapper);

            var mapper = factory.GetMapper();

            Assert.Equal(
                typeof(TMapper),
                mapper.GetType());
        }

        private void NestedObjMapperMainFactoryTestCore<TNestedObj, TImmtblMapper, TMtblMapper>()
            where TNestedObj : INestedObj
            where TImmtblMapper : INestedObjMapper
            where TMtblMapper : INestedObjMapper
        {
            NestedObjMapperMainFactoryTestCore<TNestedObj, TImmtblMapper>(false);
            NestedObjMapperMainFactoryTestCore<TNestedObj, TMtblMapper>(true);
        }

        private void NestedObjMapperMainFactoryTestCore<TNestedObj, TMapper>(bool isMtbl)
            where TNestedObj : INestedObj
            where TMapper : INestedObjMapper
        {
            var mapper = mainFactory.GetMapper(
                clnblMapper,
                typeof(TNestedObj),
                isMtbl);

            Assert.Equal(typeof(TMapper), mapper.GetType());
        }
    }
}
