using ProjectWebApi.DAOs;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class SystemController : ApiController
    {
        [HttpGet]
        [Route("system/all")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage All(HttpRequestMessage request)
        {
            var dao = new SystemDAO();
            var list = dao.all();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("system/ofTestManufs")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage ofTestManufs(HttpRequestMessage request, List<string> listTestManufs)
        {
            var dao = new SystemDAO();
            var list = dao.ofTestManufs(listTestManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("system/ofDevManufs")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage ofDevManufs(HttpRequestMessage request, List<string> listDevManufs)
        {
            var dao = new SystemDAO();
            var list = dao.ofDevManufs(listDevManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("system/ofQueueGroupDevManufs")]
        [ResponseType(typeof(IList<SystemGroupDevManuf>))]
        public HttpResponseMessage ofQueueGroupDevManufs(HttpRequestMessage request)
        {
            var dao = new SystemDAO();
            var list = dao.ofQueueGroupDevManufs();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}