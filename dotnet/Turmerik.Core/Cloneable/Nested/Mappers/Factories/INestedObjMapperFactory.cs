using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Mappers.Factories
{
    public interface INestedObjMapperFactory
    {
        INestedObjMapper GetMapper();
    }

    public interface INestedObjMapperFactory<TNested, TMapper> : INestedObjMapperFactory
        where TNested : INestedObj
        where TMapper : INestedObjMapper<TNested>
    {
        TMapper GetMapperInstn();
    }

    public abstract class NestedObjMapperFactoryBase : INestedObjMapperFactory
    {
        protected readonly IClonnerFactory ClonnerFactory;
        protected readonly ICloneableMapper Mapper;

        protected NestedObjMapperFactoryBase(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            ClonnerFactory = clonnerFactory ?? throw new ArgumentNullException(nameof(clonnerFactory));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public abstract INestedObjMapper GetMapper();
    }

    public abstract class NestedObjMapperFactoryBase<TNested, TMapper> : NestedObjMapperFactoryBase, INestedObjMapperFactory<TNested, TMapper>
        where TNested : INestedObj
        where TMapper : INestedObjMapper<TNested>
    {
        protected NestedObjMapperFactoryBase(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public abstract TMapper GetMapperInstn();
        public override INestedObjMapper GetMapper() => GetMapperInstn();
    }
}
