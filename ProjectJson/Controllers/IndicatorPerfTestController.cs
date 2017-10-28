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
    public class indicatorPerfTestController : ApiController
    {
  //      [HttpGet]
  //      [Route("indicatorPerfTest/defectDensity/fbyProject/{subproject}/{delivery}")]
  //      [ResponseType(typeof(IList<DefectDensity>))]
  //      public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
  //          var indicatorsPerfDAO = new indicatorPerfTestDAO();
  //          var list = indicatorsPerfDAO.defectDensityFbyProject(subproject, delivery);
  //          indicatorsPerfDAO.Dispose();
  //          return request.CreateResponse(HttpStatusCode.OK, list);
  //      }

  //      [HttpPost]
  //      [Route("indicatorPerfTest/defectDensity/fbyListDevManufSystemProject")]
  //      [ResponseType(typeof(IList<DefectDensity>))]
  //      public HttpResponseMessage defectDensityFbyListDevManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters) {
  //          var indicatorsPerfDAO = new indicatorPerfTestDAO();
  //          var list = indicatorsPerfDAO.defectDensityFbyListDevManufSystemProject(parameters);
  //          indicatorsPerfDAO.Dispose();
  //          return request.CreateResponse(HttpStatusCode.OK, list);
  //      }

  //      [HttpPost]
		//[Route("indicatorPerfTest/defectOfTSInTI/fbyListDevManufSystemProject")]
  //      [ResponseType(typeof(IList<defectOfTSInTI>))]
  //      public HttpResponseMessage defectOfTSInTI_fbyListDevManufSystemProject(HttpRequestMessage request, ListDevManufSystemProject parameters)
		//{
		//	var indicatorsPerfDAO = new indicatorPerfTestDAO();
		//	var list = indicatorsPerfDAO.defectOfTSInTI_fbyListDevManufSystemProject(parameters);
  //          indicatorsPerfDAO.Dispose();
		//	return request.CreateResponse(HttpStatusCode.OK, list);
		//}

    }

}