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
		[Route("pulledChain/loadData")]
		[ResponseType(typeof(IList<PulledChain>))]
		public HttpResponseMessage LoadData(HttpRequestMessage request)
		{
			var dao = new PulledChainDao();
            var result = dao.LoadData();
            dao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, result);
		}

        [HttpGet]
        [Route("pulledChain/chartCFD")]
        [ResponseType(typeof(IList<chartCFD>))]
        public HttpResponseMessage chartCFD(HttpRequestMessage request)
        {
            var dao = new PulledChainDao();
            var result = dao.chartCFD();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
		[Route("pulledChain/update")]
		public HttpResponseMessage update(PulledChain item)
		{
            var dao = new PulledChainDao();
            int result = dao.update(item);
            dao.Dispose();

            if (result == 0) {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}