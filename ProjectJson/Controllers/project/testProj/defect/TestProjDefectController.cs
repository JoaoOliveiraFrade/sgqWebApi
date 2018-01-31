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
using ProjectWebApi.Models.Project;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class TestProjDefectController : ApiController
    {
        [HttpPost]
        [Route("project/testProj/testProjDefect/defectStatus")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage DefectStatus(HttpRequestMessage request, SubprojectDelivery subprojectDelivery)
        {
            var dao = new TestProjDefectDao();
            var result = dao.DefectStatus(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("project/testProj/testProjDefect/defectGroupOrigin")]
        [ResponseType(typeof(DefectStatus))]
        public HttpResponseMessage DefectGroupOrigin(HttpRequestMessage request, SubprojectDelivery subprojectDelivery)
        {
            var dao = new TestProjDefectDao();
            var result = dao.DefectGroupOrigin(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("project/testProj/testProjDefect/ctImpactedXDefects")]
        [ResponseType(typeof(CtImpactedXDefects))]
        public HttpResponseMessage CtImpactedXDefects(HttpRequestMessage request, SubprojectDelivery subprojectDelivery)
        {
            var dao = new TestProjDefectDao();
            var result = dao.CtImpactedXDefects(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("project/testProj/testProjDefect/defectsOpen")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpen(HttpRequestMessage request, SubprojectDelivery subprojectDelivery) {
            var dao = new TestProjDefectDao();
            var result = dao.DefectsOpen(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}