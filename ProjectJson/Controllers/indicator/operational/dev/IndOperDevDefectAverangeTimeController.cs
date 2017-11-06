using ProjectWebApi.Daos;
using ProjectWebApi.Daos.Ind.Oper.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Oper.Dev {
    public class IndOperDevDefectAverangeTimeController : ApiController {
        [HttpPost]
        [Route("indOperDev/defectAverangeTime/data")]
        [ResponseType(typeof(IList<DefectAverangeTime>))]
        public HttpResponseMessage data(HttpRequestMessage request, DevManufSystemProject parameters) {
            var dao = new IndOperDevDefectAverangeTimeDao();
            var list = dao.data(parameters);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("indOperDev/defectAverangeTime/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectAverangeTime>))]
        public HttpResponseMessage dataFbyProject(HttpRequestMessage request, string subproject, string delivery) {
            var dao = new IndOperDevDefectAverangeTimeDao();
            var list = dao.dataFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        //[HttpPost]
        //[Route("indOperDev/defectAverangeTime/fbyProjectAndListIteration")]
        //[ResponseType(typeof(DefectAverangeTime))]
        //public HttpResponseMessage defectAverangeTimeFbyProjectAndListIteration(HttpRequestMessage request, ProjectAndListIteration parameters)
        //{
        //    var dao = new IndOperDevDefectAverangeTimeDao();
        //    var result = dao.DefectAverangeTimeDao(parameters);
        //    dao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}

        //[HttpPost]
        //[Route("indOperDev/defectAverangeTime/fbydevManufsystemProjectIteration")]
        //[ResponseType(typeof(IList<DefectAverangeTime>))]
        //public HttpResponseMessage defectAverangeTimeFbydevManufsystemProjectIterations(HttpRequestMessage request, DevManufSystemProjectIteration parameters) {
        //    var dao = new IndOperDevDefectAverangeTimeDao();
        //    var result = dao.defectAverangeTimeFbydevManufsystemProjectIteration(parameters);
        //    dao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}