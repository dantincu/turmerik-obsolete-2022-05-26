using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested
{
    public interface INestedObjDictnrCore<TObj, TKvp> : INestedObjNmrbl<TObj, TKvp>
    {
    }

    public interface INestedObjDictnr<TKey, TObj> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedObj<TObj>>>
    {
    }

    public interface INestedObjRdnlDictnr<TKey, TObj, TImmtbl> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedImmtblObj<TObj, TImmtbl>>>, INestedImmtblObjCore<IEnumerable<KeyValuePair<TKey, INestedImmtblObj<TObj, TImmtbl>>>, ReadOnlyDictionary<TKey, INestedImmtblObj<TObj, TImmtbl>>>
        where TImmtbl : TObj
    {
    }

    public interface INestedObjEdtblDictnr<TKey, TObj, TMtbl> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedMtblObj<TObj, TMtbl>>>, INestedMtblObjCore<IEnumerable<KeyValuePair<TKey, INestedMtblObj<TObj, TMtbl>>>, Dictionary<TKey, INestedMtblObj<TObj, TMtbl>>>
        where TMtbl : TObj
    {
    }

    public class NestedObjDictnr<TKey, TObj> : NestedObjCoreBase<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>>, INestedObjDictnr<TKey, TObj>
    {
        public NestedObjDictnr(IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> kvpNmrbl) : this(kvpNmrbl?.RdnlD())
        {
        }

        public NestedObjDictnr(ReadOnlyDictionary<TKey, INestedObj<TObj>> dictnr)
        {
            ObjCore = dictnr;
        }
    }

    public class NestedObjRdnlDictnr<TKey, TObj, TImmtbl> : NestedImmtblObjCoreBase<IEnumerable<KeyValuePair<TKey, INestedImmtblObj<TObj, TImmtbl>>>, ReadOnlyDictionary<TKey, INestedImmtblObj<TObj, TImmtbl>>>, INestedObjRdnlDictnr<TKey, TObj, TImmtbl>
        where TImmtbl : TObj
    {
        public NestedObjRdnlDictnr(IEnumerable<KeyValuePair<TKey, INestedImmtblObj<TObj, TImmtbl>>> kvpNmrbl) : this(kvpNmrbl.RdnlD())
        {
        }

        public NestedObjRdnlDictnr(ReadOnlyDictionary<TKey, INestedImmtblObj<TObj, TImmtbl>> dictnr)
        {
            ImmtblCore = dictnr;
        }
    }

    public class NestedObjEdtblDictnr<TKey, TObj, TMtbl> : NestedMtblObjCoreBase<IEnumerable<KeyValuePair<TKey, INestedMtblObj<TObj, TMtbl>>>, Dictionary<TKey, INestedMtblObj<TObj, TMtbl>>>, INestedObjEdtblDictnr<TKey, TObj, TMtbl>
        where TMtbl : TObj
    {
        public NestedObjEdtblDictnr()
        {
        }

        public NestedObjEdtblDictnr(IEnumerable<KeyValuePair<TKey, INestedMtblObj<TObj, TMtbl>>> kvpNmrbl) : this(kvpNmrbl?.Dictnr())
        {
        }

        public NestedObjEdtblDictnr(Dictionary<TKey, INestedMtblObj<TObj, TMtbl>> dictnr)
        {
            SetMtbl(dictnr);
        }

        public override void SetMtbl(Dictionary<TKey, INestedMtblObj<TObj, TMtbl>> mtbl)
        {
            base.SetMtbl(mtbl);
            ObjCore = mtbl;
        }
    }
}
