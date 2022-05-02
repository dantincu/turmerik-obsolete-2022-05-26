using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.FileSystem
{
    public interface IFsPathNormalizerResult : ICloneableObject
    {
        string NormalizedPath { get; }
        bool IsValid { get; }
        bool IsRooted { get; }
        bool? IsUnixStyle { get; }
        bool? IsAbsUri { get; }
    }

    public class FsPathNormalizerResultImmtbl : CloneableObjectImmtblBase, IFsPathNormalizerResult
    {
        public FsPathNormalizerResultImmtbl(ClnblArgs args) : base(args)
        {
        }

        public FsPathNormalizerResultImmtbl(ICloneableMapper mapper, IFsPathNormalizerResult src) : base(mapper, src)
        {
        }

        public string NormalizedPath { get; protected set; }
        public bool IsValid { get; protected set; }
        public bool IsRooted { get; protected set; }
        public bool? IsUnixStyle { get; protected set; }
        public bool? IsAbsUri { get; protected set; }
    }

    public class FsPathNormalizerResultMtbl : CloneableObjectMtblBase, IFsPathNormalizerResult
    {
        public FsPathNormalizerResultMtbl()
        {
        }

        public FsPathNormalizerResultMtbl(ClnblArgs args) : base(args)
        {
        }

        public FsPathNormalizerResultMtbl(ICloneableMapper mapper, IFsPathNormalizerResult src) : base(mapper, src)
        {
        }

        public string NormalizedPath { get; set; }
        public bool IsValid { get; set; }
        public bool IsRooted { get; set; }
        public bool? IsUnixStyle { get; set; }
        public bool? IsAbsUri { get; set; }
    }
}
