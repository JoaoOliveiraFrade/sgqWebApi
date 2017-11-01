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
  //      [Route("indicatorPerfTest/defectDensity/fbydevManufsystemProject")]
  //      [ResponseType(typeof(IList<DefectDensity>))]
  //      public HttpResponseMessage defectDensityFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
  //          var indicatorsPerfDAO = new indicatorPerfTestDAO();
  //          var list = indicatorsPerfDAO.defectDensityFbydevManufsystemProject(parameters);
  //          indicatorsPerfDAO.Dispose();
  //          return request.CreateResponse(HttpStatusCode.OK, list);
  //      }

  //      [HttpPost]
		//[Route("indicatorPerfTest/defectOfTSInTI/fbydevManufsystemProject")]
  //      [ResponseType(typeof(IList<defectOfTSInTI>))]
  //      public HttpResponseMessage defectOfTSInTI_fbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters)
		//{
		//	var indicatorsPerfDAO = new indicatorPerfTestDAO();
		//	var list = indicatorsPerfDAO.defectOfTSInTI_fbydevManufsystemProject(parameters);
  //          indicatorsPerfDAO.Dispose();
		//	return request.CreateResponse(HttpStatusCode.OK, list);
		//}

    }

}