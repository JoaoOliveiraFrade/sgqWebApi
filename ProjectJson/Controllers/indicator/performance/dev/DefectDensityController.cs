using ProjectWebApi.Daos.Ind.Perf.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Perf.Dev
{
    public class DefectDensityController : ApiController
    {
        [HttpPost]
        [Route("indPerfDev/defectDensity/data")]
        [ResponseType(typeof(IList<DefectOfTSInTI>))]
        public HttpResponseMessage data(HttpRequestMessage request, DevManufSystemProject parameters)
        {
            var dao = new DefectDensityDao();
            var result = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("indPerfDev/defectDensity/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectOfTSInTI))]
        public HttpResponseMessage dataFbyProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectDensityDao();
            var result = dao.dataFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }

}
