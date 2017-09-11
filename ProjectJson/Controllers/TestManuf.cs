using ProjectWebApi.DAOs;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class TestManufController : ApiController
    {
        [HttpGet]
        [Route("testManuf/all")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage all(HttpRequestMessage request)
        {
            var dao = new TestManufDAO();
            var list = dao.all();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}