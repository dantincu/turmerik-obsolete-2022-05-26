using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable.Nested.Mappers
{
    public interface INestedObjDictnrMapper<TKey, TObj> : INestedObjMapper<INestedObjDictnr<TKey, TObj>>
    {
    }

    public interface INestedImmtblObjDictnrMapper<TKey, TObj> : INestedObjDictnrMapper<TKey, TObj>
    {
    }

    public interface INestedMtblObjDictnrMapper<TKey, TObj> : INestedObjDictnrMapper<TKey, TObj>
    {
    }

    public class NestedImmtblObjDictnrMapper<TKey, TObj> : NestedImmtblObjMapperBase<INestedObjDictnr<TKey, TObj>, ReadOnlyDictionary<TKey, TObj>, Dictionary<TKey, TObj>>, INestedImmtblObjDictnrMapper<TKey, TObj>
    {
        protected override ReadOnlyDictionary<TKey, TObj> GetImmtbl(Dictionary<TKey, TObj> mtbl) => mtbl.RdnlD();
    }

    public class NestedMtblObjDictnrMapper<TKey, TObj> : NestedMtblObjMapperBase<INestedObjDictnr<TKey, TObj>, ReadOnlyDictionary<TKey, TObj>, Dictionary<TKey, TObj>>, INestedMtblObjDictnrMapper<TKey, TObj>
    {
        protected override Dictionary<TKey, TObj> GetMtbl(ReadOnlyDictionary<TKey, TObj> immtbl) => immtbl.Dictnr();
    }
}
