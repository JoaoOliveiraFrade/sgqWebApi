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
        [HttpGet]
        [Route("indicatorTest/produtivity/byProject/{subproject}/{delivery}")]
		[ResponseType(typeof(IList<Produtivity>))]
		public HttpResponseMessage getProductivityBySubEnt(HttpRequestMessage request, string subproject, string delivery)
        {
            var IndicatorTestDAO = new IndicatorTestDAO();
            var productivitys = IndicatorTestDAO.getProductivityByProject(subproject, delivery);
			IndicatorTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, productivitys);
        }

		[HttpPut]
		[Route("indicatorTest/produtivity/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<Produtivity>))]
		public HttpResponseMessage getProdutivityByListTestManufSystemProject(HttpRequestMessage request, ProdutivityFilterParameters ProdutivityFilterParameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var productivitys = IndicatorTestDAO.getProdutivityByListTestManufSystemProject(ProdutivityFilterParameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, productivitys);
		}

		[HttpPut]
		[Route("indicatorTest/rateEvidRejected/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<RateEvidRejected>))]
		public HttpResponseMessage getRateEvidRejectedByListTestManufSystemProject(HttpRequestMessage request, ProdutivityFilterParameters ProdutivityFilterParameters)
		{
			var IndicatorTestDAO = new IndicatorTestDAO();
			var productivitys = IndicatorTestDAO.getRateEvidRejectedByListTestManufSystemProject(ProdutivityFilterParameters);
			IndicatorTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, productivitys);
		}
	}
}