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

namespace GoSelfies
{
    public class TodoItemManager
    {
        IDataService azureService;
        IMobileServiceSyncTable<TodoItem> todoTable;
        IFileHelper fileHelper;
        public TodoItemManager()
        {
            //azureService = Xamarin.Forms.DependencyService.Get<IDataService>();
            azureService = new AzureDataService();
            todoTable = azureService.TodoTable;
            fileHelper = Xamarin.Forms.DependencyService.Get<IFileHelper>();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {

                await this.todoTable.MobileServiceClient.SyncContext.PushAsync();
                // FILES: Push file changes
                await this.todoTable.PushFileChangesAsync();
                
                // FILES: Automatic pull
                // A normal pull will automatically process new/modified/deleted files, engaging the file sync handler
                await this.todoTable.PullAsync("todoItems", this.todoTable.CreateQuery());
                
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

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            try
            {
                var todos = await todoTable.ReadAsync();
                return todos.ToList();
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

        public async Task SaveTaskAsync(TodoItem item)
        {
            if (item.Id == null)
            {
                await todoTable.InsertAsync(item);
            }
            else
                await todoTable.UpdateAsync(item);
        }

        public async Task DeleteTaskAsync(TodoItem item)
        {
            try
            {
                await todoTable.DeleteAsync(item);
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

        internal async Task<MobileServiceFile> AddImage(TodoItem todoItem, string imagePath)
        {
            string targetPath = fileHelper.CopyFileToAppDirectory(todoItem.Id, imagePath);

            // FILES: Creating/Adding file
            MobileServiceFile file =  await this.todoTable.AddFileAsync(todoItem, Path.GetFileName(targetPath));

            
            // "Touch" the record to mark it as updated
            await this.todoTable.UpdateAsync(todoItem);

            return file;
        }

        internal async Task DeleteImage(TodoItem todoItem, MobileServiceFile file)
        {
            // FILES: Deleting file
            await this.todoTable.DeleteFileAsync(file);

            // "Touch" the record to mark it as updated
            await this.todoTable.UpdateAsync(todoItem);
        }

        internal async Task<IEnumerable<MobileServiceFile>> GetImageFiles(TodoItem todoItem, bool requiresServerPull = false)
        {
            // FILES: Get files (local)
            if (requiresServerPull)
                await this.todoTable.PullFilesAsync(todoItem);
            return await this.todoTable.GetFilesAsync(todoItem);
        }
    }
}
