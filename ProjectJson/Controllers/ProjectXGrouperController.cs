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
    public class ProjectXGrouperController : ApiController
    {
        [HttpGet]
        [Route("ProjectXGrouper/All")]
        [ResponseType(typeof(IList<ProjectXGrouper>))]
        public HttpResponseMessage getProjectXGrouper(HttpRequestMessage request)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            var list = ProjectXGrouperDao.GetAll();
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("ProjectXGrouper/ByProject/{id}")]
        [ResponseType(typeof(IList<ProjectXGrouper>))]
        public HttpResponseMessage getByProject(HttpRequestMessage request, string id)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            var list = ProjectXGrouperDao.GetByProject(id);
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("ProjectXGrouper/ByProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProjectXGrouper>))]
        public HttpResponseMessage getProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            var list = ProjectXGrouperDao.GetByProject(subproject, delivery);
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("ProjectXGrouper/ByGrouper/{id}")]
        [ResponseType(typeof(IList<ProjectXGrouper>))]
        public HttpResponseMessage getByGrouper(HttpRequestMessage request, string id)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            var list = ProjectXGrouperDao.GetByGroup(id);
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("ProjectXGrouper/Create/{GrouperId}/{ProjectId}/{Subproject}/{Delivery}")]
        public HttpResponseMessage CreateProjectXGrouper(HttpRequestMessage request, string GrouperId, string ProjectId, string Subproject, string Delivery)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            ProjectXGrouperDao.Create(GrouperId, ProjectId, Subproject, Delivery);
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("ProjectXGrouper/Delete/{GrouperId}/{ProjectId}")]
        public HttpResponseMessage DeleteProjectXGrouper(HttpRequestMessage request, string GrouperId, string ProjectId)
        {
            var ProjectXGrouperDao = new ProjectXGrouperDao();
            ProjectXGrouperDao.Delete(GrouperId, ProjectId);
            ProjectXGrouperDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK);
        }

        //[HttpGet]
        //[Route("ProjectXGrouper/One/{id}")]
        //[ResponseType(typeof(ProjectXGrouper))]
        //public HttpResponseMessage getGroups(HttpRequestMessage request, string id)
        //{
        //    var GroupsDao = new GroupDao();
        //    var Groups = GroupsDao.getOne(id);
        //    GroupsDao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, Groups);
        //}

    }
}