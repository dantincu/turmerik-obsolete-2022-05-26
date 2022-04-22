using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Mappers.Factories
{
    public interface INestedObjDictnrMapperFactory : INestedObjMapperFactory
    {
    }

    public interface INestedObjDictnrMapperFactory<TKey, TObj> : INestedObjMapperFactory<INestedObjDictnr<TKey, TObj>, INestedObjDictnrMapper<TKey, TObj>>, INestedObjDictnrMapperFactory
    {
    }

    public interface INestedImmtblObjDictnrMapperFactory<TKey, TObj> : INestedObjDictnrMapperFactory<TKey, TObj>
    {
    }

    public interface INestedMtblObjDictnrMapperFactory<TKey, TObj> : INestedObjDictnrMapperFactory<TKey, TObj>
    {
    }

    public class NestedImmtblObjDictnrMapperFactory<TKey, TObj> : NestedObjMapperFactoryBase<INestedObjDictnr<TKey, TObj>, INestedObjDictnrMapper<TKey, TObj>>, INestedImmtblObjDictnrMapperFactory<TKey, TObj>
    {
        public NestedImmtblObjDictnrMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedObjDictnrMapper<TKey, TObj> GetMapperInstn() => new NestedImmtblObjDictnrMapper<TKey, TObj>();
    }

    public class NestedMtblObjDictnrMapperFactory<TKey, TObj> : NestedObjMapperFactoryBase<INestedObjDictnr<TKey, TObj>, INestedObjDictnrMapper<TKey, TObj>>, INestedImmtblObjDictnrMapperFactory<TKey, TObj>
    {
        public NestedMtblObjDictnrMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedObjDictnrMapper<TKey, TObj> GetMapperInstn() => new NestedMtblObjDictnrMapper<TKey, TObj>();
    }
}
