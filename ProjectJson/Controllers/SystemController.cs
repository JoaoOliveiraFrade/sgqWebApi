using ProjectWebApi.Daos;
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
        #region FromCTAndDefect

            [HttpGet]
            [Route("system/fromCTAndDefec")]
            [ResponseType(typeof(IList<Models.System>))]
            public HttpResponseMessage fromCTAndDefec(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromCTAndDefec();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpGet]
            [Route("system/fromCTAndDefectGbyDevManuf")]
            [ResponseType(typeof(IList<SystemGbyDevManuf>))]
            public HttpResponseMessage fromCTAndDefectGbyDevManuf(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromCTAndDefectGbyDevManuf();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpGet]
            [Route("system/fromCTAndDefectGbyTestManuf")]
            [ResponseType(typeof(IList<SystemGbyTestManuf>))]
            public HttpResponseMessage fromCTAndDefectGbyTestManuf(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromCTAndDefectGbyTestManuf();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

        #endregion


        #region FromAgent

            [HttpGet]
            [Route("system/fromAgent")]
            [ResponseType(typeof(IList<Models.System>))]
            public HttpResponseMessage fromAgent(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromAgent();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpGet]
            [Route("system/fromAgentGbyDevManuf")]
            [ResponseType(typeof(IList<SystemGbyDevManuf>))]
            public HttpResponseMessage fromAgentGbyDevManuf(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromAgentGbyDevManuf();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpGet]
            [Route("system/fromAgentGbyTestManuf")]
            [ResponseType(typeof(IList<SystemGbyTestManuf>))]
            public HttpResponseMessage fromAgentGbyTestManuf(HttpRequestMessage request)
            {
                var Dao = new SystemDao();
                var result = Dao.fromAgentGbyTestManuf();
                Dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

        #endregion
    }
}