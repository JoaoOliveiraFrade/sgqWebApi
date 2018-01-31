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
    public class defectController : ApiController
    {
        [HttpPost]
        [Route("defect/defectsOpen")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpenInDevManuf(HttpRequestMessage request, SubprojectDelivery subprojectDelivery) {
            var dao = new DefectDao();
            var result = dao.DefectsOpen(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("defect/defectDetail")]
        [ResponseType(typeof(DefectDetail))]
        public HttpResponseMessage DefectDetail(HttpRequestMessage request, Defect defect) {
            var dao = new DefectDao();
            var result = dao.DefectDetail(defect);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("defect/defectTime")]
        [ResponseType(typeof(IList<DefectTime>))]
        public HttpResponseMessage step(HttpRequestMessage request, Defect defect) {
            var dao = new DefectDao();
            var result = dao.DefectTime(defect);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpGet]
        //[Route("defect/defectsOpenInDevManuf/{subproject}/{delivery}")]
        //[ResponseType(typeof(IList<DefectsOpen>))]
        //public HttpResponseMessage DefectsOpenInDevManuf(HttpRequestMessage request, string subproject, string delivery)
        //{
        //    var dao = new DefectDao();
        //    var result = dao.DefectsOpenInDevManuf(subproject, delivery);
        //    dao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[HttpGet]
        //[Route("defect/defectsOpenInTestManuf/{subproject}/{delivery}")]
        //[ResponseType(typeof(IList<DefectsOpen>))]
        //public HttpResponseMessage DefectsOpenInTestManuf(HttpRequestMessage request, string subproject, string delivery)
        //{
        //    var dao = new DefectDao();
        //    var result = dao.DefectsOpenInTestManuf(subproject, delivery);
        //    dao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}

        [HttpPut]
        [Route("defect/defectsOpenInDevManufIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpenInDevManufIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.DefectsOpenInDevManufIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("defect/defectsOpenInTestManufIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpenInTestManufIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.DefectsOpenInTestManufIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("defect/defectStatus/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectStatusByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.defectStatusFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("defect/defectStatusIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectStatusByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.DefectStatusByProjectIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("defect/defectGroupOrigin/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage defectGroupOrigin(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.defectGroupOrigin(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPut]
        [Route("defect/defectGroupOriginIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage defectGroupOriginIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.defectGroupOriginIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}