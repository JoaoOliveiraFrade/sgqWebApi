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
    public class IndPerfTestController : ApiController
    {
  //      [HttpGet]
  //      [Route("indPerfTest/defectDensity/dataFbyProject/{subproject}/{delivery}")]
  //      [ResponseType(typeof(IList<DefectDensity>))]
  //      public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
  //          var indicatorsPerfDao = new indPerfTestDao();
  //          var list = indicatorsPerfDao.defectDensityFbyProject(subproject, delivery);
  //          indicatorsPerfDao.Dispose();
  //          return request.CreateResponse(HttpStatusCode.OK, list);
  //      }

  //      [HttpPost]
  //      [Route("indPerfTest/defectDensity/data")]
  //      [ResponseType(typeof(IList<DefectDensity>))]
  //      public HttpResponseMessage defectDensityFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
  //          var indicatorsPerfDao = new indPerfTestDao();
  //          var list = indicatorsPerfDao.defectDensityFbydevManufsystemProject(parameters);
  //          indicatorsPerfDao.Dispose();
  //          return request.CreateResponse(HttpStatusCode.OK, list);
  //      }

  //      [HttpPost]
		//[Route("indPerfTest/defectOfTSInTI/data")]
  //      [ResponseType(typeof(IList<defectOfTSInTI>))]
  //      public HttpResponseMessage defectOfTSInTI_fbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters)
		//{
		//	var indicatorsPerfDao = new indPerfTestDao();
		//	var list = indicatorsPerfDao.defectOfTSInTI_fbydevManufsystemProject(parameters);
  //          indicatorsPerfDao.Dispose();
		//	return request.CreateResponse(HttpStatusCode.OK, list);
		//}
    }

}