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
    public class DefectTrafficLightController : ApiController
    {
		[HttpGet]
		[Route("DefectTrafficLight/all")]
		[ResponseType(typeof(IList<IdName>))]
		public HttpResponseMessage DefectTrafficLight(HttpRequestMessage request)
		{
			var DefectTrafficLightDAO = new DefectTrafficLightDAO();
			var list = DefectTrafficLightDAO.All();
            DefectTrafficLightDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
   }
}