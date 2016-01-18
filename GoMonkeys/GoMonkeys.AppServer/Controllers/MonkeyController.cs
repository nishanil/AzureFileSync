using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using GoMonkeysService.DataObjects;
using GoMonkeysService.Models;

namespace GoMonkeysService.Controllers
{
    public class MonkeyController : TableController<Monkey>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMonkeysContext context = new GoMonkeysContext();
            DomainManager = new EntityDomainManager<Monkey>(context, Request);
        }

        // GET tables/Monkey
        public IQueryable<Monkey> GetAllMonkey()
        {
            return Query(); 
        }

        // GET tables/Monkey/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Monkey> GetMonkey(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Monkey/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Monkey> PatchMonkey(string id, Delta<Monkey> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Monkey
        public async Task<IHttpActionResult> PostMonkey(Monkey item)
        {
            Monkey current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Monkey/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMonkey(string id)
        {
             return DeleteAsync(id);
        }
    }
}
