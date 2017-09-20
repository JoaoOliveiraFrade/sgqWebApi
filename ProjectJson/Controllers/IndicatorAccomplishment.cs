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
    public class IndicatorAccomplishment : ApiController
    {
        [HttpGet]
        [Route("project/all")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage all(HttpRequestMessage request)
        {
            var projectDAO = new ProjectDAO();
            var projects = projectDAO.all();
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
		[Route("indicatorAccomplishment/slaOnTime/byListDevManufSystemProject")]
		[ResponseType(typeof(IList<SlaOnTime>))]
		public HttpResponseMessage slaOnTimeByListDevManufSystemProject(HttpRequestMessage request, Parameters parameters)
		{
			var indicatorAccomplishmentDAO = new IndicatorAccomplishmentDAO();
			var list = indicatorAccomplishmentDAO.slaOnTimeByListDevManufSystemProject(parameters);
            indicatorAccomplishmentDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
    }
}