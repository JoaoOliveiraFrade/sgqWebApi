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
            var result = dao.all();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("system/fbyTestManufs")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage fbyTestManufs(HttpRequestMessage request, List<string> listTestManufs)
        {
            var dao = new SystemDAO();
            var result = dao.fbyTestManufs(listTestManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("system/fbyDevManufs")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage fbyDevManufs(HttpRequestMessage request, List<string> devManufs)
        {
            var dao = new SystemDAO();
            var result = dao.fbyDevManufs(devManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("system/fromAgentFbyDevManufs")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage fromAgentFbyDevManufs(HttpRequestMessage request, List<string> devManufs) {
            var dao = new SystemDAO();
            var result = dao.fromAgentFbyDevManufs(devManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("system/fromAgentGbyDevManufs")]
        [ResponseType(typeof(IList<SystemGroupDevManuf>))]
        public HttpResponseMessage fromAgentGbyDevManufs(HttpRequestMessage request)
        {
            var dao = new SystemDAO();
            var list = dao.fromAgentGbyDevManufs();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

    }
}