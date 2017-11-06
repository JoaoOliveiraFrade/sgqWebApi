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
        [Route("devManuf/data")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage data(HttpRequestMessage request)
        {
            var dao = new DevManufDao();
            var result = dao.data();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("devManuf/dataFromAgent")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage dataFromAgent(HttpRequestMessage request)
        {
            var dao = new DevManufDao();
            var result = dao.dataFromAgent();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}