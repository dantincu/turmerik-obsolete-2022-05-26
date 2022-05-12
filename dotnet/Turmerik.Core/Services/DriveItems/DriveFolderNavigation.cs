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
            Up = src.Up;
            Id = src.Id;
            Name = src.Name;
        }

        public bool? Up { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
