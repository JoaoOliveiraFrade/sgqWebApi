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
    public class DefectStatusController : ApiController
    {
		[HttpGet]
		[Route("defectStatus/all")]
		[ResponseType(typeof(IList<IdName>))]
		public HttpResponseMessage DefectStatus(HttpRequestMessage request)
		{
			var defectStatusDao = new DefectStatusDao();
			var list = defectStatusDao.All();
            defectStatusDao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
   }
}