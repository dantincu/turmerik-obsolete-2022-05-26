using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Cloneable.UnitTests.Data
{
    public interface IClnblTestObj : ICloneableObject
    {
        int Key { get; }
        int Value { get; }
    }

    public class ClnblTestObjImmtbl : CloneableObjectImmtblBase, IClnblTestObj
    {
        public ClnblTestObjImmtbl(ICloneableMapper mapper, IMockClnblTestObj src) : base(mapper, src)
        {
        }

        public int Key { get; }
        public int Value { get; }
    }

    public class ClnblTestObjMtbl : CloneableObjectMtblBase, IClnblTestObj
    {
        public ClnblTestObjMtbl()
        {
        }

        public ClnblTestObjMtbl(ICloneableMapper mapper, IMockClnblTestObj src) : base(mapper, src)
        {
        }

        public int Key { get; set; }
        public int Value { get; set; }
    }
}
