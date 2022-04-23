using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable.Nested.Clnbl;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface INestedMockClnblTestObjDictnr<TKey> : INestedClnblDictnr<TKey, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>
    {
    }

    public class NestedMockClnblTestObjDictnr<TKey> : NestedClnblDictnr<TKey, IMockClnblTestObj, MockClnblTestObjImmtbl, MockClnblTestObjMtbl>
    {
        public NestedMockClnblTestObjDictnr()
        {
        }

        public NestedMockClnblTestObjDictnr(ReadOnlyDictionary<TKey, MockClnblTestObjImmtbl> immtbl) : base(immtbl)
        {
        }

        public NestedMockClnblTestObjDictnr(ReadOnlyDictionary<TKey, MockClnblTestObjImmtbl> immtbl, Dictionary<TKey, MockClnblTestObjMtbl> mtbl) : base(immtbl, mtbl)
        {
        }
    }
}
