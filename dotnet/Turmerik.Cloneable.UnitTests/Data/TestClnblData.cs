using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public class TestObj
    {
        public TestObj(int key, int value)
        {
            Key = key;
            Value = value;
        }

        public int Key { get; }
        public int Value { get; }
    }

    public interface IMockClnblTestObj : ICloneableObject
    {
        int Key { get; }
        int Value { get; }
    }

    public class MockClnblTestObjImmtbl : IMockClnblTestObj
    {
        public MockClnblTestObjImmtbl(IMockClnblTestObj src)
        {
            Key = src.Key;
            Value = src.Value;
        }

        public int Key { get; }
        public int Value { get; }
    }

    public class MockClnblTestObjMtbl : IMockClnblTestObj
    {
        public MockClnblTestObjMtbl()
        {
        }

        public MockClnblTestObjMtbl(IMockClnblTestObj src)
        {
            Key = src.Key;
            Value = src.Value;
        }

        public int Key { get; }
        public int Value { get; }
    }
}
