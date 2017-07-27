using ProjectWebApi.DAOs;
using ProjectWebApi.Models.SystemId;
using ProjectWebApi.Models.SystemByTestManuf;
using ProjectWebApi.Models.TestManuf;
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
        [Route("System/All")]
        [ResponseType(typeof(IList<SystemId>))]
        public HttpResponseMessage getAll(HttpRequestMessage request)
        {
            var dao = new SystemDAO();
            var list = dao.getAll();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("System/SystemsByTestManufs")]
        [ResponseType(typeof(IList<SystemId>))]
        public HttpResponseMessage getSystemsByTestManufs(HttpRequestMessage request, List<string> listTestManufs)
        {
            var dao = new SystemDAO();
            var list = dao.getSystemsByTestManuf(listTestManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}