using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Turmerik.Core.Data.Cloneable.Nested.Clnbl.Nmrbl;
using Turmerik.Core.Data.Cloneable.Nested.Dictnr;

namespace Turmerik.Core.Data.Cloneable.Nested.Clnbl.Dictnr
{
    public interface INestedClnblDictnrCore<TClnbl, TKvp> : INestedObjDictnrCore<TClnbl, TKvp>, INestedClnblNmrblCore<TClnbl, TKvp>
    {
    }

    public interface INestedClnblDictnr<TKey, TClnbl> : INestedObjDictnr<TKey, TClnbl>, INestedClnblDictnrCore<TClnbl, KeyValuePair<TKey, INestedClnbl<TClnbl>>>
        where TClnbl : ICloneableObject
    {
    }

    public interface INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> : INestedObjRdnlDictnr<TKey, TClnbl, TImmtbl>, INestedClnblDictnrCore<TClnbl, KeyValuePair<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
    }

    public interface INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> : INestedObjEdtblDictnr<TKey, TClnbl, TMtbl>, INestedClnblDictnrCore<TClnbl, KeyValuePair<TKey, INestedMtblClnbl<TClnbl, TMtbl>>>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
    }

    public class NestedClnblDictnr<TKey, TClnbl> : NestedObjDictnr<TKey, TClnbl>, INestedClnblDictnr<TKey, TClnbl>
        where TClnbl : ICloneableObject
    {
        public NestedClnblDictnr(IEnumerable<KeyValuePair<TKey, INestedObj<TClnbl>>> kvpNmrbl) : base(kvpNmrbl)
        {
        }

        public NestedClnblDictnr(ReadOnlyDictionary<TKey, INestedObj<TClnbl>> dictnr) : base(dictnr)
        {
        }

        IEnumerable<KeyValuePair<TKey, INestedClnbl<TClnbl>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedClnbl<TClnbl>>>>.GetObj()
        {
            var kvpNmrbl = GetObj();

            var retKvpNmrbl = kvpNmrbl.Select(
                kvp => new KeyValuePair<TKey, INestedClnbl<TClnbl>>(
                    kvp.Key, (INestedClnbl<TClnbl>)kvp.Value));

            return retKvpNmrbl;
        }
    }

    public class NestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl> : NestedObjRdnlDictnr<TKey, TClnbl, TImmtbl>, INestedClnblRdnlDictnr<TKey, TClnbl, TImmtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
    {
        public NestedClnblRdnlDictnr(IEnumerable<KeyValuePair<TKey, INestedImmtblObj<TClnbl, TImmtbl>>> kvpNmrbl) : base(kvpNmrbl)
        {
        }

        public NestedClnblRdnlDictnr(ReadOnlyDictionary<TKey, INestedImmtblObj<TClnbl, TImmtbl>> dictnr) : base(dictnr)
        {
        }

        IEnumerable<KeyValuePair<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>>>.GetObj()
        {
            var kvpNmrbl = GetObj();

            var retKvpNmrbl = kvpNmrbl.Select(
                kvp => new KeyValuePair<TKey, INestedImmtblClnbl<TClnbl, TImmtbl>>(
                    kvp.Key, (INestedImmtblClnbl<TClnbl, TImmtbl>)kvp.Value));

            return retKvpNmrbl;
        }
    }

    public class NestedClnblEdtblDictnr<TKey, TClnbl, TMtbl> : NestedObjEdtblDictnr<TKey, TClnbl, TMtbl>, INestedClnblEdtblDictnr<TKey, TClnbl, TMtbl>
        where TClnbl : ICloneableObject
        where TMtbl : TClnbl
    {
        public NestedClnblEdtblDictnr()
        {
        }

        public NestedClnblEdtblDictnr(IEnumerable<KeyValuePair<TKey, INestedMtblObj<TClnbl, TMtbl>>> kvpNmrbl) : base(kvpNmrbl)
        {
        }

        public NestedClnblEdtblDictnr(Dictionary<TKey, INestedMtblObj<TClnbl, TMtbl>> dictnr) : base(dictnr)
        {
        }

        IEnumerable<KeyValuePair<TKey, INestedMtblClnbl<TClnbl, TMtbl>>> INestedObjCore<IEnumerable<KeyValuePair<TKey, INestedMtblClnbl<TClnbl, TMtbl>>>>.GetObj()
        {
            var kvpNmrbl = GetObj();

            var retKvpNmrbl = kvpNmrbl.Select(
                kvp => new KeyValuePair<TKey, INestedMtblClnbl<TClnbl, TMtbl>>(
                    kvp.Key, (INestedMtblClnbl<TClnbl, TMtbl>)kvp.Value));

            return retKvpNmrbl;
        }
    }
}
