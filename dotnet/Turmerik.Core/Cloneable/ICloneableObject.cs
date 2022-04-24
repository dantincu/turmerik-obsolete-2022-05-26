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

        protected CloneableObjectBase(ClnblArgs args, bool isMtbl)
        {
            MapProps(args, isMtbl);
        }

        protected virtual void MapProps(ClnblArgs args, bool isMtbl)
        {
            if (args.src != null)
            {
                var opts = new ObjMapOptsMtbl
                {
                    SrcType = args.srcType,
                    TrgType = args.trgType,
                    SrcObj = args.src,
                    TrgObj = this,
                    PropValSetter = (propInfo, propVal) => propInfo.SetValue(this, propVal),
                    TrgIsMtbl = isMtbl
                };

                var immtblOpts = new ObjMapOptsImmtbl(opts);
                args.mapper.MapTarget(immtblOpts);
            }
        }
    }

    public abstract class CloneableObjectImmtblBase : CloneableObjectBase, ICloneableObject
    {
        public CloneableObjectImmtblBase(ClnblArgs args) : base(args, false)
        {
        }

        protected CloneableObjectImmtblBase(
            ICloneableMapper mapper,
            ICloneableObject src) : this(
                new ClnblArgs(mapper, src))
        {
        }
    }

    public abstract class CloneableObjectMtblBase : CloneableObjectBase, ICloneableObject
    {
        public CloneableObjectMtblBase()
        {
        }

        public CloneableObjectMtblBase(ClnblArgs args) : base(args, true)
        {
        }

        protected CloneableObjectMtblBase(
            ICloneableMapper mapper,
            ICloneableObject src) : this(
                new ClnblArgs(mapper, src))
        {
        }
    }

    public readonly struct ClnblArgs
    {
        public readonly ICloneableMapper mapper;
        public readonly ICloneableObject src;
        public readonly Type srcType;
        public readonly Type trgType;

        public ClnblArgs(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type srcType = null,
            Type trgType = null)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.src = src ?? throw new ArgumentNullException(nameof(src));
            this.srcType = srcType;
            this.trgType = trgType;
        }
    }


    /* public abstract class CloneableBaseAttribute : Attribute
    {
        public CloneableBaseAttribute(
            Type type,
            Type clnblTypeFactoryType)
        {
            Type = type;
            ClnblTypeFactoryType = clnblTypeFactoryType;
        }

        public Type Type { get; }
        public Type ClnblTypeFactoryType { get; }
    }

    public class CloneableAttribute : CloneableBaseAttribute
    {
        public CloneableAttribute(
            Type type,
            Type clnblTypeFactoryType) : base(
                type,
                clnblTypeFactoryType)
        {
        }
    }

    public class CloneableImmtblAttribute : CloneableBaseAttribute
    {
        public CloneableImmtblAttribute(Type type,
            Type clnblTypeFactoryType) : base(
                type,
                clnblTypeFactoryType)
        {
        }
    }

    public class CloneableMtblAttribute : CloneableBaseAttribute
    {
        public CloneableMtblAttribute(Type type,
            Type clnblTypeFactoryType) : base(
                type,
                clnblTypeFactoryType)
        {
        }
    }

    public abstract class CloneableTypeFactoryBase
    {
        public abstract Type GetType(Type trgPropType);
    } */
}
