using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using ProjectWebApi.Daos;
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
			var pulledChainDao = new PulledChainDao();
            IList<PulledChain> projects = pulledChainDao.getAll();
            pulledChainDao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, projects);
		}

        [HttpGet]
        [Route("pulledChain/chartCFD")]
        [ResponseType(typeof(IList<chartCFD>))]
        public HttpResponseMessage chartCFD(HttpRequestMessage request)
        {
            var pulledChainDao = new PulledChainDao();
            IList<chartCFD> projects = pulledChainDao.chartCFD();
            pulledChainDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPut]
		[Route("pulledChain/update")]
		public HttpResponseMessage update(PulledChain item)
		{
            var pulledChainDao = new PulledChainDao();
            int result = pulledChainDao.update(item);

            if (result == 0) {
                pulledChainDao.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else {
                pulledChainDao.Dispose();
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}