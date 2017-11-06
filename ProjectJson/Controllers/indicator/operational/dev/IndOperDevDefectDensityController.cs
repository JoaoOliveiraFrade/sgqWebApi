using ProjectWebApi.Daos;
using ProjectWebApi.Daos.Ind.Oper.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Oper.Dev {
    public class IndOperDevDefectDensityController : ApiController {
        [HttpPost]
        [Route("indOperDev/defectDensity/data")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage data(HttpRequestMessage request, DevManufSystemProject parameters) {
            var dao = new IndOperDevDefectDensityDao();
            var list = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("indOperDev/defectDensity/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectDensity>))]
        public HttpResponseMessage dataFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var dao = new IndOperDevDefectDensityDao();
            var list = dao.dataFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }

}