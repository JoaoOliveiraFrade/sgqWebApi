using ProjectWebApi.Daos;
using ProjectWebApi.Daos.Ind.Oper.Dev;
using ProjectWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers.Ind.Oper.Dev {
    public class IndOperDevDefectReopenedController : ApiController {

        [HttpGet]
        [Route("indOperDev/defectReopened/dataFbyProject/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectReopened))]
        public HttpResponseMessage defectReopenedFbyProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var dao = new DefectReopenedDao();
            var result = dao.defectReopenedFbyProject(subproject, delivery);
            dao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        //[HttpPut]
        //[Route("indOperDev/DefectsReopenedIterations/{subproject}/{delivery}")]
        //[ResponseType(typeof(DefectReopened))]
        //public HttpResponseMessage getDefectReopenedByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations) {
        //    var TestProjDao = new TestProjDao();
        //    var item = TestProjDao.getDefectReopenedByProjectIterations(subproject, delivery, iterations);
        //    TestProjDao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, item);
        //}

        //[HttpGet]
        //[Route("indOperDev/defectsReopened")]
        //public List<DefectReopened> GetReopened()
        //{
        //    string sql = @"
        //        select
	       //         --'{' +   'date: ''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	       //         --'devManuf:''' + fabrica_desenvolvimento + ''', ' +
	       //         --'system:''' + sistema_defeito + ''', ' +
	       //         --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
	       //         --'subproject:''' + subprojeto + ''', ' +
	       //         --'delivery:''' + entrega + ''', ' +
	       //         --'qtyTotal:'  + '' + convert(varchar, count(*)) + ', ' +
	       //         --'qty:'  + '' + convert(varchar, sum(qtd_reopen)) + ', ' +
	       //         --'percent:'  + '' + convert(varchar, round(convert(float,sum(qtd_reopen)) / count(*) * 100,2)) + ', ' +
	       //         --'percentReference:5, ' +
	       //         --'qtyReference:'  + '' + convert(varchar, round(convert(float,count(*) * 0.05),2)) + 
	       //         --'}, ' as json,
	       //         substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	       //         fabrica_desenvolvimento as devManuf,
	       //         sistema_defeito as system,
	       //         convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as subDel,
	       //         entrega as delivery,
	       //         subprojeto as subproject,
	       //         count(*) as qtyTotal,
	       //         sum(qtd_reopen) as qty,
	       //         round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as [percent],
	       //         5 as percentReference,
	       //         round(convert(float,count(*) * 0.05),2) as qtyReference
        //        from 
	       //         alm_defeitos 
        //        where 
	       //         ciclo in ('TI', 'UAT') and
	       //         status_atual = 'CLOSED' and
	       //         dt_final <> ''
        //        group by
	       //         substring(dt_final,4,2),
	       //         substring(dt_final,7,2),
	       //         subprojeto,
	       //         entrega,
	       //         fabrica_desenvolvimento,
	       //         sistema_defeito
        //        order by 
	       //         substring(dt_final,7,2),
	       //         substring(dt_final,4,2),
	       //         fabrica_desenvolvimento
        //    ";

        //    var Connection = new Connection(Bancos.Sgq);
        //    List<DefectReopened> List = Connection.Executar<DefectReopened>(sql);
        //    Connection.Dispose();

        //    return List;
        //}



        //   [HttpGet]
        //   [Route("indOperDev/defectsReopened/{dateBegin}/{dateEnd}")]
        //   public List<DefectReopened> getReopenedByDate(string dateBegin, string dateEnd)
        //   {
        //       string sql = @"
        //           select
        //            substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
        //            fabrica_desenvolvimento as devManuf,
        //            sistema_defeito as system,
        //            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as subDel,
        //            entrega as delivery,
        //            subprojeto as subproject,
        //            count(*) as qtyTotal,
        //            sum(qtd_reopen) as qty,
        //            round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as [percent],
        //            5 as percentReference,
        //            round(convert(float,count(*) * 0.05),2) as qtyReference
        //           from 
        //            alm_defeitos 
        //           where 
        //            ciclo in ('TI', 'UAT') and
        //            status_atual = 'CLOSED' and
        //            dt_final <> '' and
        //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
        //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
        //           group by
        //   substring(dt_final,1,2),
        //            substring(dt_final,4,2),
        //            substring(dt_final,7,2),
        //            subprojeto,
        //            entrega,
        //            fabrica_desenvolvimento,
        //            sistema_defeito
        //           order by 
        //            substring(dt_final,7,2),
        //            substring(dt_final,4,2),
        //substring(dt_final,1,2),
        //            fabrica_desenvolvimento
        //       ";

        //       sql = sql.Replace("@dateBegin", dateBegin);
        //       sql = sql.Replace("@dateEnd", dateEnd);

        //       var Connection = new Connection(Bancos.Sgq);
        //       List<DefectReopened> List = Connection.Executar<DefectReopened>(sql);
        //       Connection.Dispose();

        //       return List;
        //   }

    }
}