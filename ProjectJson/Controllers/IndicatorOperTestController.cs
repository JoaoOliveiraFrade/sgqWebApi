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


        #region RejectionEvidence

            [HttpGet]
            [Route("indicatorOperTest/rejectionEvidence/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<RejectionEvidence>))]
            public HttpResponseMessage rejectionEvidenceFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperTestDAO = new IndicatorOperTestDAO();
                var list = indicatorOperTestDAO.rejectionEvidenceFbyProject(subproject, delivery);
                indicatorOperTestDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
		    [Route("indicatorOperTest/rejectionEvidence/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<RejectionEvidence>))]
		    public HttpResponseMessage rejectionEvidenceByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var indicatorOperTestDAO = new IndicatorOperTestDAO();
			    var list = indicatorOperTestDAO.rejectionEvidenceByListTestManufSystemProject(parameters);
			    indicatorOperTestDAO.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, list);
		    }

        //[HttpPost]
        //[Route("indicatorOperTest/rejectionEvidence/byListTestManufSystemProject/groupTimeline")]
        //[ResponseType(typeof(IList<rejectionEvidenceGroupTimeline>))]
        //public HttpResponseMessage rejectionEvidenceByListTestManufSystemProjectGroupTimeline(HttpRequestMessage request, Parameters parameters)
        //{
        //    var indicatorOperTestDAO = new IndicatorOperTestDAO();
        //    var list = indicatorOperTestDAO.rejectionEvidenceByListTestManufSystemProjectGroupTimeline(parameters);
        //    indicatorOperTestDAO.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

        #endregion


        #region DefectUnfounded

            [HttpGet]
            [Route("indicatorOperTest/defectUnfounded/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectUnfounded>))]
            public HttpResponseMessage defectUnfoundedFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperTestDAO = new IndicatorOperTestDAO();
                var list = indicatorOperTestDAO.defectUnfoundedFbyProject(subproject, delivery);
                indicatorOperTestDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorOperTest/defectUnfounded/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUnfounded>))]
            public HttpResponseMessage defectUnfoundedFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var indicatorOperTestDAO = new IndicatorOperTestDAO();
                var list = indicatorOperTestDAO.defectUnfoundedFbyListTestManufSystemProject(parameters);
                indicatorOperTestDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectUAT

            [HttpGet]
            [Route("indicatorOperTest/defectUAT/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperTestDAO = new IndicatorOperTestDAO();
                var list = indicatorOperTestDAO.defectUATFbyProject(subproject, delivery);
                indicatorOperTestDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorOperTest/defectUAT/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var indicatorOperTestDAO = new IndicatorOperTestDAO();
                var list = indicatorOperTestDAO.defectUATFbyListTestManufSystemProject(parameters);
                indicatorOperTestDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion

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