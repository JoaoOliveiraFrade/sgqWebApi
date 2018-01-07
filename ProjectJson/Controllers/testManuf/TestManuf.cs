using ProjectWebApi.Daos;
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
        [Route("testManuf/data")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage TestManuf(HttpRequestMessage request)
        {
            var Dao = new TestManufDao();
            var list = Dao.Data();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}