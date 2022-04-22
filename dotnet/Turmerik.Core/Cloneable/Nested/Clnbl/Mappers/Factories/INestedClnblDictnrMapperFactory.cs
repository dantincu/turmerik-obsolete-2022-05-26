using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers.Factories;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers.Factories
{
    public interface INestedClnblDictnrMapperFactory : INestedObjMapperFactory
    {
    }

    public interface INestedClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : INestedObjMapperFactory<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>, INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>>, INestedClnblMapperFactory
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>, INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedImmtblClnblDictnrMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedImmtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }

    public class NestedMtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>, INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblDictnrMapperFactory<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedMtblClnblDictnrMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedMtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }
}
