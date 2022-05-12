using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services
{
    public class TabHeaderViewState
    {
        public List<TabPageHead> TabPageHeads { get; set ; }
    }

    public class TabPageHead
    {
        public Guid Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? IsCurrent { get; set; }
    }
}
