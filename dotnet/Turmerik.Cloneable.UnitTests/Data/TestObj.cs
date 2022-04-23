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
}
