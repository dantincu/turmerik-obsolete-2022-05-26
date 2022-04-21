using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable
{
    public interface IClonnerComponent
    {
        object Clone(Type trgType, object srcObj, Type srcType = null, Type intfType = null);
    }

    public interface IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        TImmtbl ToImmtbl(TClnbl srcObj, Type srcType = null);
        TMtbl ToMtbl(TClnbl srcObj, Type srcType = null);
        TImmtbl ToImmtbl(TMtbl srcObj);
        TMtbl ToMtbl(TImmtbl srcObj);
    }

    public class ClonnerComponent : IClonnerComponent
    {
        protected readonly ICloneableMapper Mapper;

        public ClonnerComponent(ICloneableMapper mapper)
        {
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public object Clone(Type trgType, object srcObj, Type srcType = null, Type intfType = null)
        {
            object trgObj = null;

            if (srcObj != null)
            {
                srcType = srcType ?? srcObj.GetType();

                trgObj = Activator.CreateInstance(
                    trgType,
                    Mapper,
                    srcObj,
                    srcType,
                    intfType);
            }

            return trgObj;
        }
    }

    public class ClonnerComponent<TClnbl, TImmtbl, TMtbl> : ClonnerComponent, IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public ClonnerComponent(ICloneableMapper mapper) : base(mapper)
        {
        }

        public TImmtbl ToImmtbl(TClnbl srcObj, Type srcType = null)
        {
            srcType = srcType ?? srcObj?.GetType();

            TImmtbl trgObj = Clone(
                typeof(TImmtbl), srcObj, srcType, typeof(TClnbl)) as TImmtbl;

            return trgObj;
        }

        public TMtbl ToMtbl(TClnbl srcObj, Type srcType = null)
        {
            srcType = srcType ?? srcObj?.GetType();

            TMtbl trgObj = Clone(
                typeof(TMtbl), srcObj, srcType, typeof(TClnbl)) as TMtbl;

            return trgObj;
        }

        public TImmtbl ToImmtbl(TMtbl srcObj)
        {
            TImmtbl trgObj = Clone(
                typeof(TImmtbl), srcObj) as TImmtbl;

            return trgObj;
        }

        public TMtbl ToMtbl(TImmtbl srcObj)
        {
            TMtbl trgObj = Clone(
                typeof(TMtbl), srcObj) as TMtbl;

            return trgObj;
        }
    }
}
