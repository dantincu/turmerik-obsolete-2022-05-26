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
        public LocalSessionData(
            ILocalSessionData data,
            ReadOnlyCollection<byte> publicKey)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
        }

        public ILocalSessionData Data { get; }
        public ReadOnlyCollection<byte> PublicKey { get; }
    }

    public class ExtendedLocalSessionData : LocalSessionData
    {
        public ExtendedLocalSessionData(
            ILocalSessionData data,
            ReadOnlyCollection<byte> publicKey,
            ReadOnlyCollection<byte> privateKey) : base(
                data,
                publicKey)
        {
            PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(privateKey));
        }

        public ReadOnlyCollection<byte> PrivateKey { get; }
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
