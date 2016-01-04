using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;

[assembly: Xamarin.Forms.Dependency(typeof(GoSelfies.AndroidFileSyncHelper))]
namespace GoSelfies
{
    // https://github.com/Azure/azure-mobile-apps-net-files-client/blob/master/src/Xamarin.Shared/MobileServiceTableExtensions.cs
    /// The above class is not referenced in PCL due to the lack of Files support. 
    /// This is a workaround

    public class AndroidFileSyncHelper : IFileSyncHelper
    {
        public async Task DownloadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string targetPath)
        {
            await table.DownloadFileAsync<T>(file, targetPath);
        }

        public IMobileServiceFileDataSource GetMobileServiceDataSource(string filePath)
        {
            return new PathMobileServiceFileDataSource(filePath);
        }

        public async Task UploadFileAsync<T>(IMobileServiceSyncTable<T> table, MobileServiceFile file, string filePath)
        {
            await table.DownloadFileAsync<T>(file, filePath);
        }
    }
}