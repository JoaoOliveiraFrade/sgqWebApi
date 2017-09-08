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
    public class DefectQueueController : ApiController
    {
		[HttpGet]
		[Route("defectQueue/all")]
		[ResponseType(typeof(IList<IdName>))]
		public HttpResponseMessage DefectQueue(HttpRequestMessage request)
		{
			var defectQueueDAO = new DefectQueueDAO();
			var list = defectQueueDAO.All();
            defectQueueDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
   }
}