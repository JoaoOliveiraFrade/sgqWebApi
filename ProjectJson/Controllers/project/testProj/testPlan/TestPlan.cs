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
    public class TestPlanController : ApiController
    {
        [HttpGet]
        [Route("project/testProj/testPlan/data/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<TestPlan>))]
        public HttpResponseMessage data(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new testPlanDao();
            var result = dao.data(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("project/testProj/testPlan/step")]
        [ResponseType(typeof(IList<Step>))]
        public HttpResponseMessage step(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new testPlanDao();
            var result = dao.step(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}