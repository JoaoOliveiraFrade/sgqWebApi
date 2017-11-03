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
    public class IndOperDevDefectDensityController : ApiController
    {
        [HttpGet]
        [Route("indOperDev/defectDensity/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage dataFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var dao = new IndOperDevDefectDensityDao();
            var list = dao.dataFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        [Route("indOperDev/defectDensity/data")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage data(HttpRequestMessage request, devManufsystemProject parameters) {
            var dao = new IndOperDevDefectDensityDao();
            var list = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }

}