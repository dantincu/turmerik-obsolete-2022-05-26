using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable
{
    public abstract class CloneableBaseAttribute : Attribute
    {
        public CloneableBaseAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public Type Type { get; }
    }

    public class CloneableAttribute : CloneableBaseAttribute
    {
        public CloneableAttribute(Type type) : base(type)
        {
        }
    }

    public class CloneableImmtblAttribute : CloneableBaseAttribute
    {
        public CloneableImmtblAttribute(Type type) : base(type)
        {
        }
    }

    public class CloneableMtblAttribute : CloneableBaseAttribute
    {
        public CloneableMtblAttribute(Type type, Type optionsType = null) : base(type)
        {
            OptionsType = optionsType;
        }

        public Type OptionsType { get; }
    }
}
