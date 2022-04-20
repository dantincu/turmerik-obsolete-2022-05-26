using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl
{
    public interface INestedClnblNmrblCore<TClnbl, TNested> : INestedClnblCore<IEnumerable<TNested>>, INestedObjNmrblCore<TClnbl, TNested>
    {
    }

    public interface INestedClnblNmrbl<TClnbl> : INestedClnblNmrblCore<TClnbl, INestedObj<TClnbl>>
        where TClnbl : ICloneableObject
    {
    }

    public interface INestedImmtblClnblClctn<TClnbl, TImmtbl> : INestedClnblNmrblCore<TClnbl, INestedImmtblObj<TClnbl, TImmtbl>>, INestedImmtblClnblCore<IEnumerable<INestedImmtblObj<TClnbl, TImmtbl>>, ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
    }

    public interface INestedMtblClnblList<TClnbl, TMtbl> : INestedClnblNmrblCore<TClnbl, INestedMtblObj<TClnbl, TMtbl>>, INestedMtblClnblCore<IEnumerable<INestedMtblObj<TClnbl, TMtbl>>, List<INestedMtblClnbl<TClnbl, TMtbl>>>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblNmrbl<TClnbl> : NestedObjCoreBase<IEnumerable<INestedClnbl<TClnbl>>>, INestedClnblNmrbl<TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblNmrbl(IEnumerable<INestedClnbl<TClnbl>> nmrbl) : this(nmrbl.RdnlC())
        {
        }

        public NestedClnblNmrbl(ReadOnlyCollection<INestedClnbl<TClnbl>> nmrbl)
        {
            ObjCore = nmrbl;
        }

        IEnumerable<INestedObj<TClnbl>> INestedObjCore<IEnumerable<INestedObj<TClnbl>>>.GetObj()
        {
            var obj = GetObj();
            var retObj = obj.Cast<INestedObj<TClnbl>>();

            return retObj;
        }
    }

    public class NestedImmtblClnblColctn<TClnbl, TImmtbl> : NestedImmtblObjClctn<TClnbl, TImmtbl>, INestedImmtblClnblClctn<TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
        public NestedImmtblClnblColctn(IEnumerable<INestedImmtblClnbl<TClnbl, TImmtbl>> nmrbl) : base(nmrbl)
        {
        }

        public NestedImmtblClnblColctn(ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>> immtblColctn) : base(immtblColctn)
        {
        }

        ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>> INestedImmtblObjCore<IEnumerable<INestedImmtblObj<TClnbl, TImmtbl>>, ReadOnlyCollection<INestedImmtblClnbl<TClnbl, TImmtbl>>>.Immtbl => Immtbl.Cast<INestedImmtblClnbl<TClnbl, TImmtbl>>().RdnlC();
    }

    public class NestedMtblClnblList<TClnbl, TMtbl> : NestedMtblObjList<TClnbl, TMtbl>, INestedMtblClnblList<TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
        public NestedMtblClnblList()
        {
        }

        public NestedMtblClnblList(IEnumerable<INestedMtblClnbl<TClnbl, TMtbl>> nmrbl) : base(nmrbl)
        {
        }

        public NestedMtblClnblList(List<INestedMtblClnbl<TClnbl, TMtbl>> mtblList) : base(mtblList)
        {
        }

        List<INestedMtblClnbl<TClnbl, TMtbl>> INestedMtblObjCore<IEnumerable<INestedMtblObj<TClnbl, TMtbl>>, List<INestedMtblClnbl<TClnbl, TMtbl>>>.Mtbl => Mtbl.Cast<INestedMtblClnbl<TClnbl, TMtbl>>().ToList();

        public void SetMtbl(List<INestedMtblClnbl<TClnbl, TMtbl>> mtbl)
        {
            var baseMtbl = mtbl.Cast<INestedMtblObj<TClnbl, TMtbl>>().ToList();
            base.SetMtbl(baseMtbl);
        }
    }
}
