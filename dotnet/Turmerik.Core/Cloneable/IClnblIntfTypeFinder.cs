using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable
{
    public interface IClnblIntfTypeFinder
    {
        Type GetCloneableType(Type srcType);
    }

    public class ClnblIntfTypeFinder : IClnblIntfTypeFinder
    {
        private readonly ITypesStaticDataCache typesCache;

        public ClnblIntfTypeFinder(ITypesStaticDataCache typesCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
        }

        public Type GetCloneableType(Type srcType)
        {
            Type clnblType;

            if (srcType.IsInterface)
            {
                clnblType = srcType;
            }
            else
            {
                var attr = typesCache.Get(
                    srcType).Attrs.Value.Get<CloneableAttribute>().Single();

                clnblType = attr.Type;
            }

            return clnblType;
        }
    }
}
