using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class TabPageHistory
    {
        public TabPageHistory()
        {
        }

        public TabPageHistory(TabPageHistory src) : this()
        {
            BackHistory = src.BackHistory;
            ForwardHistory = src.ForwardHistory;
        }

        public string InitialId { get; set; }
        public List<DriveFolderNavigation> BackHistory { get; set; }
        public List<DriveFolderNavigation> ForwardHistory { get; set; }
    }
}
