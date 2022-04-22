using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable
{
    public interface ICloneableObject
    {
    }

    public abstract class CloneableObjectBase : ICloneableObject
    {
        protected CloneableObjectBase()
        {
        }

        protected CloneableObjectBase(
            ICloneableMapper mapper,
            ICloneableObject src,
            bool isMtbl,
            Type srcType = null,
            Type trgType = null)
        {
            MapProps(
                mapper,
                src,
                isMtbl,
                srcType,
                trgType);
        }

        protected virtual void MapProps(
            ICloneableMapper mapper,
            ICloneableObject src,
            bool isMtbl,
            Type srcType = null,
            Type trgType = null)
        {
            if (src != null)
            {
                var opts = new ObjMapOptsMtbl
                {
                    SrcType = srcType,
                    TrgType = trgType,
                    SrcObj = src,
                    TrgObj = this,
                    PropValSetter = (propInfo, propVal) => propInfo.SetValue(this, propVal),
                    TrgIsMtbl = isMtbl
                };

                var immtblOpts = new ObjMapOptsImmtbl(opts);
                mapper.MapTarget(immtblOpts);
            }
        }
    }

    public abstract class CloneableObjectImmtblBase : CloneableObjectBase, ICloneableObject
    {
        public CloneableObjectImmtblBase(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type srcType = null,
            Type trgType = null) : base(
                mapper,
                src,
                false,
                srcType,
                trgType)
        {
        }
    }

    public abstract class CloneableObjectMtblBase : CloneableObjectBase, ICloneableObject
    {
        public CloneableObjectMtblBase()
        {
        }

        public CloneableObjectMtblBase(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type srcType = null,
            Type trgType = null) : base(
                mapper,
                src,
                true,
                srcType,
                trgType)
        {
        }
    }

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
