using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Cloneable.Nested.Mappers;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable.Nested.Clnbl.Mappers
{
    public interface INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> : INestedObjMapper<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedImmtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public interface INestedMtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> : INestedClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
    }

    public class NestedImmtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> : NestedImmtblObjMapperBase<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>, ReadOnlyCollection<TImmtbl>, List<TMtbl>>, INestedImmtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedImmtblClnblNmrblMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override ReadOnlyCollection<TImmtbl> GetImmtbl(
            List<TMtbl> mtbl) => mtbl.RdnlC(
            obj => clonner.ToImmtbl(obj));

        protected override INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl> GetNested(
            ReadOnlyCollection<TImmtbl> immtbl) => new NestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>(immtbl);
    }

    public class NestedMtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl> : NestedMtblObjMapperBase<INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>, ReadOnlyCollection<TImmtbl>, List<TMtbl>>, INestedMtblClnblNmrblMapper<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        private readonly IClonnerComponent<TClnbl, TImmtbl, TMtbl> clonner;

        public NestedMtblClnblNmrblMapper(
            IClonnerFactory clonnerFactory,
            ICloneableMapper mapper)
        {
            clonner = clonnerFactory.GetClonner<TClnbl, TImmtbl, TMtbl>(mapper);
        }

        protected override List<TMtbl> GetMtbl(
            ReadOnlyCollection<TImmtbl> immtbl) => immtbl.Select(
                obj => clonner.ToMtbl(obj)).ToList();

        protected override INestedClnblNmrbl<TClnbl, TImmtbl, TMtbl> GetNested(
            List<TMtbl> mtbl) => new NestedClnblNmrbl<TClnbl, TImmtbl, TMtbl>(null, mtbl);
    }
}
