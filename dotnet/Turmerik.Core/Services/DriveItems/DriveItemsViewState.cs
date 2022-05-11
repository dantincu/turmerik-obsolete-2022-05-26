using System;
using System.Collections.Generic;
using System.Text;

namespace Turmerik.Core.Services.DriveItems
{
    public class DriveItemsViewState
    {
        public DriveItemsViewState()
        {
        }

        public DriveItemsViewState(DriveItemsViewState src)
        {
            Header = src.Header;
            CurrentlyOpenFolder = src.CurrentlyOpenFolder;
            CurrentAddress = src.CurrentAddress;
            GoBackButtonEnabled = src.GoBackButtonEnabled;
            GoForwardButtonEnabled = src.GoForwardButtonEnabled;
            GoUpButtonEnabled = src.GoUpButtonEnabled;
        }

        public TabHeaderViewState Header { get; set; }
        public DriveFolder CurrentlyOpenFolder { get; set; }
        public string CurrentAddress { get; set; }
        public bool GoBackButtonEnabled { get; set; }
        public bool GoForwardButtonEnabled { get; set; }
        public bool GoUpButtonEnabled { get; set; }
    }
}
