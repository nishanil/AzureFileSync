using GoMonkeysService.DataObjects;
using Microsoft.WindowsAzure.MobileServices.Files.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.MobileServices.Files;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service.Files;

namespace GoMonkeysService.Controllers
{
    public class MonkeyStorageController : StorageController<Monkey>
    {
        public MonkeyStorageController()
        {

        }
        [HttpPost]
        [Route("tables/Monkey/{id}/StorageToken")]
        public async Task<HttpResponseMessage> PostStorageTokenRequest(string id, StorageTokenRequest value)
        {
            StorageToken token = await GetStorageTokenAsync(id, value);

            return Request.CreateResponse(token);
        }

        // Get the files associated with this record
        [HttpGet]
        [Route("tables/Monkey/{id}/MobileServiceFiles")]
        public async Task<HttpResponseMessage> GetFiles(string id)
        {
            IEnumerable<MobileServiceFile> files = await GetRecordFilesAsync(id);

            return Request.CreateResponse(files);
        }

        [HttpDelete]
        [Route("tables/Monkey/{id}/MobileServiceFiles/{name}")]
        public Task Delete(string id, string name)
        {
            return base.DeleteFileAsync(id, name);
        }
    }
}