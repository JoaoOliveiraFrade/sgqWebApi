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
    public class ProjectBitiController : ApiController
    {
        [HttpGet]
        [Route("project/biti/data")]
        [ResponseType(typeof(IList<ProjectBiti>))]
        public HttpResponseMessage data(HttpRequestMessage request)
        {
            var dao = new ProjectBitiDao();
            var result = dao.data();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}