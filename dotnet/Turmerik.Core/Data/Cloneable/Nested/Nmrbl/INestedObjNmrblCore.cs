using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Nmrbl
{
    public interface INestedObjNmrblCore<TObj, TNested, TNmrbl> : INestedObjCore<TNmrbl>
        where TNmrbl : IEnumerable<TNested>
    {
    }

    public interface INestedObjNmrbl<TObj, TNmrbl> : INestedObjNmrblCore<TObj, INestedObj<TObj>, TNmrbl>
        where TNmrbl : IEnumerable<INestedObj<TObj>>
    {
    }

    public interface INestedObjNmrbl<TObj> : INestedObjNmrbl<TObj, IEnumerable<INestedObj<TObj>>>
    {
    }

    public interface INestedImmtblObjClctn<TObj> : INestedObjNmrbl<TObj, ReadOnlyCollection<INestedObj<TObj>>>, INestedImmtblObjCore<ReadOnlyCollection<INestedObj<TObj>>, ReadOnlyCollection<INestedObj<TObj>>>
    {
    }

    public interface INestedMtblObjList<TObj> : INestedObjNmrbl<TObj, List<INestedObj<TObj>>>, INestedMtblObjCore<List<INestedObj<TObj>>, List<INestedObj<TObj>>>
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

    public class NestedImmtblObjClctn<TObj> : NestedImmtblObjCoreBase<ReadOnlyCollection<INestedObj<TObj>>, ReadOnlyCollection<INestedObj<TObj>>>, INestedImmtblObjClctn<TObj>
    {
        public NestedImmtblObjClctn(IEnumerable<INestedObj<TObj>> nmrbl) : this(nmrbl?.RdnlC())
        {
        }

        public NestedImmtblObjClctn(ReadOnlyCollection<INestedObj<TObj>> immtblColctn)
        {
            ObjCore = immtblColctn;
            ImmtblCore = immtblColctn;
        }
    }

    public class NestedMtblObjList<TObj> : NestedMtblObjCoreBase<List<INestedObj<TObj>>, List<INestedObj<TObj>>>, INestedMtblObjList<TObj>
    {
        public NestedMtblObjList()
        {
        }

        public NestedMtblObjList(IEnumerable<INestedObj<TObj>> nmrbl) : this(nmrbl?.ToList())
        {
        }

        public NestedMtblObjList(List<INestedObj<TObj>> mtblList)
        {
            SetMtbl(mtblList);
        }

        public override void SetMtbl(List<INestedObj<TObj>> mtbl)
        {
            MtblCore = mtbl;
            ObjCore = mtbl;
        }
    }
}
