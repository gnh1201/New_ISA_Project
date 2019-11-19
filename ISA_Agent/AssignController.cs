using System.Collections.Generic;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace ISA_Agent
{
    // A very simple controller to handle People CRUD.
    // Notice how it Inherits from WebApiController and the methods have WebApiHandler attributes 
    // This is for sampling purposes only.
    public sealed class AssignController : WebApiController
    {
        // Gets all records.
        // This will respond to 
        //     GET http://localhost:8877/assign/get
        [Route(HttpVerbs.Get, "/get")]
        public Task<AssignAjaxModel> GetAllPeople() => AssignService.GetDataAsync();
    }
}