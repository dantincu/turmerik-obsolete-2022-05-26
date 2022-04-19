using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Turmerik.Core.Data;
using Turmerik.Core.Reflection.Wrappers;

namespace Turmerik.Core.Helpers
{
    public interface ITypesStaticDataCache : IStaticDataCache<Type, TypeWrapper>
    {
        TypeWrapper Get<T>();
    }

    public class TypesStaticDataCache : StaticDataCache<Type, TypeWrapper>, ITypesStaticDataCache
    {
        public TypesStaticDataCache() : base(
                type => new TypeWrapper(type))
        {
        }

        public TypeWrapper Get<T>()
        {
            var wrapper = Get(typeof(T));
            return wrapper;
        }
    }
}
