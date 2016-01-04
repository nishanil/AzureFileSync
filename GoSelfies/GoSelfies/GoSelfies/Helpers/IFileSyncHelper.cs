using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;

namespace GoSelfies
{
    /// <summary>
    /// This is a helper to workaround the problem of not having the support of 
    /// Uploading and Downloading Files in IMobileServiceSyncTable Extensions inside PCL
    /// </summary>
    public interface IFileSyncHelper
    {
        /// https://github.com/Azure/azure-mobile-apps-net-files-client/blob/master/src/Xamarin.Shared/MobileServiceTableExtensions.cs
        /// The above class is not referenced in PCL due to the lack of Files support. 
        /// It can be easily worked around to fit into Xamarin.Forms PCL project with dependency injection
        /// Refer the implementation in Platform Projects

        Task UploadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string filePath);
        Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string targetPath);

        IMobileServiceFileDataSource GetMobileServiceDataSource(string filePath);
    } 
}
