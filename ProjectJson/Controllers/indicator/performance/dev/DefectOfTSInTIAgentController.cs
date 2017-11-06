using ProjectWebApi.Daos.Ind.Perf.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Perf.Dev
{
    public class DefectOfTSInTIAgentController : ApiController
    {
        [HttpPost]
        [Route("indPerfDev/defectOfTSInTIAgent/data")]
        [ResponseType(typeof(IList<DefectOfTSInTI>))]
        public HttpResponseMessage defectOfTSInTIAgentFbydevManufsystemProject(HttpRequestMessage request, DevManufSystemProject parameters)
        {
            var dao = new DefectOfTSInTIAgentDao();
            var result = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("indPerfDev/defectOfTSInTIAgent/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectOfTSInTI))]
        public HttpResponseMessage defectOfTSInTIAgentFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var dao = new DefectOfTSInTIAgentDao();
            var result = dao.dataFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }

}