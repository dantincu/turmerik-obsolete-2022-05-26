using System;
using System.Collections.Generic;
using System.Text;
using Turmerik.AspNetCore.Services.DriveItems;

namespace Turmerik.Core.Services.DriveItems
{
    public class CurrentDriveItemsTuple
    {
        public CurrentDriveItemsTuple(IReadOnlyCollection<IDriveItemCore> currentItems, IDriveItemCore currentlyOpenItem)
        {
            CurrentItems = currentItems ?? throw new ArgumentNullException(nameof(currentItems));
            CurrentlyOpenItem = currentlyOpenItem ?? throw new ArgumentNullException(nameof(currentlyOpenItem));
        }

        public IReadOnlyCollection<IDriveItemCore> CurrentItems { get; }
        public IDriveItemCore CurrentlyOpenItem { get; }
    }
}
