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
            var dao = new IndOperTestDao();
            var result = dao.productivityFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
		    [Route("indOperTest/productivity/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<Productivity>))]
		    public HttpResponseMessage productivityByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var dao = new IndOperTestDao();
			    var result = dao.productivityByListTestManufSystemProject(parameters);
                dao.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, result);
		    }

        #endregion


        #region RejectionEvidence

            [HttpGet]
            [Route("indOperTest/rejectionEvidence/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<RejectionEvidence>))]
            public HttpResponseMessage rejectionEvidenceFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var dao = new IndOperTestDao();
                var result = dao.rejectionEvidenceFbyProject(subproject, delivery);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpPost]
		    [Route("indOperTest/rejectionEvidence/byListTestManufSystemProject")]
		    [ResponseType(typeof(IList<RejectionEvidence>))]
		    public HttpResponseMessage rejectionEvidenceByListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
		    {
			    var dao = new IndOperTestDao();
			    var result = dao.rejectionEvidenceByListTestManufSystemProject(parameters);
                dao.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, result);
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
                var dao = new IndOperTestDao();
                var result = dao.defectUnfoundedFbyProject(subproject, delivery);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpPost]
            [Route("indOperTest/defectUnfounded/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUnfounded>))]
            public HttpResponseMessage defectUnfoundedFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var dao = new IndOperTestDao();
                var result = dao.defectUnfoundedFbyListTestManufSystemProject(parameters);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

        #endregion


        #region DefectUAT

            [HttpGet]
            [Route("indOperTest/defectUAT/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var dao = new IndOperTestDao();
                var result = dao.defectUATFbyProject(subproject, delivery);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpPost]
            [Route("indOperTest/defectUAT/fbyListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectUAT>))]
            public HttpResponseMessage defectUATFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var dao = new IndOperTestDao();
                var result = dao.defectUATFbyListTestManufSystemProject(parameters);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

        #endregion


        #region DefectAverangeRetestTime

            [HttpGet]
            [Route("indOperTest/defectAverangeRetestTime/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectAverangeRetestTime>))]
            public HttpResponseMessage defectAverangeRetestTimeFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var dao = new IndOperTestDao();
                var result = dao.defectAverangeRetestTimeFbyProject(subproject, delivery);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

            [HttpPost]
            [Route("indOperTest/defectAverangeRetestTime/byListTestManufSystemProject")]
            [ResponseType(typeof(IList<DefectAverangeRetestTime>))]
            public HttpResponseMessage defectAverangeRetestTimeFbyListTestManufSystemProject(HttpRequestMessage request, Parameters parameters)
            {
                var dao = new IndOperTestDao();
                var result = dao.defectAverangeRetestTimeFbyListTestManufSystemProject(parameters);
                dao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, result);
            }

        #endregion

    }
}