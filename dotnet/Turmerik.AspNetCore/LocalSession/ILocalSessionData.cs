using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;
using Turmerik.Core.Cloneable.Nested;

namespace Turmerik.AspNetCore.LocalSession
{
    public class LocalSessionData
    {
        public LocalSessionData(ILocalSessionData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public ILocalSessionData Data { get; }
    }

    public class ExtendedLocalSessionData : LocalSessionData
    {
        public ExtendedLocalSessionData(ILocalSessionData data) : base(data)
        {
        }
    }

    public interface ILocalSessionData : ICloneableObject
    {
        Guid LocalSessionGuid { get; }
    }

    public class LocalSessionDataImmtbl : CloneableObjectImmtblBase, ILocalSessionData
    {
        public LocalSessionDataImmtbl(ClnblArgs args) : base(args)
        {
        }

        public LocalSessionDataImmtbl(ICloneableMapper mapper, ILocalSessionData src) : base(mapper, src)
        {
        }

        public Guid LocalSessionGuid { get; protected set; }
    }

    public class LocalSessionDataMtbl : CloneableObjectMtblBase, ILocalSessionData
    {
        public LocalSessionDataMtbl()
        {
        }

        public LocalSessionDataMtbl(ClnblArgs args) : base(args)
        {
        }

        public LocalSessionDataMtbl(ICloneableMapper mapper, ILocalSessionData src) : base(mapper, src)
        {
        }

        public Guid LocalSessionGuid { get; set; }
    }
}
