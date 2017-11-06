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
            var dao = new SystemFromAgentDao();
            var result = dao.data();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("systemFromAgent/dataGbyDevManuf")]
        [ResponseType(typeof(IList<SystemGbyDevManuf>))]
        public HttpResponseMessage dataGbyDevManuf(HttpRequestMessage request)
        {
            var dao = new SystemFromAgentDao();
            var result = dao.dataGbyDevManuf();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("systemFromAgent/dataGbyTestManuf")]
        [ResponseType(typeof(IList<SystemGbyTestManuf>))]
        public HttpResponseMessage dataGbyTestManuf(HttpRequestMessage request)
        {
            var dao = new SystemFromAgentDao();
            var result = dao.dataGbyTestManuf();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}