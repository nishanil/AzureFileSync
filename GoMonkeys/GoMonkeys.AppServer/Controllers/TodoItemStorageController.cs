using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.WindowsAzure.MobileServices.Files.Controllers;
using GoMonkeysService.DataObjects;
using GoMonkeysService.Models;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices.Files;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service.Files;

namespace GoMonkeysService.Controllers
{
    public class TodoItemStorageController : StorageController<TodoItem>
    {
        public TodoItemStorageController()
        {

        }
        [HttpPost]
        [Route("tables/TodoItem/{id}/StorageToken")]
        public async Task<HttpResponseMessage> PostStorageTokenRequest(string id, StorageTokenRequest value)
        {
            StorageToken token = await GetStorageTokenAsync(id, value);

            return Request.CreateResponse(token);
        }

        // Get the files associated with this record
        [HttpGet]
        [Route("tables/TodoItem/{id}/MobileServiceFiles")]
        public async Task<HttpResponseMessage> GetFiles(string id)
        {
            IEnumerable<MobileServiceFile> files = await GetRecordFilesAsync(id);

            return Request.CreateResponse(files);
        }

        [HttpDelete]
        [Route("tables/TodoItem/{id}/MobileServiceFiles/{name}")]
        public Task Delete(string id, string name)
        {
            return base.DeleteFileAsync(id, name);
        }
    }
}
