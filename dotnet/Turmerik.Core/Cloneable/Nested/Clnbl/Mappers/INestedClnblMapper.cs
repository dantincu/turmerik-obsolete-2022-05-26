using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers
{
    public interface INestedClnblMapper<TClnbl, TImmtbl, TMtbl> : INestedObjMapper<INestedClnbl<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblMapper<TClnbl, TImmtbl, TMtbl> : NestedImmtblObjMapperBase<INestedClnbl<TClnbl, TImmtbl, TMtbl>, TImmtbl, TMtbl>, INestedImmtblClnblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedImmtblClnblMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override TImmtbl GetImmtbl(TMtbl mtbl) => clonner.ToImmtbl(mtbl);
        protected override INestedClnbl<TClnbl, TImmtbl, TMtbl> GetNested(TImmtbl immtbl) => new NestedClnbl<TClnbl, TImmtbl, TMtbl>(immtbl);
    }

    public class NestedMtblClnblMapper<TClnbl, TImmtbl, TMtbl> : NestedMtblObjMapperBase<INestedClnbl<TClnbl, TImmtbl, TMtbl>, TImmtbl, TMtbl>, INestedMtblClnblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedMtblClnblMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override TMtbl GetMtbl(TImmtbl immtbl) => clonner.ToMtbl(immtbl);
        protected override INestedClnbl<TClnbl, TImmtbl, TMtbl> GetNested(TMtbl mtbl) => new NestedClnbl<TClnbl, TImmtbl, TMtbl>(default, mtbl);
    }
}
