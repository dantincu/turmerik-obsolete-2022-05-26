﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.AspNetCore.Infrastructure;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.Blazor.Core.Pages.Components
{
    public partial class FilesPageComponentBase
    {
        protected async Task OpenCurrentFolderInOSFileExplorerFromModalAsync(MouseEventArgs args)
        {
            await CloseCurrentFolderOptionsModalAsync();
        }

        protected async Task OpenCurrentFolderInOSFileExplorerFromFileModalAsync(MouseEventArgs args)
        {
            await CloseDriveItemOptionsModalAsync();
        }

        protected async Task OpenCurrentFolderInTrmrkFileExplorerFromModalAsync(MouseEventArgs args)
        {
            await CloseCurrentFolderOptionsModalAsync();
        }

        protected async Task OpenSelectedFolderInOSFileExplorerFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveFolderItemOptionsModalAsync();
        }

        protected async Task OpenSelectedFolderInTrmrkFileExplorerFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveFolderItemOptionsModalAsync();
        }

        protected async Task OpenSelectedFolderFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveFolderItemOptionsModalAsync();
            await OpenDriveFolderAsync(SelectedDriveFolder);
        }

        protected async Task OpenSelectedDriveItemInOSDefaultAppFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveItemOptionsModalAsync();
        }

        protected async Task OpenSelectedDriveItemInOSDefaultTextEditorFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveItemOptionsModalAsync();
            await OpenDriveItemAsync(SelectedDriveItem);
        }

        protected async Task OpenSelectedDriveItemInTrmrkTextEditorFromModalAsync(MouseEventArgs args)
        {
            await CloseDriveItemOptionsModalAsync();
        }

        protected async Task BeginCreateNewFolderInCurrentFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewTextFileInCurrentFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewTextFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficeWordFileInCurrentFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficeExcelFileInCurrentFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficePowerPointFileInCurrentFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficeWordFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficeExcelFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCreateNewMsOfficePowerPointFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginDeleteCurrentlyOpenFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginDeleteFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginDeleteFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginRenameCurrentlyOpenFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginRenameFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginRenameFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginMoveCurrentlyOpenFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginMoveFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginMoveFileFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCopyCurrentlyOpenFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCopyFolderFromModalAsync(MouseEventArgs args)
        {

        }

        protected async Task BeginCopyFileFromModalAsync(MouseEventArgs args)
        {

        }
    }
}