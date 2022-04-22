using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers
{
    public interface INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedObjMapper<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> : INestedClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedImmtblObjMapperBase<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>, ReadOnlyDictionary<TKey, TImmtbl>, Dictionary<TKey, TMtbl>>, INestedImmtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedImmtblClnblDictnrMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override ReadOnlyDictionary<TKey, TImmtbl> GetImmtbl(
            Dictionary<TKey, TMtbl> mtbl) => mtbl.RdnlD(
                kvp => kvp.Key, kvp => clonner.ToImmtbl(kvp.Value));
    }

    public class NestedMtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl> : NestedMtblObjMapperBase<INestedClnblDictnr<TKey, TClnbl, TImmtbl, TMtbl>, ReadOnlyDictionary<TKey, TImmtbl>, Dictionary<TKey, TMtbl>>, INestedImmtblClnblDictnrMapper<TKey, TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedMtblClnblDictnrMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override Dictionary<TKey, TMtbl> GetMtbl(
            ReadOnlyDictionary<TKey, TImmtbl> immtbl) => immtbl.ToDictionary(
                kvp => kvp.Key, kvp => clonner.ToMtbl(kvp.Value));
    }
}
