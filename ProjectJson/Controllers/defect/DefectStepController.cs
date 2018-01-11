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
    public class DefectStepController : ApiController
    {
        [HttpPost]
        [Route("defectStep/fbyProject")]
        [ResponseType(typeof(IList<DefectStep>))]
        public HttpResponseMessage OpenDefects(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectStepDao();
            var result = dao.FbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}