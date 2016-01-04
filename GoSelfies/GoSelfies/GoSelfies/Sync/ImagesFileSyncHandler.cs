using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Metadata;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace GoSelfies
{
    public class ImagesFileSyncHandler<T> : IFileSyncHandler where T : class
    {
        private readonly IFileHelper fileHelper;
        private readonly IFileSyncHelper fileSyncHelper;
        private readonly IMobileServiceSyncTable<T> todoTable;
        public ImagesFileSyncHandler(IMobileServiceSyncTable<T> table)
        {
            fileHelper = DependencyService.Get<IFileHelper>();
            fileSyncHelper = DependencyService.Get<IFileSyncHelper>();
            todoTable = table;
        }
        public Task<IMobileServiceFileDataSource> GetDataSource(MobileServiceFileMetadata metadata)
        {
            IMobileServiceFileDataSource source = fileSyncHelper.GetMobileServiceDataSource(fileHelper.GetLocalFilePath(metadata.ParentDataItemId, metadata.FileName));

            return Task.FromResult(source);
        }

        public async Task ProcessFileSynchronizationAction(MobileServiceFile file, FileSynchronizationAction action)
        {
            if (action == FileSynchronizationAction.Delete)
            {
                fileHelper.DeleteLocalFile(file);
            }
            else // Create or update. We're aggressively downloading all files.
            {
                await this.fileSyncHelper.DownloadFileAsync(todoTable, file, fileHelper.GetLocalFilePath(file.ParentId, file.Name));
            }
        }
    }
}
