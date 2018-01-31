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
    public class AgileCentral : ApiController
    {
        [HttpPost]
        [Route("agileCentral/data")]
        [ResponseType(typeof(IList<AgileCentral>))]
        public HttpResponseMessage Data(HttpRequestMessage request, SubprojectDelivery subprojectDelivery) {
            var dao = new AgileCentralDao();
            var result = dao.Data(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("agileCentral/Detail")]
        [ResponseType(typeof(DefectDetail))]
        public HttpResponseMessage Detail(HttpRequestMessage request, SubprojectDelivery subprojectDelivery) {
            var dao = new AgileCentralDao();
            var result = dao.Detail(subprojectDelivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}