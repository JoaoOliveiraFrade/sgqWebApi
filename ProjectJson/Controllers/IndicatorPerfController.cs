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

using ProjectWebApi.DAOs;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class IndicatorPerfController : ApiController
    {
        [HttpPost]
        [Route("indicatorPerf/defectDensity/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage defectDensity_fbyListDevManufSystemProject(HttpRequestMessage request, Parameters2 parameters) {
            var indicatorsPerfDAO = new IndicatorPerfDAO();
            var list = indicatorsPerfDAO.defectDensity_fbyListDevManufSystemProject(parameters);
            indicatorsPerfDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
		[Route("indicatorPerf/defectOfTSInTI/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<defectOfTSInTI>))]
        public HttpResponseMessage defectOfTSInTI_fbyListDevManufSystemProject(HttpRequestMessage request, Parameters2 parameters)
		{
			var indicatorsPerfDAO = new IndicatorPerfDAO();
			var list = indicatorsPerfDAO.defectOfTSInTI_fbyListDevManufSystemProject(parameters);
            indicatorsPerfDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
    }

}