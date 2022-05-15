using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.Core.Services.DriveItems;

namespace Turmerik.AspNetCore.Services.DriveItems
{
    public abstract partial class DriveExplorerServiceBase
    {
        protected abstract Task CreateNewFolderCoreAsync(string parentFolderId, string newFolderName);
        protected abstract Task CreateNewTextFileCoreAsync(string parentFolderId, string newTextFileName, string text = null);
        protected abstract Task CreateNewMsOfficeFileCoreAsync(string parentFolderId, string newMsOfficeFileName);
        protected abstract Task DeleteFolderCoreAsync(string folderId, bool recursive);
        protected abstract Task DeleteFileCoreAsync(string fileId);
        protected abstract Task RenameFolderCoreAsync(string folderId, string newFolderName);
        protected abstract Task RenameFileCoreAsync(string fileId, string newFileName);
        protected abstract Task MoveFolderCoreAsync(string folderId, string newParentFolderId, string newFolderName);
        protected abstract Task MoveFileCoreAsync(string fileId, string newParentFolderId, string newFileName);

        private async Task CreateNewFolderAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task CreateNewTextFileAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task CreateNewMsOfficeFileAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task DeleteFolderAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task DeleteFileAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task RenameFolderAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task RenameFileAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task MoveFolderAsync(DriveExplorerServiceArgs args)
        {

        }

        private async Task MoveFileAsync(DriveExplorerServiceArgs args)
        {

        }
    }
}
