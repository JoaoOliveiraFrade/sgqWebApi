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
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class executionController : ApiController
    {
        [HttpGet]
        [Route("project/testProj/execution/lastDays/{subproject}/{delivery}")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage lastDays(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new ExecutionDao();
            var result = dao.lastDays(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("project/testProj/execution/groupMonth/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<Status>))]
        public HttpResponseMessage groupMonth(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new ExecutionDao();
            var result = dao.groupMonth(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("project/testProj/execution/productivityXDefects/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefects>))]
        public HttpResponseMessage productivityXDefects(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new ExecutionDao();
            var result = dao.productivityXDefects(subproject, delivery);
            dao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("project/testProj/execution/productivityXDefectsGroupWeekly/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefectsGroupWeekly>))]
        public HttpResponseMessage productivityXDefectsGroupWeekly(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new ExecutionDao();
            var result = dao.productivityXDefectsGroupWeekly(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        // Iteration
        // =================================================

        [HttpPut]
        [Route("project/testProj/execution/lastDaysByIteration/{subproject}/{delivery}")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage lastDaysByIteration(HttpRequestMessage request, string subproject, string delivery, List<string> iteration)
        {
            var dao = new ExecutionDao();
            var result = dao.lastDaysByIteration(subproject, delivery, iteration);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("project/testProj/execution/groupMonthByIteration/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<Status>))]
        public HttpResponseMessage groupMonthByIteration(HttpRequestMessage request, string subproject, string delivery, List<string> iteration)
        {
            var dao = new ExecutionDao();
            var result = dao.groupMonthByIteration(subproject, delivery, iteration);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("project/testProj/execution/productivityXDefectsIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefects>))]
        public HttpResponseMessage productivityXDefectsIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new ExecutionDao();
            var result = dao.productivityXDefectsIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("project/testProj/execution/productivityXDefectsGroupWeeklyIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefectsGroupWeekly>))]
        public HttpResponseMessage productivityXDefectsGroupWeeklyIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new ExecutionDao();
            var result = dao.productivityXDefectsGroupWeeklyIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}