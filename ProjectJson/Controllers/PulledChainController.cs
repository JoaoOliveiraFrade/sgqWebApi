using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using ProjectWebApi.DAOs;
using ProjectWebApi.Models.Project;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class PulledChainController : ApiController
    {
		[HttpGet]
		[Route("PulledChain/All")]
		[ResponseType(typeof(IList<PulledChain>))]
		public HttpResponseMessage getAll(HttpRequestMessage request)
		{
			var pulledChainDAO = new PulledChainDAO();
            IList<PulledChain> projects = pulledChainDAO.getAll();
            pulledChainDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, projects);
		}

		[HttpPut]
		[Route("PulledChain/Update/{id:int}")]
		public HttpResponseMessage Update(int id, PulledChain item)
		{
            var pulledChainDAO = new PulledChainDAO();
            int result = pulledChainDAO.update(item);

            if (result == 0) {
                pulledChainDAO.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else {
                pulledChainDAO.Dispose();
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}