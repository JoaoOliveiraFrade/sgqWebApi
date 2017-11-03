﻿using Classes;
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
        [Route("Groupers")]
        [ResponseType(typeof(IList<Grouper>))]
        public HttpResponseMessage getGroupers(HttpRequestMessage request)
        {
            var GrouperDao = new GrouperDao();
            var Groupers = GrouperDao.getAll();
            GrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, Groupers);
        }

        [HttpGet]
        [Route("Grouper/{id}")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage getGroupers(HttpRequestMessage request, string id)
        {
            var GroupersDao = new GrouperDao();
            var Groupers = GroupersDao.get(id);
            GroupersDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, Groupers);
        }

        [HttpPut]
        [Route("Grouper/create")]
        [ResponseType(typeof(Grouper))]
        public HttpResponseMessage UpdatetGrouper(HttpRequestMessage request, Grouper Grouper)
        {
            var GrouperDao = new GrouperDao();
            var createdItem = GrouperDao.Create(Grouper);
            GrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, createdItem);
        }

        [HttpPut]
        [Route("Grouper/update/{id}")]
        public HttpResponseMessage UpdatetGrouper(HttpRequestMessage request, string id, Grouper Grouper)
        {
            var GrouperDao = new GrouperDao();
            GrouperDao.Update(id, Grouper);
            GrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("Grouper/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var GrouperDao = new GrouperDao();
            GrouperDao.Delete(id);
            GrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

    }
}