using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class DriveItemsTreeState
    {
        public DriveItemsTreeState()
        {
        }

        public DriveItemsTreeState(DriveItemsTreeState src)
        {
            RootFoldersList = src.RootFoldersList;
        }

        public List<DriveFolder> RootFoldersList { get; set; }
    }
}
