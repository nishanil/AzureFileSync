using GoMonkeys.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files;
using Microsoft.WindowsAzure.MobileServices.Sync;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace GoMonkeys
{

    public class MonkeyDataManager
    {
        IDataService azureService;
        IMobileServiceSyncTable<Monkey> monkeyTable;
        IFileHelper fileHelper;

        public MonkeyDataManager()
        {
            azureService = Xamarin.Forms.DependencyService.Get<IDataService>();
            monkeyTable = azureService.MonkeyTable;
            fileHelper = Xamarin.Forms.DependencyService.Get<IFileHelper>();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {

                await this.monkeyTable.MobileServiceClient.SyncContext.PushAsync();
                // FILES: Push file changes
                await this.monkeyTable.PushFileChangesAsync();

                // FILES: Automatic pull
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await this.monkeyTable.PullAsync("allmonkeys", this.monkeyTable.CreateQuery().Where((monkey)=> monkey.UserName == App.UserName));

            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }

        public async Task<List<Monkey>> GetMonkeysAsync()
        {
            try
            {
                var monkeys = await monkeyTable.ReadAsync();
                return monkeys.ToList();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public async Task SaveMonkeyAsync(Monkey item)
        {
            if (item.Id == null)
            {
                await this.monkeyTable.InsertAsync(item);
            }
            else
                await this.monkeyTable.UpdateAsync(item);
        }

        public async Task DeleteMonkeyAsync(Monkey item)
        {
            try
            {
                await monkeyTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
        }

        internal async Task<MobileServiceFile> AddImage(Monkey monkey, string imagePath)
        {
            string targetPath = fileHelper.CopyFileToAppDirectory(monkey.Id, imagePath);

            // FILES: Creating/Adding file
            MobileServiceFile file = await this.monkeyTable.AddFileAsync(monkey, Path.GetFileName(targetPath));


            // "Touch" the record to mark it as updated
            await this.monkeyTable.UpdateAsync(monkey);

            return file;
        }

        internal async Task DeleteImage(Monkey monkey, MobileServiceFile file)
        {
            // FILES: Deleting file
            await this.monkeyTable.DeleteFileAsync(file);

            // "Touch" the record to mark it as updated
            await this.monkeyTable.UpdateAsync(monkey);
        }

        internal async Task<IEnumerable<MobileServiceFile>> GetImageFiles(Monkey monkey)
        {
            // FILES: Get files (local)
            //if (requiresServerPull)
            //    await this.monkeyTable.PullFilesAsync(todoItem);
            return await this.monkeyTable.GetFilesAsync(monkey);
        }
    }
}
