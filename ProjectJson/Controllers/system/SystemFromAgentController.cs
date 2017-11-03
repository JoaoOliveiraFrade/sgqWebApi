using ProjectWebApi.Daos;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class SystemFromAgentController : ApiController
    {
        [HttpGet]
        [Route("systemFromAgent/data")]
        [ResponseType(typeof(IList<Models.System>))]
        public HttpResponseMessage data(HttpRequestMessage request)
        {
            var Dao = new SystemDao();
            var result = Dao.data();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("systemFromAgent/dataGbyDevManuf")]
        [ResponseType(typeof(IList<SystemGbyDevManuf>))]
        public HttpResponseMessage dataGbyDevManuf(HttpRequestMessage request)
        {
            var Dao = new SystemDao();
            var result = Dao.dataGbyDevManuf();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("systemFromAgent/dataGbyTestManuf")]
        [ResponseType(typeof(IList<SystemGbyTestManuf>))]
        public HttpResponseMessage dataGbyTestManuf(HttpRequestMessage request)
        {
            var Dao = new SystemDao();
            var result = Dao.dataGbyTestManuf();
            Dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}