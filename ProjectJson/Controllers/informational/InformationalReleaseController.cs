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
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class InformationalReleaseController : ApiController
    {
        [HttpGet]
        [Route("informational/release/loadData")]
        [ResponseType(typeof(IList<Grouper>))]
        public HttpResponseMessage LoadData(HttpRequestMessage request)
        {
            var dao = new InformationalReleaseDao();
            var result = dao.LoadData();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}