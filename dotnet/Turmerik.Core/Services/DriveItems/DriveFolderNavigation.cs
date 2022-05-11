using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class DriveFolderNavigation
    {
        public DriveFolderNavigation()
        {
        }

        public DriveFolderNavigation(DriveFolderNavigation src)
        {
            FolderId = src.FolderId;
            SubFolderName = src.SubFolderName;
        }

        public bool? NavigateToParent { get; set; }
        public string FolderId { get; set; }
        public string SubFolderName { get; set; }
    }
}
