using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Cloneable
{
    public interface IClonnerComponent
    {
        object Clone(Type trgType, object srcObj, Type srcType = null);
    }

    public interface IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
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

        public object Clone(Type trgType, object srcObj, Type srcType = null)
        {
            object trgObj = null;

            if (srcObj != null)
            {
                srcType = srcType ?? srcObj.GetType();

                var args = new ClnblArgs(
                    Mapper,
                    (ICloneableObject)srcObj,
                    srcType,
                    trgType);

                trgObj = Activator.CreateInstance(trgType, args);
            }

            return trgObj;
        }
    }

    public class ClonnerComponent<TClnbl, TImmtbl, TMtbl> : ClonnerComponent, IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        public ClonnerComponent(ICloneableMapper mapper) : base(mapper)
        {
        }

        public TImmtbl ToImmtbl(TClnbl srcObj, Type srcType = null)
        {
            var trgObj = CloneCore<TImmtbl>(
                () => Clone(
                    typeof(TImmtbl),
                    srcObj,
                    srcType ?? srcObj?.GetType()));

            return trgObj;
        }

        public TMtbl ToMtbl(TClnbl srcObj, Type srcType = null)
        {
            var trgObj = CloneCore<TMtbl>(
                () => Clone(
                    typeof(TMtbl),
                    srcObj,
                    srcType ?? srcObj?.GetType()));

            return trgObj;
        }

        public TImmtbl ToImmtbl(TMtbl srcObj)
        {
            var trgObj = CloneCore<TImmtbl>(
                () => Clone(
                    typeof(TImmtbl),
                    srcObj,
                    typeof(TMtbl)));

            return trgObj;
        }

        public TMtbl ToMtbl(TImmtbl srcObj)
        {
            var trgObj = CloneCore<TMtbl>(
                () => Clone(
                    typeof(TMtbl),
                    srcObj,
                    typeof(TImmtbl)));

            return trgObj;
        }

        private TOutput CloneCore<TOutput>(
            Func<object> converter)
        {
            var trgObj = converter();

            TOutput output;

            if (trgObj != null)
            {
                output = (TOutput)trgObj;
            }
            else
            {
                output = default;
            }

            return output;
        }
    }
}
