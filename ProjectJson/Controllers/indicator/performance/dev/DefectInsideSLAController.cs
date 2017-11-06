using ProjectWebApi.Daos.Ind.Perf.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Perf.Dev
{
    public class DefectInsideSLAController : ApiController
    {
        [HttpPost]
        [Route("indPerfDev/defectInsideSLA/data")]
        [ResponseType(typeof(IList<DefectInsideSLA>))]
        public HttpResponseMessage data(HttpRequestMessage request, DevManufSystemProject parameters) {
            var dao = new DefectInsideSLADao();
            var result = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpPost]
        //[Route("indOperDev/defectInsideSLA/data")]
        //[ResponseType(typeof(IList<DefectInsideSLA>))]
        //public HttpResponseMessage defectInsideSLAFbyListTestManufSystemProject(HttpRequestMessage request, devManufsystemProject parameters)
        //{
        //    var indPerfDevDao = new indPerfDevDao();
        //    var list = indPerfDevDao.defectInsideSLAFbyListTestManufSystemProject(parameters);
        //    indPerfDevDao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, list);
        //}
    }

}