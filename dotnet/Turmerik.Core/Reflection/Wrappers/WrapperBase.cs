using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Turmerik.Core.Collections;

namespace Turmerik.Core.Reflection.Wrappers
{
    public abstract class WrapperBase<TData>
        where TData : MemberInfo
    {
        public readonly TData Data;

        protected WrapperBase(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Name = data.Name;
            DeclaringType = new Lazy<TypeWrapper>(() => new TypeWrapper(data.DeclaringType));

            Attrs = new Lazy<TypeMappedCollection<Attribute>>(
                () => new TypeMappedCollection<Attribute>(
                    data.GetCustomAttributes()));
        }

        public readonly string Name;
        public readonly Lazy<TypeWrapper> DeclaringType;
        public readonly Lazy<TypeMappedCollection<Attribute>> Attrs;
    }
}
