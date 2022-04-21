using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable
{
    public interface IClonnerComponent
    {
        object Clone(Type trgType, object srcObj);
    }

    public interface IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : TClnbl
        where TMtbl : TClnbl
    {
        TImmtbl ToImmtbl(TClnbl srcObj);
        TMtbl ToMtbl(TClnbl srcObj);
    }

    public class ClonnerComponent : IClonnerComponent
    {
        public object Clone(Type trgType, object srcObj)
        {
            object trgObj = null;

            if (srcObj != null)
            {
                trgObj = Activator.CreateInstance(trgType, srcObj);
            }

            return trgObj;
        }
    }

    public class ClonnerComponent<TClnbl, TImmtbl, TMtbl> : ClonnerComponent, IClonnerComponent<TClnbl, TImmtbl, TMtbl>
        where TClnbl : ICloneableObject
        where TImmtbl : class, TClnbl
        where TMtbl : class, TClnbl
    {
        public TImmtbl ToImmtbl(TClnbl srcObj)
        {
            TImmtbl trgObj = Clone(
                typeof(TImmtbl), srcObj) as TImmtbl;

            return trgObj;
        }

        public TMtbl ToMtbl(TClnbl srcObj)
        {
            TMtbl trgObj = Clone(
                typeof(TMtbl), srcObj) as TMtbl;

            return trgObj;
        }
    }
}
