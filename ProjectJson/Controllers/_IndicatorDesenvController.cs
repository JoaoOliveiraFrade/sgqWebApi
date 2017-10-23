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
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class IndicatorDesenvController : ApiController
    {
        //[HttpGet]
        //[Route("indicatorTest/productivity/byProject/{subproject}/{delivery}")]
        //[ResponseType(typeof(IList<productivity>))]
        //public HttpResponseMessage getProductivityBySubEnt(HttpRequestMessage request, string subproject, string delivery)
        //{
        //    var IndicatorTestDAO = new IndicatorTestDAO();
        //    var list = IndicatorTestDAO.getProductivityByProject(subproject, delivery);
        //    IndicatorTestDAO.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

		[HttpPost]
		[Route("indicatorTest/productivity/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<Productivity>))]
		public HttpResponseMessage productivityByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var list = IndicatorTestDAO.productivityByListTestManufSystemProject(parameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

		[HttpPost]
		[Route("indicatorTest/rateEvidRejected/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<RateEvidRejected>))]
		public HttpResponseMessage rateEvidRejectedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var list = IndicatorTestDAO.rateEvidRejectedByListTestManufSystemProject(parameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

        [HttpPost]
        [Route("indicatorTest/rateEvidRejected/byListTestManufSystemProject/groupTimeline")]
        [ResponseType(typeof(IList<RateEvidRejectedGroupTimeline>))]
        public HttpResponseMessage rateEvidRejectedByListTestManufSystemProjectGroupTimeline(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.rateEvidRejectedByListTestManufSystemProjectGroupTimeline(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorTest/rateDefectUnfounded/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<RateDefectUnfounded>))]
        public HttpResponseMessage rateDefectUnfoundedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.rateDefectUnfoundedByListTestManufSystemProject(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorTest/rateDefectUat/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<RateDefectUat>))]
        public HttpResponseMessage rateDefectUatByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.rateDefectUatByListTestManufSystemProject(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorTest/averangeRetestHours/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<AverangeRetestHours>))]
        public HttpResponseMessage averangeRetestHoursByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.averangeRetestHoursByListTestManufSystemProject(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}