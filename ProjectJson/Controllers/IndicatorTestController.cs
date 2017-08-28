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
    public class IndicatorTestController : ApiController
    {
        //[HttpGet]
        //[Route("indicatorTest/produtivity/byProject/{subproject}/{delivery}")]
        //[ResponseType(typeof(IList<Produtivity>))]
        //public HttpResponseMessage getProductivityBySubEnt(HttpRequestMessage request, string subproject, string delivery)
        //{
        //    var IndicatorTestDAO = new IndicatorTestDAO();
        //    var list = IndicatorTestDAO.getProductivityByProject(subproject, delivery);
        //    IndicatorTestDAO.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

		[HttpPost]
		[Route("indicatorTest/produtivity/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<Produtivity>))]
		public HttpResponseMessage getProdutivityByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var list = IndicatorTestDAO.getProdutivityByListTestManufSystemProject(parameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

		[HttpPost]
		[Route("indicatorTest/rateEvidRejected/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<RateEvidRejected>))]
		public HttpResponseMessage getRateEvidRejectedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var list = IndicatorTestDAO.getRateEvidRejectedByListTestManufSystemProject(parameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

        [HttpPost]
        [Route("indicatorTest/rateEvidRejected/byListTestManufSystemProject/groupTimeline")]
        [ResponseType(typeof(IList<RateEvidRejectedGroupTimeline>))]
        public HttpResponseMessage getRateEvidRejectedByListTestManufSystemProjectGroupTimeline(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.getRateEvidRejectedByListTestManufSystemProjectGroupTimeline(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorTest/rateDefectUnfounded/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<RateDefectUnfounded>))]
        public HttpResponseMessage getRateDefectUnfoundedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.getRateDefectUnfoundedByListTestManufSystemProject(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorTest/rateDefectUat/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<RateDefectUat>))]
        public HttpResponseMessage getRateDefectUatByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var list = IndicatorTestDAO.getRateDefectUatByListTestManufSystemProject(parameters);
            IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}