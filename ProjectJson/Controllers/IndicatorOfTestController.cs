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
    public class IndicatorOfTestController : ApiController
    {
        [HttpGet]
        [Route("IndicatorOfTest/ProdutivityInd/byProject/{subproject}/{delivery}")]
		[ResponseType(typeof(IList<ProdutivityInd>))]
		public HttpResponseMessage getProductivityBySubEnt(HttpRequestMessage request, string subproject, string delivery)
        {
            var IndicatorOfTestDAO = new IndicatorOfTestDAO();
            var productivitys = IndicatorOfTestDAO.getProductivityByProject(subproject, delivery);
			IndicatorOfTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, productivitys);
        }

		[HttpPut]
		[Route("IndicatorOfTest/ProdutivityInd/ByListTestManufSystemProject")]
		[ResponseType(typeof(IList<ProdutivityInd>))]
		public HttpResponseMessage getProdutivityIndByListTestManufSystemProject(HttpRequestMessage request, ProdutivityIndFilterParameters ProdutivityIndFilterParameters)
		{
			var IndicatorOfTestDAO = new IndicatorOfTestDAO();
			var productivitys = IndicatorOfTestDAO.getProdutivityIndByListTestManufSystemProject(ProdutivityIndFilterParameters);
			IndicatorOfTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, productivitys);
		}

		[HttpPut]
		[Route("IndicatorOfTest/RateRejectionEvidenceInd/ByListTestManufSystemProject")]
		[ResponseType(typeof(IList<RateRejectionEvidenceInd>))]
		public HttpResponseMessage getRateRejectionEvidenceIndByListTestManufSystemProject(HttpRequestMessage request, ProdutivityIndFilterParameters ProdutivityIndFilterParameters)
		{
			var IndicatorOfTestDAO = new IndicatorOfTestDAO();
			var productivitys = IndicatorOfTestDAO.getRateRejectionEvidenceIndByListTestManufSystemProject(ProdutivityIndFilterParameters);
			IndicatorOfTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, productivitys);
		}
	}
}