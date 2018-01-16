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
        [HttpGet]
        [Route("defect/defectDetail/{subproject}/{delivery}/{defect}")]
        [ResponseType(typeof(DefectDetail))]
        public HttpResponseMessage DefectDetail(HttpRequestMessage request, string subproject, string delivery, string defect)
        {
            var dao = new DefectDao();
            var result = dao.DefectDetail(subproject, delivery, defect);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("defect/defectTime/{subproject}/{delivery}/{defect}")]
        [ResponseType(typeof(IList<DefectTime>))]
        public HttpResponseMessage step(HttpRequestMessage request, string subproject, string delivery, string defect)
        {
            var dao = new DefectDao();
            var result = dao.DefectTime(subproject, delivery, defect);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("defect/defectsOpenInDevManuf/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpenInDevManuf(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.DefectsOpenInDevManuf(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("defect/defectsOpenInTestManuf/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage DefectsOpenInTestManuf(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.DefectsOpenInTestManuf(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

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
        [Route("defect/DefectsStatus/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectStatusByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.defectStatusFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("defect/DefectsStatusIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectStatusByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.DefectStatusByProjectIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("defect/DefectsGroupOrigin/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectsGroupOrigin(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDao();
            var result = dao.DefectsGroupOrigin(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPut]
        [Route("defect/DefectsGroupOriginIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage DefectsGroupOriginIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var dao = new DefectDao();
            var result = dao.DefectsGroupOriginIterations(subproject, delivery, iterations);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}