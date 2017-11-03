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
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class IndOperTestController : ApiController
    {

        #region Productivity

        [HttpGet]
        [Route("indOperTest/productivity/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage dataFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var indOperTestDao = new indOperTestDao();
            var list = indOperTestDao.productivityFbyProject(subproject, delivery);
            indOperTestDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
		    [Route("indOperTest/productivity/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<Productivity>))]
		    public HttpResponseMessage productivityByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var indOperTestDao = new indOperTestDao();
			    var list = indOperTestDao.productivityByListTestManufSystemProject(parameters);
			    indOperTestDao.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, list);
		    }

        #endregion


        #region RejectionEvidence

            [HttpGet]
            [Route("indOperTest/rejectionEvidence/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<RejectionEvidence>))]
            public HttpResponseMessage rejectionEvidenceFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.rejectionEvidenceFbyProject(subproject, delivery);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
		    [Route("indOperTest/rejectionEvidence/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<RejectionEvidence>))]
		    public HttpResponseMessage rejectionEvidenceByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var indOperTestDao = new indOperTestDao();
			    var list = indOperTestDao.rejectionEvidenceByListTestManufSystemProject(parameters);
			    indOperTestDao.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, list);
		    }

        //[HttpPost]
        //[Route("indOperTest/rejectionEvidence/byListTestManufSystemProject/groupTimeline")]
        //[ResponseType(typeof(IList<rejectionEvidenceGroupTimeline>))]
        //public HttpResponseMessage rejectionEvidenceByListTestManufSystemProjectGroupTimeline(HttpRequestMessage request, Parameters parameters)
        //{
        //    var indOperTestDao = new indOperTestDao();
        //    var list = indOperTestDao.rejectionEvidenceByListTestManufSystemProjectGroupTimeline(parameters);
        //    indOperTestDao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}

        #endregion


        #region DefectUnfounded

            [HttpGet]
            [Route("indOperTest/defectUnfounded/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectUnfounded>))]
            public HttpResponseMessage defectUnfoundedFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectUnfoundedFbyProject(subproject, delivery);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indOperTest/defectUnfounded/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUnfounded>))]
            public HttpResponseMessage defectUnfoundedFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectUnfoundedFbyListTestManufSystemProject(parameters);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectUAT

            [HttpGet]
            [Route("indOperTest/defectUAT/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectUATFbyProject(subproject, delivery);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indOperTest/defectUAT/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectUATFbyListTestManufSystemProject(parameters);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectAverangeRetestTime

            [HttpGet]
            [Route("indOperTest/defectAverangeRetestTime/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectAverangeRetestTime>))]
            public HttpResponseMessage defectAverangeRetestTimeFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectAverangeRetestTimeFbyProject(subproject, delivery);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indOperTest/defectAverangeRetestTime/byListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectAverangeRetestTime>))]
            public HttpResponseMessage defectAverangeRetestTimeFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var indOperTestDao = new indOperTestDao();
                var list = indOperTestDao.defectAverangeRetestTimeFbyListTestManufSystemProject(parameters);
                indOperTestDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion

    }
}