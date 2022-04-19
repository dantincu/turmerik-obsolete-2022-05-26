using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Collections
{
    public class TypeMappedCollection<TBaseType> : StaticDataCache<Type, IReadOnlyCollection<Tuple<Type, TBaseType>>>
    {
        private readonly IReadOnlyCollection<Tuple<Type, TBaseType>> allTuples;

        public TypeMappedCollection(IEnumerable<TBaseType> allItems) : this(allItems.Select(
            item => new Tuple<Type, TBaseType>(item.GetType(), item)).RdnlC())
        {
        }

        protected TypeMappedCollection(
            IReadOnlyCollection<Tuple<Type, TBaseType>> allTuples) : base(type => allTuples.RdnlC(
                    item => type != null ? type.IsAssignableFrom(item.Item1) : false))
        {
            this.allTuples = allTuples ?? throw new ArgumentNullException(nameof(allTuples));
            this.AllItems = allTuples.Select(tuple => tuple.Item2).RdnlC();
        }

        public IReadOnlyCollection<TBaseType> AllItems { get; }

        public TType[] Get<TType>()
            where TType : TBaseType
        {
            TType[] retVal;
            var obj = Get(typeof(TType));

            if (obj != null)
            {
                retVal = obj.Select(
                    item => (TType)item.Item2).ToArray();
            }
            else
            {
                retVal = default;
            }

            return retVal;
        }
    }
}
