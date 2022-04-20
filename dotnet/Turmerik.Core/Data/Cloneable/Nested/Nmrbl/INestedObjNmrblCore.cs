using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl
{
    public interface INestedObjNmrblCore<TObj, TNested> : INestedObjCore<IEnumerable<TNested>>
    {
    }

    public interface INestedObjNmrbl<TObj> : INestedObjNmrblCore<TObj, INestedObj<TObj>>
    {
    }

    public interface INestedImmtblObjClctn<TObj, TImmtbl> : INestedObjNmrblCore<TObj, INestedImmtblObj<TObj, TImmtbl>>, INestedImmtblObjCore<IEnumerable<INestedImmtblObj<TObj, TImmtbl>>, ReadOnlyCollection<INestedImmtblObj<TObj, TImmtbl>>>
        where TImmtbl : TObj
    {
    }

    public interface INestedMtblObjList<TObj, TMtbl> : INestedObjNmrblCore<TObj, INestedMtblObj<TObj, TMtbl>>, INestedMtblObjCore<IEnumerable<INestedMtblObj<TObj, TMtbl>>, List<INestedMtblObj<TObj, TMtbl>>>
        where TMtbl : TObj
    {
    }

    public class NestedObjNmrbl<TObj> : NestedObjCoreBase<IEnumerable<INestedObj<TObj>>>, INestedObjNmrbl<TObj>
    {
        public NestedObjNmrbl(IEnumerable<INestedObj<TObj>> nmrbl) : this(nmrbl?.RdnlC())
        {
        }

        public NestedObjNmrbl(ReadOnlyCollection<INestedObj<TObj>> nmrbl)
        {
            ObjCore = nmrbl;
        }
    }

    public class NestedImmtblObjClctn<TObj, TImmtbl> : NestedImmtblObjCoreBase<IEnumerable<INestedImmtblObj<TObj, TImmtbl>>, ReadOnlyCollection<INestedImmtblObj<TObj, TImmtbl>>>, INestedImmtblObjClctn<TObj, TImmtbl>
        where TImmtbl : TObj
    {
        public NestedImmtblObjClctn(IEnumerable<INestedImmtblObj<TObj, TImmtbl>> nmrbl) : this(nmrbl?.RdnlC())
        {
        }

        public NestedImmtblObjClctn(ReadOnlyCollection<INestedImmtblObj<TObj, TImmtbl>> immtblColctn)
        {
            ImmtblCore = immtblColctn;
        }
    }

    public class NestedMtblObjList<TObj, TMtbl> : NestedMtblObjCoreBase<IEnumerable<INestedMtblObj<TObj, TMtbl>>, List<INestedMtblObj<TObj, TMtbl>>>, INestedMtblObjList<TObj, TMtbl>
        where TMtbl : TObj
    {
        public NestedMtblObjList()
        {
        }

        public NestedMtblObjList(IEnumerable<INestedMtblObj<TObj, TMtbl>> nmrbl) : this(nmrbl?.ToList())
        {
        }

        public NestedMtblObjList(List<INestedMtblObj<TObj, TMtbl>> mtblList)
        {
            SetMtbl(mtblList);
        }

        public override void SetMtbl(List<INestedMtblObj<TObj, TMtbl>> mtbl)
        {
            base.SetMtbl(mtbl);
            ObjCore = mtbl;
        }
    }
}
