using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Data.Cloneable
{
    public interface IClonnerFactory
    {
        IClonnerComponent<TClnbl, TImmtbl, TMtbl> GetClonner<TClnbl, TImmtbl, TMtbl>(
            ICloneableMapper mapper)
            where TClnbl : ICloneableObject
            where TImmtbl : class, TClnbl
            where TMtbl : class, TClnbl;
    }

    public class ClonnerFactory : IClonnerFactory
    {
        public IClonnerComponent<TClnbl, TImmtbl, TMtbl> GetClonner<TClnbl, TImmtbl, TMtbl>(
            ICloneableMapper mapper)
            where TClnbl : ICloneableObject
            where TImmtbl : class, TClnbl
            where TMtbl : class, TClnbl
        {
            var clonner = new ClonnerComponent<TClnbl, TImmtbl, TMtbl>(mapper);
            return clonner;
        }
    }
}
