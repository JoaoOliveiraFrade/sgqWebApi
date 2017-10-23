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
		[Route("indicatorAccomplishmentQueue/rateDefectsWithinSLA/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<rateDefectsWithinSLA>))]
        public HttpResponseMessage rateDefectsWithinSLAFbyListTestManufSystemProject(HttpRequestMessage request, Parameters2 parameters)
		{
			var indicatorAccomplishmentDAO = new IndicatorAccomplishmentDAO();
			var list = indicatorAccomplishmentDAO.rateDefectsWithinSLAFbyListTestManufSystemProject(parameters);
            indicatorAccomplishmentDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

        [HttpPost]
        [Route("indicatorAccomplishment/defectDensity/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage defectDensitybyListTestManufSystemProject(HttpRequestMessage request, Parameters2 parameters)
        {
            var indicatorAccomplishmentDAO = new IndicatorAccomplishmentDAO();
            var list = indicatorAccomplishmentDAO.defectDensitybyListTestManufSystemProject(parameters);
            indicatorAccomplishmentDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }

}