using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Reflection;

namespace Turmerik.Core.Helpers
{
    public static class DictnrH
    {
        public static TValue AddOrUpdateValue<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> dictnr,
            TKey key,
            Func<TKey, TValue> factory,
            Func<TKey, bool, TValue, TValue> updateFunc)
        {
            var val = dictnr.AddOrUpdate(
                key,
                k => updateFunc(k, false, factory(k)),
                (k, v) => updateFunc(k, true, v));

            return val;
        }

        public static TClnbl AddOrUpdateClnbl<TKey, TClnbl, TImmtbl, TMtbl>(
            this ConcurrentDictionary<TKey, TClnbl> dictnr,
            ICloneableMapper mapper,
            TKey key,
            Func<TKey, TMtbl> factory,
            Func<TKey, bool, TMtbl, TMtbl> updateFunc)
            where TClnbl : ICloneableObject
            where TImmtbl : CloneableObjectImmtblBase, TClnbl
            where TMtbl : CloneableObjectMtblBase, TClnbl
        {
            var clnbl = dictnr.AddOrUpdate(
                key,
                k => ActvtrH.Create<TImmtbl>(
                    mapper,
                    updateFunc(
                        k,
                        false,
                        factory(k))),
                (k, v) => ActvtrH.Create<TImmtbl>(
                    mapper,
                    updateFunc(
                        k,
                        true,
                        ActvtrH.Create<TMtbl>(mapper, v))));

            return clnbl;
        }
    }
}
