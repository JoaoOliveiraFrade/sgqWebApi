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
    public class DefectQueueController : ApiController
    {
		[HttpGet]
		[Route("defectQueue/all")]
		[ResponseType(typeof(IList<IdName>))]
		public HttpResponseMessage DefectQueue(HttpRequestMessage request)
		{
			var defectQueueDao = new DefectQueueDao();
			var list = defectQueueDao.All();
            defectQueueDao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
   }
}