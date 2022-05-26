using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class ModalIds
    {
        public const string CURRENTLY_OPEN_DRIVE_FOLDER_OPTIONS = "currently-open-folder-options-modal";
        public const string DRIVE_FOLDER_OPTIONS = "drive-folder-options-modal";
        public const string DRIVE_ITEM_OPTIONS = "drive-item-options-modal";

        public const string RENAME_CURRENTLY_OPEN_FOLDER = "rename-current-currently-open-folder-modal";
        public const string RENAME_SELECTED_FOLDER = "rename-selected-folder-modal";
        public const string RENAME_SELECTED_FILE = "rename-selected-file-modal";

        public const string CREATE_NEW_FOLDER_IN_CURRENT = "create-new-folder-in-current-modal";
        public const string CREATE_NEW_FILE_IN_CURRENT = "create-new-file-in-current-modal";
        public const string CREATE_NEW_FOLDER_IN_SELECTED = "create-new-folder-in-selected-modal";
        public const string CREATE_NEW_FILE_IN_SELECTED = "create-new-file-in-selected-modal";
    }
}
