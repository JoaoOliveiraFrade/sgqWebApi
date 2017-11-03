using ProjectWebApi.Daos;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class DevManufController : ApiController
    {
        [HttpGet]
        [Route("devManuf/all")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage all(HttpRequestMessage request)
        {
            var Dao = new DevManufDao();
            var list = Dao.all();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("devManuf/allfromAgent")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage allfromAgent(HttpRequestMessage request)
        {
            var Dao = new DevManufDao();
            var list = Dao.allfromAgent();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

    }
}