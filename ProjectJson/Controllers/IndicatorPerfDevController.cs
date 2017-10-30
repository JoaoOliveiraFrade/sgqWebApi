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
    public class indicatorPerfDevController : ApiController
    {

        #region DefectDensity

        [HttpGet]
            [Route("indicatorPerfDev/defectDensity/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorsPerfDAO = new IndicatorPerfDevDAO();
                var list = indicatorsPerfDAO.defectDensityFbyProject(subproject, delivery);
                indicatorsPerfDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorPerfDev/defectDensity/fbyListDevManufSystemProject")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbyListDevManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters) {
                var indicatorsPerfDAO = new IndicatorPerfDevDAO();
                var list = indicatorsPerfDAO.defectDensityFbyListDevManufSystemProject(parameters);
                indicatorsPerfDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region defectInsideSLA

        [HttpPost]
            [Route("IndicatorPerfDevQueue/defectInsideSLA/fbyListDevManufSystemProject")]
            [ResponseType(typeof(IList<DefectInsideSLA>))]
            public HttpResponseMessage defectInsideSLAFbyListTestManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters) {
                var IndicatorPerfDevDAO = new IndicatorPerfDevDAO();
                var list = IndicatorPerfDevDAO.defectInsideSLAFbyListTestManufSystemProject(parameters);
                IndicatorPerfDevDAO.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region DefectDensity

        [HttpPost]
		    [Route("indicatorPerfDev/defectOfTSInTI/fbyListDevManufSystemProject")]
            [ResponseType(typeof(IList<DefectOfTSInTI>))]
            public HttpResponseMessage defectOfTSInTI_fbyListDevManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters)
		    {
			    var indicatorsPerfDAO = new IndicatorPerfDevDAO();
			    var list = indicatorsPerfDAO.defectOfTSInTI_fbyListDevManufSystemProject(parameters);
                indicatorsPerfDAO.Dispose();
			    return request.CreateResponse(HttpStatusCode.OK, list);
		    }

        #endregion

    }

}