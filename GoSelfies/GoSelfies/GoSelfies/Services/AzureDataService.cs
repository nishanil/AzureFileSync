﻿using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.Files;

[assembly: Xamarin.Forms.Dependency(typeof(GoSelfies.AzureDataService))]
namespace GoSelfies
{
    public class AzureDataService : IDataService
    {
        MobileServiceClient client;
        public MobileServiceClient Client { get { return client; } }

        IMobileServiceSyncTable<TodoItem> todoTable;

        public IMobileServiceSyncTable<TodoItem> TodoTable { get { return todoTable; } }

        public AzureDataService()
        {
            this.client = new MobileServiceClient(App.ApplicationURL);
            var store = new MobileServiceSQLiteStore("goselfiesstore.db");
            store.DefineTable<TodoItem>();
            this.client.InitializeFileSyncContext(new ImagesFileSyncHandler(), store);
            this.client.SyncContext.InitializeAsync(store, StoreTrackingOptions.AllNotifications);
            this.todoTable = this.client.GetSyncTable<TodoItem>();
            var dispose = this.client.EventManager.Subscribe<Microsoft.WindowsAzure.MobileServices.Eventing.IMobileServiceEvent>((e) => {
                System.Diagnostics.Debug.WriteLine("Event Handled: " + e.Name);
            });
        }
    }
}
