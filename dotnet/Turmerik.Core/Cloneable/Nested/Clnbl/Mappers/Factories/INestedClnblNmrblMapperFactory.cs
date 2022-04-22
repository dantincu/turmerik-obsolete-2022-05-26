using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers.Factories;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers.Factories
{
    public interface INestedClnblNmrblMapperFactory : INestedObjMapperFactory
    {
    }

    public interface INestedClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedObjMapperFactory<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>, INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>>, INestedClnblMapperFactory
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>, INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedImmtblClnblNmrblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedImmtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }

    public class NestedMtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl> : NestedObjMapperFactoryBase<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>, INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>>, INestedImmtblClnblNmrblMapperFactory<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public NestedMtblClnblNmrblMapperFactory(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper) : base(
                clonnerFactory,
                mapper)
        {
        }

        public override INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> GetMapperInstn(
            ) => new NestedMtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>(ClonnerFactory, Mapper);
    }
}
