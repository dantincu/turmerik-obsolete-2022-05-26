using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Cloneable.Nested.Mappers.Factories;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers.Factories
{
    public interface INestedClnblMapperFactory : INestedObjMapperFactory
    {
    }

    public interface INestedClnblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjMapperFactory<INestedClnbl<TClnbl, TImmtbl, TMtbl>, INestedClnblMapper<TClnbl, TImmtbl, TMtbl>>, INestedClnblMapperFactory
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedClnblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedClnblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnbl<TClnbl, TImmtbl, TMtbl>, INestedClnblMapper<TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedImmtblClnblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblMapper<TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedImmtblClnblMapper<TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }

    public class NestedMtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnbl<TClnbl, TImmtbl, TMtbl>, INestedClnblMapper<TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedMtblClnblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblMapper<TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedMtblClnblMapper<TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }
}
