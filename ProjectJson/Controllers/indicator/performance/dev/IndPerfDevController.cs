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
    public class IndPerfDevController : ApiController
    {
        #region DefectDensity

        [HttpGet]
            [Route("indPerfDev/defectDensity/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorsPerfDao = new indPerfDevDao();
                var list = indicatorsPerfDao.defectDensityFbyProject(subproject, delivery);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indPerfDev/defectDensity/data")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorsPerfDao = new indPerfDevDao();
                var list = indicatorsPerfDao.defectDensityFbydevManufsystemProject(parameters);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region defectInsideSLA

            [HttpPost]
            [Route("indPerfDev/defectInsideSLA/data")]
            [ResponseType(typeof(IList<DefectInsideSLA>))]
            public HttpResponseMessage defectInsideSLAFbyListTestManufSystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indPerfDevDao = new indPerfDevDao();
                var list = indPerfDevDao.defectInsideSLAFbyListTestManufSystemProject(parameters);
                indPerfDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectOfTSInTI

        [HttpGet]
            [Route("indPerfDev/defectOfTSInTI/dataFbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(DefectOfTSInTI))]
            public HttpResponseMessage defectOfTSInTIFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorsPerfDao = new indPerfDevDao();
                var densityDefects = indicatorsPerfDao.defectOfTSInTIFbyProject(subproject, delivery);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, densityDefects);
            }

            [HttpPost]
            [Route("indPerfDev/defectOfTSInTI/data")]
            [ResponseType(typeof(IList<DefectOfTSInTI>))]
            public HttpResponseMessage data(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorsPerfDao = new indPerfDevDao();
                var list = indicatorsPerfDao.data(parameters);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectOfTSInTIAgent

        [HttpGet]
        [Route("indPerfDev/defectOfTSInTIAgent/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectOfTSInTI))]
        public HttpResponseMessage defectOfTSInTIAgentFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var indicatorsPerfDao = new indPerfDevDao();
            var densityDefects = indicatorsPerfDao.defectOfTSInTIAgentFbyProject(subproject, delivery);
            indicatorsPerfDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }

        [HttpPost]
        [Route("indPerfDev/defectOfTSInTIAgent/data")]
        [ResponseType(typeof(IList<DefectOfTSInTI>))]
        public HttpResponseMessage defectOfTSInTIAgentFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
            var indicatorsPerfDao = new indPerfDevDao();
            var list = indicatorsPerfDao.defectOfTSInTIAgentFbydevManufsystemProject(parameters);
            indicatorsPerfDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        #endregion

    }

}