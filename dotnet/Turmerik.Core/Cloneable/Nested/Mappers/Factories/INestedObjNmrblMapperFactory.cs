using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable.Nested.Mappers.Factories
{
    public interface INestedObjNmrblMapperFactory : INestedObjMapperFactory
    {
    }

    public interface INestedObjNmrblMapperFactory<TObj> : INestedObjMapperFactory<INestedObjNmrbl<TObj>, INestedObjNmrblMapper<TObj>>, INestedObjNmrblMapperFactory
    {
    }

    public interface INestedImmtblObjNmrblMapperFactory<TObj> : INestedObjNmrblMapperFactory<TObj>
    {
    }

    public interface INestedMtblObjNmrblMapperFactory<TObj> : INestedObjNmrblMapperFactory<TObj>
    {
    }

    public class NestedImmtblObjNmrblMapperFactory<TObj> : NestedObjMapperFactoryBase<INestedObjNmrbl<TObj>, INestedObjNmrblMapper<TObj>>, INestedImmtblObjNmrblMapperFactory<TObj>
    {
        public NestedImmtblObjNmrblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedObjNmrblMapper<TObj> GetMapperInstn() => new NestedImmtblObjNmrblMapper<TObj>();
    }

    public class NestedMtblObjNmrblMapperFactory<TObj> : NestedObjMapperFactoryBase<INestedObjNmrbl<TObj>, INestedObjNmrblMapper<TObj>>, INestedImmtblObjNmrblMapperFactory<TObj>
    {
        public NestedMtblObjNmrblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedObjNmrblMapper<TObj> GetMapperInstn() => new NestedMtblObjNmrblMapper<TObj>();
    }
}
