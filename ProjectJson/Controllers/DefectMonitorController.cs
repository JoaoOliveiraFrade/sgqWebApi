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
    public class defectMonitorController : ApiController
    {
		//[HttpGet]
		//[Route("defectMonitor/defectAqueue")]
		//[ResponseType(typeof(IList<DefectAqueue>))]
		//public HttpResponseMessage DefectAqueue(HttpRequestMessage request, Parameters parameters)
		//{
		//	var defectMonitorDAO = new DefectMonitorDAO();
		//	var list = defectMonitorDAO.defectAqueue(parameters);
        //  defectMonitorDAO.Dispose();
		//	return request.CreateResponse(HttpStatusCode.OK, list);
		//}
   }
}