using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ProjectWebApi.Daos;
using ProjectWebApi.Models.Project;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class TrgExecutionController : ApiController
    {
        [HttpPost]
        [Route("trg/trgExecution/lastDays")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage lastDays(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgExecutionDao();
            var result = dao.lastDays(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgExecution/groupMonth")]
        [ResponseType(typeof(IList<Status>))]
        public HttpResponseMessage groupMonth(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgExecutionDao();
            var result = dao.groupMonth(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgExecution/productivityXDefects")]
        [ResponseType(typeof(IList<ProductivityXDefects>))]
        public HttpResponseMessage productivityXDefects(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgExecutionDao();
            var result = dao.productivityXDefects(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgExecution/productivityXDefectsGroupWeekly")]
        [ResponseType(typeof(IList<ProductivityXDefectsGroupWeekly>))]
        public HttpResponseMessage productivityXDefectsGroupWeekly(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgExecutionDao();
            var result = dao.productivityXDefectsGroupWeekly(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgExecution/groupSystem")]
        [ResponseType(typeof(IList<GroupSystem>))]
        public HttpResponseMessage groupSystem(HttpRequestMessage request, TrgFilter TrgFilter) {
            var dao = new TrgExecutionDao();
            var result = dao.groupSystem(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}