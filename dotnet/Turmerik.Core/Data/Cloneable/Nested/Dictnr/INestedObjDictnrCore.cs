using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Nmrbl;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable.Nested.Dictnr
{
    public interface INestedObjDictnrCore<TObj, TKvp, TDictnr> : INestedObjNmrblCore<TObj, TKvp, TDictnr>
        where TDictnr : IEnumerable<TKvp>
    {
    }

    public interface INestedObjDictnr<TKey, TObj, TDictnr> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedObj<TObj>>, TDictnr>
        where TDictnr : IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>
    {
    }

    public interface INestedObjDictnr<TKey, TObj> : INestedObjDictnr<TKey, TObj, IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>>
    {
    }

    public interface INestedObjRdnlDictnr<TKey, TObj> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedObj<TObj>>, ReadOnlyDictionary<TKey, INestedObj<TObj>>>, INestedImmtblObjCore<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>, ReadOnlyDictionary<TKey, INestedObj<TObj>>>, INestedObjDictnr<TKey, TObj>
    {
    }

    public interface INestedObjEdtblDictnr<TKey, TObj> : INestedObjDictnrCore<TObj, KeyValuePair<TKey, INestedObj<TObj>>, Dictionary<TKey, INestedObj<TObj>>>, INestedMtblObjCore<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>, Dictionary<TKey, INestedObj<TObj>>>, INestedObjDictnr<TKey, TObj>
    {
    }

    public class NestedObjDictnr<TKey, TObj> : NestedObjCoreBase<ReadOnlyDictionary<TKey, INestedObj<TObj>>>, INestedObjDictnr<TKey, TObj>
    {
        public NestedObjDictnr(IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> kvpNmrbl) : this(kvpNmrbl?.RdnlD())
        {
        }

        public NestedObjDictnr(ReadOnlyDictionary<TKey, INestedObj<TObj>> dictnr)
        {
            ObjCore = dictnr;
        }

        IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>>.GetObj() => GetObj().RdnlD();
    }

    public class NestedObjRdnlDictnr<TKey, TObj> : NestedImmtblObjCoreBase<ReadOnlyDictionary<TKey, INestedObj<TObj>>, ReadOnlyDictionary<TKey, INestedObj<TObj>>>, INestedObjRdnlDictnr<TKey, TObj>
    {
        public NestedObjRdnlDictnr(IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> kvpNmrbl) : this(kvpNmrbl?.RdnlD())
        {
        }

        public NestedObjRdnlDictnr(ReadOnlyDictionary<TKey, INestedObj<TObj>> dictnr)
        {
            ObjCore = dictnr;
            ImmtblCore = dictnr;
        }

        IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>>.GetObj() => GetObj();
    }

    public class NestedObjEdtblDictnr<TKey, TObj> : NestedMtblObjCoreBase<Dictionary<TKey, INestedObj<TObj>>, Dictionary<TKey, INestedObj<TObj>>>, INestedObjEdtblDictnr<TKey, TObj>
    {
        public NestedObjEdtblDictnr()
        {
        }

        public NestedObjEdtblDictnr(IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> kvpNmrbl) : this(kvpNmrbl?.Dictnr())
        {
        }

        public NestedObjEdtblDictnr(Dictionary<TKey, INestedObj<TObj>> dictnr)
        {
            SetMtbl(dictnr);
        }

        public override void SetMtbl(Dictionary<TKey, INestedObj<TObj>> mtbl)
        {
            MtblCore = mtbl;
            ObjCore = mtbl;
        }

        IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedObj<TObj>>>>.GetObj() => GetObj();
    }
}
