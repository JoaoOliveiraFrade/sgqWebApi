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
    public class indicatorPerfDevController : ApiController
    {

        #region DefectDensity

        [HttpGet]
            [Route("indicatorPerfDev/defectDensity/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorsPerfDao = new IndicatorPerfDevDao();
                var list = indicatorsPerfDao.defectDensityFbyProject(subproject, delivery);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorPerfDev/defectDensity/fbydevManufsystemProject")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorsPerfDao = new IndicatorPerfDevDao();
                var list = indicatorsPerfDao.defectDensityFbydevManufsystemProject(parameters);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region defectInsideSLA

            [HttpPost]
            [Route("indicatorPerfDev/defectInsideSLA/fbydevManufsystemProject")]
            [ResponseType(typeof(IList<DefectInsideSLA>))]
            public HttpResponseMessage defectInsideSLAFbyListTestManufSystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var IndicatorPerfDevDao = new IndicatorPerfDevDao();
                var list = IndicatorPerfDevDao.defectInsideSLAFbyListTestManufSystemProject(parameters);
                IndicatorPerfDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectOfTSInTI

        [HttpGet]
            [Route("indicatorPerfDev/defectOfTSInTI/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(DefectOfTSInTI))]
            public HttpResponseMessage defectOfTSInTIFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorsPerfDao = new IndicatorPerfDevDao();
                var densityDefects = indicatorsPerfDao.defectOfTSInTIFbyProject(subproject, delivery);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, densityDefects);
            }

            [HttpPost]
            [Route("indicatorPerfDev/defectOfTSInTI/fbydevManufsystemProject")]
            [ResponseType(typeof(IList<DefectOfTSInTI>))]
            public HttpResponseMessage defectOfTSInTIFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorsPerfDao = new IndicatorPerfDevDao();
                var list = indicatorsPerfDao.defectOfTSInTIFbydevManufsystemProject(parameters);
                indicatorsPerfDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion

        #region DefectOfTSInTIAgent

        [HttpGet]
        [Route("indicatorPerfDev/defectOfTSInTIAgent/fbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectOfTSInTI))]
        public HttpResponseMessage defectOfTSInTIAgentFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var indicatorsPerfDao = new IndicatorPerfDevDao();
            var densityDefects = indicatorsPerfDao.defectOfTSInTIAgentFbyProject(subproject, delivery);
            indicatorsPerfDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }

        [HttpPost]
        [Route("indicatorPerfDev/defectOfTSInTIAgent/fbydevManufsystemProject")]
        [ResponseType(typeof(IList<DefectOfTSInTI>))]
        public HttpResponseMessage defectOfTSInTIAgentFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
            var indicatorsPerfDao = new IndicatorPerfDevDao();
            var list = indicatorsPerfDao.defectOfTSInTIAgentFbydevManufsystemProject(parameters);
            indicatorsPerfDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        #endregion





    }

}