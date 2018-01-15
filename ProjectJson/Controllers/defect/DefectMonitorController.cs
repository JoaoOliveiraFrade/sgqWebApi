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
    public class defectMonitorController : ApiController
    {
        [HttpPost]
        [Route("defect/defectMonitor/fbyQueueStatusTrafficLightProject")]
        [ResponseType(typeof(IList<DefectMonitor>))]
        public HttpResponseMessage OpenDefects(HttpRequestMessage request, DefectMonitorParameter parameter)
        {
            var dao = new DefectMonitorDao();
            var result = dao.FbyQueueStatusTrafficLightProject(parameter);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}