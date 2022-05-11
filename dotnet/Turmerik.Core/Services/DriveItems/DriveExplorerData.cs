using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class DriveExplorerData
    {
        public DriveExplorerData()
        {
        }

        public DriveExplorerData(DriveExplorerData src)
        {
            TabPageItems = src.TabPageItems;
        }

        public DriveItemsViewState TabPageItems { get; set; }
    }
}
