using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.Core.Cloneable;

namespace Turmerik.Core.Services.DriveItems
{
    public interface ITabViewState : ICloneableObject
    {
        DriveFolder CurrentlyOpenFolder { get; }
        DriveItemIdentifier CurrentIdentifier { get; }
        string CurrentAddress { get; }
        Guid? CurrentlyOpenUuid { get; }
        TabPageHeadsList TabPageHeadsList { get; }
    }

    public class TabViewStateImmtbl : CloneableObjectImmtblBase, ITabViewState
    {
        public TabViewStateImmtbl(ClnblArgs args) : base(args)
        {
        }

        public TabViewStateImmtbl(ICloneableMapper mapper, ITabViewState src) : base(mapper, src)
        {
        }

        public DriveFolder CurrentlyOpenFolder { get; protected set; }
        public DriveItemIdentifier CurrentIdentifier { get; protected set; }
        public string CurrentAddress { get; protected set; }
        public Guid? CurrentlyOpenUuid { get; protected set; }
        public TabPageHeadsList TabPageHeadsList { get; protected set; }
    }

    public class TabViewStateMtbl : CloneableObjectMtblBase, ITabViewState
    {
        public TabViewStateMtbl()
        {
        }

        public TabViewStateMtbl(ClnblArgs args) : base(args)
        {
        }

        public TabViewStateMtbl(ICloneableMapper mapper, ITabViewState src) : base(mapper, src)
        {
        }

        public DriveFolder CurrentlyOpenFolder { get; set; }
        public DriveItemIdentifier CurrentIdentifier { get; set; }
        public string CurrentAddress { get; set; }
        public Guid? CurrentlyOpenUuid { get; set; }
        public TabPageHeadsList TabPageHeadsList { get; set; }
    }
}
