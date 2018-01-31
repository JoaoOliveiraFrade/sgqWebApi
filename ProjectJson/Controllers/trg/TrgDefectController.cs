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
    public class TrgDefectController : ApiController
    {
        [HttpPost]
        [Route("trg/trgDefect/defectsOpen")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpen(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgDefectDao();
            var result = dao.DefectsOpen(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgDefect/defectStatus")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage DefectStatus(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgDefectDao();
            var result = dao.DefectStatus(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgDefect/defectGroupOrigin")]
        [ResponseType(typeof(DefectStatus))]
        public HttpResponseMessage defectGroupOrigin(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgDefectDao();
            var result = dao.defectGroupOrigin(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("trg/trgDefect/ctImpactedXDefects")]
        [ResponseType(typeof(CtImpactedXDefects))]
        public HttpResponseMessage ctImpactedXDefects(HttpRequestMessage request, TrgFilter TrgFilter)
        {
            var dao = new TrgDefectDao();
            var result = dao.ctImpactedXDefects(TrgFilter.release, TrgFilter.systems);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}