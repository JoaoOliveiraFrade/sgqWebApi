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
    public class TrgController : ApiController
    {
        [HttpPost]
        [Route("trg/loadSystems")]
        [ResponseType(typeof(IList<IdName>))]
        public HttpResponseMessage LoadSystems(HttpRequestMessage request, Release release)
        {
            var dao = new TrgDao();
            var result = dao.LoadSystems(release);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}