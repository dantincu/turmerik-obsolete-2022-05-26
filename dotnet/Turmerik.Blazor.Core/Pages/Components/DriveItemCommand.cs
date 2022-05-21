using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Cloneable;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public interface IDriveItemCommand : ICloneableObject
    {
        string CommandText { get; }
        Func<MouseEventArgs, Task> Action { get; }
    }

    public class DriveItemCommandImmtbl : CloneableObjectImmtblBase, IDriveItemCommand
    {
        public DriveItemCommandImmtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemCommandImmtbl(ICloneableMapper mapper, IDriveItemCommand src) : base(mapper, src)
        {
        }

        public string CommandText { get; protected set; }
        public Func<MouseEventArgs, Task> Action { get; protected set; }
    }

    public class DriveItemCommandMtbl : CloneableObjectMtblBase, IDriveItemCommand
    {
        public DriveItemCommandMtbl()
        {
        }

        public DriveItemCommandMtbl(ClnblArgs args) : base(args)
        {
        }

        public DriveItemCommandMtbl(ICloneableMapper mapper, IDriveItemCommand src) : base(mapper, src)
        {
        }

        public string CommandText { get; set; }
        public Func<MouseEventArgs, Task> Action { get; set; }
    }
}
