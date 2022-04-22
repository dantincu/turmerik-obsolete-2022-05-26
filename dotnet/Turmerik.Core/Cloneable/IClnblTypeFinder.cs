using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Cloneable
{
    public interface ClnblTypeFinder
    {
        Type GetClnblType(Type srcType);
    }

    public interface IImmtblTypeFinder : ClnblTypeFinder
    {
    }

    public interface IMtblTypeFinder : ClnblTypeFinder
    {
    }

    public abstract class ClnblTypeFinderBase<TClnblAttrType> : ClnblTypeFinder
        where TClnblAttrType : CloneableBaseAttribute
    {
        private readonly ITypesStaticDataCache typesCache;
        private readonly IClnblIntfTypeFinder cloneableTypeFinder;

        public ClnblTypeFinderBase(ITypesStaticDataCache typesCache, IClnblIntfTypeFinder cloneableTypeFinder)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.cloneableTypeFinder = cloneableTypeFinder ?? throw new ArgumentNullException(nameof(cloneableTypeFinder));
        }

        public Type GetClnblType(Type srcType)
        {
            Type clnblType = cloneableTypeFinder.GetCloneableType(srcType);

            var attr = typesCache.Get(
                clnblType).Attrs.Value.Get<TClnblAttrType>().Single();

            var immtblType = attr.Type;
            return immtblType;
        }
    }

    public class ImmtblTypeFinder : ClnblTypeFinderBase<CloneableImmtblAttribute>, IImmtblTypeFinder
    {
        public ImmtblTypeFinder(ITypesStaticDataCache typesCache, IClnblIntfTypeFinder cloneableTypeFinder) : base(typesCache, cloneableTypeFinder)
        {
        }
    }

    public class MtblTypeFinder : ClnblTypeFinderBase<CloneableMtblAttribute>, IMtblTypeFinder
    {
        public MtblTypeFinder(ITypesStaticDataCache typesCache, IClnblIntfTypeFinder cloneableTypeFinder) : base(typesCache, cloneableTypeFinder)
        {
        }
    }
}
