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
    public class indicatorOperTestController : ApiController
    {
        //[HttpGet]
        //[Route("indicatorOperTest/productivity/byProject/{subproject}/{delivery}")]
        //[ResponseType(typeof(IList<productivity>))]
        //public HttpResponseMessage getProductivityBySubEnt(HttpRequestMessage request, string subproject, string delivery)
        //{
        //    var indicatorOperTestDAO = new indicatorOperTestDAO();
        //    var list = indicatorOperTestDAO.getProductivityByProject(subproject, delivery);
        //    indicatorOperTestDAO.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

        #region Productivity

        [HttpGet]
        [Route("indicatorOperTest/productivity/fbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var indicatorOperTestDAO = new IndicatorOperTestDAO();
            var list = indicatorOperTestDAO.productivityFbyProject(subproject, delivery);
            indicatorOperTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
		    [Route("indicatorOperTest/productivity/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<Productivity>))]
		    public HttpResponseMessage productivityByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var indicatorOperTestDAO = new IndicatorOperTestDAO();
			    var list = indicatorOperTestDAO.productivityByListTestManufSystemProject(parameters);
			    indicatorOperTestDAO.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, list);
		    }

        #endregion

        [HttpPost]
		[Route("indicatorOperTest/evidRejected/byListTestManufSystemProject")]
		[ResponseType(typeof(IList<evidRejected>))]
		public HttpResponseMessage evidRejectedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var indicatorOperTestDAO = new IndicatorOperTestDAO();
			var list = indicatorOperTestDAO.evidRejectedByListTestManufSystemProject(parameters);
			indicatorOperTestDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

        [HttpPost]
        [Route("indicatorOperTest/evidRejected/byListTestManufSystemProject/groupTimeline")]
        [ResponseType(typeof(IList<evidRejectedGroupTimeline>))]
        public HttpResponseMessage evidRejectedByListTestManufSystemProjectGroupTimeline(HttpRequestMessage request, Parameters parameters)
        {
            var indicatorOperTestDAO = new IndicatorOperTestDAO();
            var list = indicatorOperTestDAO.evidRejectedByListTestManufSystemProjectGroupTimeline(parameters);
            indicatorOperTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorOperTest/rateDefectUnfounded/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<RateDefectUnfounded>))]
        public HttpResponseMessage rateDefectUnfoundedByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var indicatorOperTestDAO = new IndicatorOperTestDAO();
            var list = indicatorOperTestDAO.rateDefectUnfoundedByListTestManufSystemProject(parameters);
            indicatorOperTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorOperTest/defectUat/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<defectUat>))]
        public HttpResponseMessage defectUatByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var indicatorOperTestDAO = new IndicatorOperTestDAO();
            var list = indicatorOperTestDAO.defectUatByListTestManufSystemProject(parameters);
            indicatorOperTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indicatorOperTest/averangeRetestHours/byListTestManufSystemProject")]
        [ResponseType(typeof(IList<AverangeRetestHours>))]
        public HttpResponseMessage averangeRetestHoursByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
        {
            var indicatorOperTestDAO = new IndicatorOperTestDAO();
            var list = indicatorOperTestDAO.averangeRetestHoursByListTestManufSystemProject(parameters);
            indicatorOperTestDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}