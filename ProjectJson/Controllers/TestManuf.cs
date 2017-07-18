using ProjectWebApi.DAOs;
using ProjectWebApi.Models.TestManuf;
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
        [Route("TestManuf/All")]
        [ResponseType(typeof(IList<TestManuf>))]
        public HttpResponseMessage getAll(HttpRequestMessage request)
        {
            var dao = new TestManufDAO();
            var list = dao.getAll();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}