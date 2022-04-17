using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Data;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Collections
{
    public class TypeMappedCollection<TBaseType> : StaticDataCache<Type, IReadOnlyCollection<TBaseType>>
    {
        private readonly IReadOnlyCollection<TBaseType> allItems;

        public TypeMappedCollection(IEnumerable<TBaseType> allItems) : this(allItems.RdnlC())
        {
        }

        public TypeMappedCollection(TBaseType[] allItems) : this(allItems.RdnlC())
        {
        }

        public TypeMappedCollection(
            IReadOnlyCollection<TBaseType> allItems) : base(type => allItems.RdnlC(
                    item => type != null ? type.IsAssignableFrom(item.GetType()) : false))
        {
            this.allItems = allItems ?? throw new ArgumentNullException(nameof(allItems));
        }

        public TType Get<TType>()
            where TType : TBaseType
        {
            TType retVal;
            var obj = Get(typeof(TType));

            if (obj != null)
            {
                retVal = (TType)obj;
            }
            else
            {
                retVal = default;
            }

            return retVal;
        }
    }
}
