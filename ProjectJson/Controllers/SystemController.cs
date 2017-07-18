using ProjectWebApi.DAOs;
using ProjectWebApi.Models.System_;
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
        [ResponseType(typeof(IList<System_>))]
        public HttpResponseMessage getAll(HttpRequestMessage request)
        {
            var dao = new SystemDAO();
            var list = dao.getAll();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        //[HttpGet]
        //[Route("System/SystemsByTestManuf")]
        //[ResponseType(typeof(IList<SystemByTestManuf>))]
        //public HttpResponseMessage getSystemsByTestManuf(HttpRequestMessage request)
        //{
        //    var dao = new SystemDAO();
        //    var list = dao.getSystemsByTestManuf();
        //    dao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

        [HttpPost]
        [Route("System/SystemsByTestManufs")]
        [ResponseType(typeof(IList<System_>))]
        public HttpResponseMessage getSystemsByTestManufs(HttpRequestMessage request, List<string> listTestManufs)
        {
            var dao = new SystemDAO();
            var list = dao.getSystemsByTestManufs(listTestManufs);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}