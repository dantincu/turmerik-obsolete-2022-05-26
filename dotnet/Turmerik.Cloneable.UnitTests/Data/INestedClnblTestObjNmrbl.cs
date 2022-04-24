using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedClnblTestObjNmrbl : INestedClnblNmrbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>
    {
    }

    public class NestedClnblTestObjNmrbl : NestedClnblNmrbl<IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>, INestedClnblTestObjNmrbl
    {
        public NestedClnblTestObjNmrbl()
        {
        }

        public NestedClnblTestObjNmrbl(ReadOnlyCollection<ClnblTestObjImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedClnblTestObjNmrbl(ReadOnlyCollection<ClnblTestObjImmtbl> immtbl, List<ClnblTestObjMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
