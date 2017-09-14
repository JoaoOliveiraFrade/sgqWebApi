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
		[Route("pulledChain/all")]
		[ResponseType(typeof(IList<PulledChain>))]
		public HttpResponseMessage all(HttpRequestMessage request)
		{
			var pulledChainDAO = new PulledChainDAO();
            IList<PulledChain> projects = pulledChainDAO.getAll();
            pulledChainDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, projects);
		}

        [HttpGet]
        [Route("pulledChain/chartCFD")]
        [ResponseType(typeof(IList<chartCFD>))]
        public HttpResponseMessage chartCFD(HttpRequestMessage request)
        {
            var pulledChainDAO = new PulledChainDAO();
            IList<chartCFD> projects = pulledChainDAO.chartCFD();
            pulledChainDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPut]
		[Route("pulledChain/update/{id:int}")]
		public HttpResponseMessage update(int id, PulledChain item)
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