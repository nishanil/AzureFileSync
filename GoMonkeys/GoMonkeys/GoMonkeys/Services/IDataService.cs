using GoMonkeys.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Files.Sync;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMonkeys
{
    interface IDataService
    {
        MobileServiceClient Client { get; }

        IMobileServiceSyncTable<Monkey> MonkeyTable { get; }
    }
}
