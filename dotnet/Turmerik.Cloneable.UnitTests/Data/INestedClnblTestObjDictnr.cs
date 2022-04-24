using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedClnblTestObjDictnr<TKey> : INestedClnblDictnr<TKey, IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>
    {
    }

    public class NestedClnblTestObjDictnr<TKey> : NestedClnblDictnr<TKey, IClnblTestObj, ClnblTestObjImmtbl, ClnblTestObjMtbl>, INestedClnblTestObjDictnr<TKey>
    {
        public NestedClnblTestObjDictnr()
        {
        }

        public NestedClnblTestObjDictnr(ReadOnlyDictionary<TKey, ClnblTestObjImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedClnblTestObjDictnr(ReadOnlyDictionary<TKey, ClnblTestObjImmtbl> immtbl, Dictionary<TKey, ClnblTestObjMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
