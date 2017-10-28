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
    public class IndicatorAccomplishmentController : ApiController
    {
		[HttpPost]
		[Route("indicatorAccomplishmentQueue/defectInsideSla/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<defectInsideSla>))]
        public HttpResponseMessage defectInsideSlaFbyListTestManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters)
		{
			var indicatorAccomplishmentDAO = new IndicatorAccomplishmentDAO();
			var list = indicatorAccomplishmentDAO.defectInsideSlaFbyListTestManufSystemProject(parameters);
            indicatorAccomplishmentDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
    }

}