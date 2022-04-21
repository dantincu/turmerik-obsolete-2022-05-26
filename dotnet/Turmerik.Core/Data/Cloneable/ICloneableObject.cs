using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Helpers;

namespace Turmerik.Core.Data.Cloneable
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
            Type trgType = null,
            Type srcType = null,
            Type intfType = null)
        {
            MapProps(mapper, src);
        }

        protected abstract bool IsMtbl { get; }

        protected virtual void MapProps(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type trgType = null,
            Type srcType = null,
            Type intfType = null)
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
                    TrgIsMtbl = IsMtbl
                };

                var immtblOpts = new ObjMapOptsImmtbl(opts);
                mapper.MapTarget(immtblOpts);
            }
        }
    }

    public abstract class CloneableObjectImmtblBase : CloneableObjectBase, ICloneableObject
    {
        protected CloneableObjectImmtblBase(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type trgType = null,
            Type srcType = null,
            Type intfType = null) : base(
                mapper,
                src,
                trgType,
                srcType,
                intfType)
        {
        }

        protected override bool IsMtbl => false;
    }

    public abstract class CloneableObjectMtblBase : CloneableObjectBase, ICloneableObject
    {
        protected CloneableObjectMtblBase()
        {
        }

        protected CloneableObjectMtblBase(
            ICloneableMapper mapper,
            ICloneableObject src,
            Type trgType = null,
            Type srcType = null,
            Type intfType = null) : base(
                mapper,
                src,
                trgType,
                srcType,
                intfType)
        {
        }

        protected override bool IsMtbl => true;
    }
}
