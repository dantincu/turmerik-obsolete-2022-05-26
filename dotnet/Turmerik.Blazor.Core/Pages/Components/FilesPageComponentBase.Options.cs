using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Services.DriveItems;
using Turmerik.Core.Helpers;
using System.Reflection;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public partial class FilesPageComponentBase
    {
        private const string OPEN_FOLDER_IN_OS_FILE_EXPLORER = "Open folder in OS File Explorer";
        private const string OPEN_FOLDER_IN_TRMRK_FILE_EXPLORER = "Open folder in Turmerik Local Disk Explorer";
        private const string OPEN_FILE_IN_OS_DEFAULT_APP = "Open file in OS default app";
        private const string OPEN_FILE_IN_OS_DEFAULT_TEXT_EDITOR = "Open file in OS default text editor";
        private const string OPEN_FILE_IN_TRMRK_TEXT_EDITOR = "Open file in Turmerik Text Editor";
        private const string CREATE_NEW_FOLDER = "Create new folder";
        private const string CREATE_NEW_TEXT_FILE = "Create new text file";
        private const string CREATE_NEW_MS_OFFICE_WORD_FILE = "Create new MS Office Word file";
        private const string CREATE_NEW_MS_OFFICE_EXCEL_FILE = "Create new MS Office Excel file";
        private const string CREATE_NEW_MS_OFFICE_POWERPOINT_FILE = "Create new MS Office PowerPoint file";
        private const string RENAME_FOLDER = "Rename folder";
        private const string DELETE_FOLDER = "Delete folder";
        private const string MOVE_FOLDER = "Move folder";
        private const string COPY_FOLDER = "Copy folder";
        private const string RENAME_FILE = "Rename file";
        private const string DELETE_FILE = "Delete file";
        private const string MOVE_FILE = "Move file";
        private const string COPY_FILE = "Copy file";
        private const string OPEN = "Open";
        private const string OPEN_FOLDER_IN_NEW_TAB = "Open folder in new tab";

        protected virtual List<List<IDriveItemCommand>> GetCurrentlyOpenDriveFolderCommandsMx()
        {
            var commandsMx = new List<List<IDriveItemCommand>>();

            var firstMtblList = new List<DriveItemCommandMtbl>
            {
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_NEW_TAB,
                    OpenCurrentFolderInNewTabFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_OS_FILE_EXPLORER,
                    OpenCurrentFolderInOSFileExplorerFromModalAsync)
            };

            if (!IsLocalDiskExplorer)
            {
                firstMtblList.Add(
                    GetDriveItemCommandMtbl(OPEN_FOLDER_IN_TRMRK_FILE_EXPLORER,
                        OpenCurrentFolderInTrmrkFileExplorerFromModalAsync));
            }

            AddDriveItemCommandsList(
                commandsMx,
                firstMtblList.ToArray());

            if (ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.IsRootFolder != true)
            {
                if (ServiceArgs.FolderIdentifier.DriveItemType == null)
                {
                    AddDriveItemCommandsList(
                        commandsMx,
                        GetDriveItemCommandMtbl(RENAME_FOLDER,
                            BeginRenameCurrentlyOpenFolderFromModalAsync),
                        GetDriveItemCommandMtbl(DELETE_FOLDER,
                            BeginDeleteCurrentlyOpenFolderFromModalAsync),
                        GetDriveItemCommandMtbl(COPY_FOLDER,
                            BeginCopyCurrentlyOpenFolderFromModalAsync),
                        GetDriveItemCommandMtbl(MOVE_FOLDER,
                            BeginMoveCurrentlyOpenFolderFromModalAsync));

                    AddDriveItemCommandsList(
                        commandsMx,
                        GetDriveItemCommandMtbl(CREATE_NEW_FOLDER,
                            BeginCreateNewFolderInCurrentFromModalAsync),
                        GetDriveItemCommandMtbl(CREATE_NEW_TEXT_FILE,
                            BeginCreateNewTextFileInCurrentFromModalAsync),
                        GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_WORD_FILE,
                            BeginCreateNewMsOfficeWordFileInCurrentFromModalAsync),
                        GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_EXCEL_FILE,
                            BeginCreateNewMsOfficeExcelFileInCurrentFromModalAsync),
                        GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_POWERPOINT_FILE,
                            BeginCreateNewMsOfficePowerPointFileInCurrentFromModalAsync));
                }
            }

            return commandsMx;
        }

        protected virtual List<List<IDriveItemCommand>> GetSelectedDriveFolderCommandsMx()
        {
            var commandsMx = new List<List<IDriveItemCommand>>();

            var firstMtblList = new List<DriveItemCommandMtbl>
            {
                GetDriveItemCommandMtbl(OPEN,
                    OpenSelectedFolderFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_NEW_TAB,
                    OpenSelectedFolderInNewTabFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_OS_FILE_EXPLORER,
                    OpenSelectedFolderInOSFileExplorerFromModalAsync)
            };

            if (!IsLocalDiskExplorer)
            {
                firstMtblList.Add(
                    GetDriveItemCommandMtbl(OPEN_FOLDER_IN_TRMRK_FILE_EXPLORER,
                        OpenSelectedFolderInTrmrkFileExplorerFromModalAsync));
            }

            AddDriveItemCommandsList(
                commandsMx,
                firstMtblList.ToArray());

            if (ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.IsRootFolder != true)
            {
                AddDriveItemCommandsList(
                    commandsMx,
                    GetDriveItemCommandMtbl(RENAME_FOLDER,
                        BeginRenameFolderFromModalAsync),
                    GetDriveItemCommandMtbl(DELETE_FOLDER,
                        BeginDeleteFolderFromModalAsync),
                    GetDriveItemCommandMtbl(COPY_FOLDER,
                        BeginCopyFolderFromModalAsync),
                    GetDriveItemCommandMtbl(MOVE_FOLDER,
                        BeginMoveFolderFromModalAsync));

                AddDriveItemCommandsList(
                    commandsMx,
                    GetDriveItemCommandMtbl(CREATE_NEW_FOLDER,
                        BeginCreateNewFolderFromModalAsync),
                    GetDriveItemCommandMtbl(CREATE_NEW_TEXT_FILE,
                        BeginCreateNewTextFileFromModalAsync),
                    GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_WORD_FILE,
                        BeginCreateNewMsOfficeWordFileFromModalAsync),
                    GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_EXCEL_FILE,
                        BeginCreateNewMsOfficeExcelFileFromModalAsync),
                    GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_POWERPOINT_FILE,
                        BeginCreateNewMsOfficePowerPointFileFromModalAsync));
            }

            return commandsMx;
        }

        protected virtual List<List<IDriveItemCommand>> GetSelectedDriveItemCommandsMx()
        {
            var commandsMx = new List<List<IDriveItemCommand>>();

            var firstMtblList = new List<DriveItemCommandMtbl>
            {
                GetDriveItemCommandMtbl(OPEN,
                    OpenSelectedDriveItemFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FILE_IN_OS_DEFAULT_APP,
                    OpenSelectedDriveItemInOSDefaultAppFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FILE_IN_OS_DEFAULT_TEXT_EDITOR,
                    OpenSelectedDriveItemInOSDefaultTextEditorFromModalAsync),
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_NEW_TAB,
                    OpenCurrentFolderInNewTabFromFileModalAsync),
                GetDriveItemCommandMtbl(OPEN_FOLDER_IN_OS_FILE_EXPLORER,
                    OpenCurrentFolderInOSFileExplorerFromFileModalAsync)
            };

            if (!IsLocalDiskExplorer)
            {
                firstMtblList.AddRange(
                    GetDriveItemCommandMtbl(OPEN_FOLDER_IN_TRMRK_FILE_EXPLORER,
                        OpenSelectedFolderInTrmrkFileExplorerFromModalAsync),
                    GetDriveItemCommandMtbl(OPEN_FILE_IN_TRMRK_TEXT_EDITOR,
                        OpenSelectedDriveItemInTrmrkTextEditorFromModalAsync));
            }

            AddDriveItemCommandsList(
                commandsMx,
                firstMtblList.ToArray());

            AddDriveItemCommandsList(
                commandsMx,
                GetDriveItemCommandMtbl(RENAME_FILE,
                    BeginRenameFileFromModalAsync),
                GetDriveItemCommandMtbl(DELETE_FILE,
                    BeginDeleteFileFromModalAsync),
                GetDriveItemCommandMtbl(COPY_FILE,
                    BeginCopyFileFromModalAsync),
                GetDriveItemCommandMtbl(MOVE_FILE,
                    BeginMoveFileFromModalAsync));

            AddDriveItemCommandsList(
                commandsMx,
                GetDriveItemCommandMtbl(CREATE_NEW_FOLDER,
                    BeginCreateNewFolderInCurrentFromModalAsync),
                GetDriveItemCommandMtbl(CREATE_NEW_TEXT_FILE,
                    BeginCreateNewTextFileInCurrentFromModalAsync),
                GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_WORD_FILE,
                    BeginCreateNewMsOfficeWordFileInCurrentFromModalAsync),
                GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_EXCEL_FILE,
                    BeginCreateNewMsOfficeExcelFileInCurrentFromModalAsync),
                GetDriveItemCommandMtbl(CREATE_NEW_MS_OFFICE_POWERPOINT_FILE,
                    BeginCreateNewMsOfficePowerPointFileInCurrentFromModalAsync));

            return commandsMx;
        }

        protected void AddDriveItemCommandsList(
            List<List<IDriveItemCommand>> commandsMx,
            params DriveItemCommandMtbl[] commandsArr)
        {
            var commandsList = GetDriveItemCommandsList(commandsArr);
            commandsMx.Add(commandsList);
        }

        protected List<IDriveItemCommand> GetDriveItemCommandsList(
            params DriveItemCommandMtbl[] commandsArr)
        {
            var commandsList = commandsArr.Select(
                cmd => new DriveItemCommandImmtbl(
                    Mapper, cmd) as IDriveItemCommand).ToList();

            return commandsList;
        }

        private Task OpenCurrentFolderOptionsModalAsync() => OpenModalAsync(
            ModalIds.CURRENTLY_OPEN_DRIVE_FOLDER_OPTIONS,
            nameof(CurrentlyOpenDriveFolderOptionsModalIsOpen));

        private Task OpenDriveFolderOptionsModalAsync(DriveItem driveFolder) => OpenModalAsync(
            ModalIds.DRIVE_FOLDER_OPTIONS,
            nameof(DriveFolderItemOptionsModalIsOpen),
            () =>
            {
                var identifier = new DriveItemIdentifier
                {
                    Id = driveFolder.Id,
                    Name = driveFolder.Name,
                    ParentId = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id,
                };

                SelectedDriveFolderItem = driveFolder;
                SelectedDriveFolderName = driveFolder.Name;
                SelectedDriveFolderId = DriveFolderService.GetDriveItemId(identifier);
                SelectedDriveFolderAddress = DriveFolderService.GetDriveItemAddress(identifier);
                SelectedDriveFolderUri = DriveFolderService.GetDriveItemUri(identifier);

                StateHasChanged();
            });

        private Task OpenDriveItemOptionsModalAsync(DriveItem driveItem) => OpenModalAsync(
            ModalIds.DRIVE_ITEM_OPTIONS,
            nameof(DriveItemOptionsModalIsOpen),
            () =>
            {
                var identifier = new DriveItemIdentifier
                {
                    Id = driveItem.Id,
                    Name = driveItem.Name,
                    ParentId = ServiceArgs.Data.TabPageItems.CurrentlyOpenFolder.Id,
                };

                SelectedDriveItem = driveItem;
                SelectedDriveItemName = driveItem.Name;
                SelectedDriveItemId = DriveFolderService.GetDriveItemId(identifier);
                SelectedDriveItemAddress = DriveFolderService.GetDriveItemAddress(identifier);
                SelectedDriveItemUri = DriveFolderService.GetDriveItemUri(identifier);

                StateHasChanged();
            });

        private Task CloseCurrentFolderOptionsModalAsync() => CloseModalAsync(
            ModalIds.CURRENTLY_OPEN_DRIVE_FOLDER_OPTIONS,
            nameof(CurrentlyOpenDriveFolderOptionsModalIsOpen));

        private Task CloseDriveFolderItemOptionsModalAsync() => CloseModalAsync(
            ModalIds.DRIVE_FOLDER_OPTIONS,
            nameof(DriveFolderItemOptionsModalIsOpen));

        private Task CloseDriveItemOptionsModalAsync() => CloseModalAsync(
            ModalIds.DRIVE_ITEM_OPTIONS,
            nameof(DriveItemOptionsModalIsOpen));

        private Task CloseRenameCurrentlyOpenFolderModalAsync() => CloseModalAsync(
            ModalIds.RENAME_CURRENTLY_OPEN_FOLDER,
            nameof(RenameCurrentlyOpenFolderModalIsOpen));

        private Task CloseRenameSelectedFolderModalAsync() => CloseModalAsync(
            ModalIds.RENAME_SELECTED_FOLDER,
            nameof(RenameSelectedFolderModalIsOpen));

        private Task CloseRenameSelectedFileModalAsync() => CloseModalAsync(
            ModalIds.RENAME_SELECTED_FILE,
            nameof(RenameSelectedFileModalIsOpen));

        private Task CloseCreateNewFolderInCurrentModalAsync() => CloseModalAsync(
            ModalIds.CREATE_NEW_FOLDER_IN_CURRENT,
            nameof(CreateNewFolderInCurrentModalIsOpen));

        private Task CloseCreateNewFileInCurrentModalAsync() => CloseModalAsync(
            ModalIds.CREATE_NEW_FILE_IN_CURRENT,
            nameof(CreateNewFileInCurrentModalIsOpen));

        private Task CloseCreateNewFolderInSelectedModalAsync() => CloseModalAsync(
            ModalIds.CREATE_NEW_FOLDER_IN_SELECTED,
            nameof(CreateNewFolderInSelectedModalIsOpen));

        private Task CloseCreateNewFileInSelectedModalAsync() => CloseModalAsync(
            ModalIds.CREATE_NEW_FILE_IN_SELECTED,
            nameof(CreateNewFileInSelectedModalIsOpen));

        private async Task AssureAllModalsAreClosedAsync()
        {
            await Task.WhenAll(
                CloseCurrentFolderOptionsModalAsync(),
                CloseDriveFolderItemOptionsModalAsync(),
                CloseDriveItemOptionsModalAsync());
        }

        private DriveItemCommandMtbl GetDriveItemCommandMtbl(
            string commandText,
            Func<MouseEventArgs, Task> action)
        {
            var mtbl = new DriveItemCommandMtbl
            {
                CommandText = commandText,
                Action = async args => await action.InvokeMouseClickAsyncIfLeftBtn(args)
            };

            return mtbl;
        }

        private async Task OpenModalAsync(
            string modalId,
            string propName,
            Action callback = null)
        {
            var propInfo = GetPropInfo(propName);
            bool condition = (bool)propInfo.GetValue(this);

            if (condition)
            {
                propInfo.SetValue(this, true);
                callback?.Invoke();

                await JSRuntime.InvokeVoidAsync(
                    TrmrkJsH.Get(TrmrkJsH.OpenModal),
                    modalId);
            }
        }

        private async Task OpenModalAsync(
            string modalId,
            string propName,
            Func<Task> callback)
        {
            var propInfo = GetPropInfo(propName);
            bool condition = (bool)propInfo.GetValue(this);

            if (condition)
            {
                propInfo.SetValue(this, true);
                await callback();

                await JSRuntime.InvokeVoidAsync(
                    TrmrkJsH.Get(TrmrkJsH.OpenModal),
                    modalId);
            }
        }

        private async Task CloseModalAsync(
            string modalId,
            string propName)
        {
            var propInfo = GetPropInfo(propName);
            bool condition = (bool)propInfo.GetValue(this);

            if (condition)
            {
                await JSRuntime.InvokeVoidAsync(
                    TrmrkJsH.Get(TrmrkJsH.CloseModal),
                    modalId);

                propInfo.SetValue(this, false);
            }
        }

        private PropertyInfo GetPropInfo(string propName)
        {
            var allProps = ThisTypeWrapper.InstFamGetProps.Value;

            var propInfo = allProps.Single(
                pi => pi.Name == propName).Data;

            return propInfo;
        }
    }
}
