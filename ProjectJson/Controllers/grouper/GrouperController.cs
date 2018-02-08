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
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class GrouperController : ApiController
    {
        [HttpGet]
        [Route("grouper/loadData")]
        [ResponseType(typeof(IList<Grouper>))]
        public HttpResponseMessage LoadData(HttpRequestMessage request)
        {
            var dao = new GrouperDao();
            var result = dao.LoadData();
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("grouper/loadById/{id}")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage LoadById(HttpRequestMessage request, string id)
        {
            var dao = new GrouperDao();
            var result = dao.LoadById(id);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("grouper/create")]
        [ResponseType(typeof(Grouper))]
        public HttpResponseMessage Create(HttpRequestMessage request, Grouper Grouper)
        {
            var dao = new GrouperDao();
            var result = dao.Create(Grouper);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("grouper/update/{id}")]
        public HttpResponseMessage Update(HttpRequestMessage request, string id, Grouper grouper)
        {
            var dao = new GrouperDao();
            dao.Update(id, grouper);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("grouper/delete/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var dao = new GrouperDao();
            dao.Delete(id);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

    }
}