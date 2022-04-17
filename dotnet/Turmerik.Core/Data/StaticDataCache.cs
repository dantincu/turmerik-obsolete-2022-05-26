using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data
{
    public class StaticDataCache<TKey, TValue>
    {
        private readonly DataCache<TKey, TValue> innerCache;
        private readonly Func<TKey, TValue> factory;

        public StaticDataCache(Func<TKey, TValue> factory, bool isThreadSafe = false)
        {
            innerCache = new DataCache<TKey, TValue>(isThreadSafe);
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public TValue Get(TKey key)
        {
            TValue value = innerCache.GetOrCreate(key, factory);
            return value;
        }
    }
}
