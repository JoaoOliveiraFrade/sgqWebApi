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
    public class DefectTrafficLightController : ApiController
    {
		[HttpGet]
		[Route("defect/DefectTrafficLight/data")]
		[ResponseType(typeof(IList<IdName>))]
		public HttpResponseMessage DefectTrafficLight(HttpRequestMessage request)
		{
			var DefectTrafficLightDao = new DefectTrafficLightDao();
			var list = DefectTrafficLightDao.Data();
            DefectTrafficLightDao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
   }
}