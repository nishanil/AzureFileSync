using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.Files;
using GoMonkeys.Models;

[assembly: Xamarin.Forms.Dependency(typeof(GoMonkeys.AzureDataService))]
namespace GoMonkeys
{
    public class AzureDataService : IDataService
    {
        MobileServiceClient client;
        public MobileServiceClient Client { get { return client; } }

        //IMobileServiceSyncTable<TodoItem> todoTable;
        //public IMobileServiceSyncTable<TodoItem> TodoTable { get { return todoTable; } }

        IMobileServiceSyncTable<Monkey> monkeyTable;
        public IMobileServiceSyncTable<Monkey> MonkeyTable { get { return monkeyTable; } }
        public AzureDataService()
        {
            this.client = new MobileServiceClient(App.ApplicationURL);
            var store = new MobileServiceSQLiteStore("gomonkeystore.db");
            store.DefineTable<Monkey>(); 
            this.monkeyTable = this.client.GetSyncTable<Monkey>();
            this.client.InitializeFileSyncContext(new ImagesFileSyncHandler<Monkey>(monkeyTable), store);
            this.client.SyncContext.InitializeAsync(store, StoreTrackingOptions.AllNotifications);
            var dispose = this.client.EventManager.Subscribe<Microsoft.WindowsAzure.MobileServices.Eventing.IMobileServiceEvent>((e) => {
                System.Diagnostics.Debug.WriteLine("Event Handled: " + e.Name);
            });
        }
    }
}
