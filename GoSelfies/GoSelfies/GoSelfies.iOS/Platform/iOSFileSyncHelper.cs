using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;

[assembly: Xamarin.Forms.Dependency(typeof(GoSelfies.iOSFileSyncHelper))]
namespace GoSelfies
{
    // https://github.com/Azure/azure-mobile-apps-net-files-client/blob/master/src/Xamarin.Shared/MobileServiceTableExtensions.cs
    /// The above class is not referenced in PCL due to the lack of Files support. 
    /// This is a workaround

    public class iOSFileSyncHelper : IFileSyncHelper
    {
        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string targetPath)
        {
            await table.DownloadFileAsync(file, targetPath);
        }

        public async Task UploadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string filePath)
        {
            await table.DownloadFileAsync(file, filePath);
        }

        public IMobileServiceFileDataSource GetMobileServiceDataSource(string filePath)
        {
            return new PathMobileServiceFileDataSource(filePath);
        }
    }
}
