using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl
{
    public interface INestedClnblNmrblCore<TClnbl, TNested, TNmrbl> : INestedObjNmrblCore<TClnbl, TNested, TNmrbl>
        where TClnbl : ICloneableObject
        where TNmrbl : IEnumerable<TNested>
    {
    }

    public interface INestedClnblNmrbl<TClnbl> : INestedClnblNmrblCore<TClnbl, INestedObj<TClnbl>, IEnumerable<INestedObj<TClnbl>>>
        where TClnbl : ICloneableObject
    {
    }

    public interface INestedImmtblClnblClctn<TClnbl, TImmtbl> : INestedClnblNmrblCore<TClnbl, INestedImmtblObj<TClnbl, TImmtbl>, ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>>>, INestedImmtblObjCore<ReadOnlyCollection<INestedObj<TClnbl>>, ReadOnlyCollection<INestedObj<TClnbl>>>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
    {
    }

    public interface INestedMtblClnblList<TClnbl, TMtbl> : INestedClnblNmrblCore<TClnbl, INestedMtblObj<TClnbl, TMtbl>, List<INestedMtblClnbl<TClnbl, TMtbl>>>, INestedMtblObjCore<List<INestedObj<TClnbl>>, List<INestedObj<TClnbl>>>
        where TClnbl : ICloneableObject
        where TMtbl : class, TClnbl
    {
    }

    public class NestedClnblNmrbl<TClnbl> : NestedClnblCoreBase<IEnumerable<INestedClnbl<TClnbl>>>, INestedClnblNmrbl<TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblNmrbl(IEnumerable<INestedClnbl<TClnbl>> nmrbl) : this(nmrbl?.RdnlC())
        {
        }

        public NestedClnblNmrbl(ReadOnlyCollection<INestedClnbl<TClnbl>> nmrbl)
        {
            ObjCore = nmrbl;
        }

        IEnumerable<INestedObj<TClnbl>> INestedObjCore<IEnumerable<INestedObj<TClnbl>>>.GetObj() => GetObj().Cast<INestedObj<TClnbl>>();
    }

    public class NestedImmtblClnblClctn<TClnbl, TImmtbl> : NestedImmtblClnblCoreBase<ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>>, ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>>>, INestedImmtblClnblClctn<TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
    {
        public NestedImmtblClnblClctn(IEnumerable<INestedImmtblClnbl<TClnbl, TImmtbl>> nmrbl) : this(nmrbl?.RdnlC())
        {
        }

        public NestedImmtblClnblClctn(ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>> immtblColctn)
        {
            ImmtblCore = immtblColctn;
            ObjCore = immtblColctn;
        }

        ReadOnlyCollection<INestedObj<TClnbl>> INestedImmtblObjCore<ReadOnlyCollection<INestedObj<TClnbl>>, ReadOnlyCollection<INestedObj<TClnbl>>>.Immtbl => Immtbl.Cast<INestedObj<TClnbl>>().RdnlC();
        ReadOnlyCollection<INestedObj<TClnbl>> INestedObjCore<ReadOnlyCollection<INestedObj<TClnbl>>>.GetObj() => GetObj().Cast<INestedObj<TClnbl>>().RdnlC();
    }

    public class NestedMtblClnblList<TClnbl, TMtbl> : NestedMtblClnblCoreBase<List<INestedMtblClnbl<TClnbl, TMtbl>>, List<INestedMtblClnbl<TClnbl, TMtbl>>>, INestedMtblClnblList<TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : class, TClnbl
    {
        public NestedMtblClnblList()
        {
        }

        public NestedMtblClnblList(IEnumerable<INestedMtblClnbl<TClnbl, TMtbl>> nmrbl) : this(nmrbl.ToList())
        {
        }

        public NestedMtblClnblList(List<INestedMtblClnbl<TClnbl, TMtbl>> mtblList)
        {
            SetMtbl(mtblList);
        }

        List<INestedObj<TClnbl>> INestedObjCore<List<INestedObj<TClnbl>>>.GetObj() => GetObj().Cast<INestedObj<TClnbl>>().ToList();

        List<INestedObj<TClnbl>> INestedMtblObjCore<List<INestedObj<TClnbl>>, List<INestedObj<TClnbl>>>.Mtbl => Mtbl.Cast<INestedObj<TClnbl>>().ToList();

        public void SetMtbl(List<INestedObj<TClnbl>> mtbl)
        {
            var list = mtbl.Cast<INestedMtblClnbl<TClnbl, TMtbl>>().ToList();
            SetMtbl(list);
        }

        public override void SetMtbl(List<INestedMtblClnbl<TClnbl, TMtbl>> mtbl)
        {
            MtblCore = mtbl;
            ObjCore = mtbl;
        }
    }
}
